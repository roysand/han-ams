using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MBusReader.Code;
using MBusReader.Contracts;
using MessageParser.Code;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureServiceBus
{
    public class ReadQueueSaveToDb
    {
        private readonly ILogger<ReadQueueSaveToDb> _logger;
        // TODO Add to configuration file
        private readonly string QueueConnectionString = "";

        public ReadQueueSaveToDb(ILogger<ReadQueueSaveToDb> log)
        {
            _logger = log;
        }

        [FunctionName("ReadQueueSaveToDb")]
        public async Task Run(
        [ServiceBusTrigger("ams", "raw", Connection = "QueueConnection")] string mySbMsg)
        {
            try
            {
                var raw = JsonSerializer.Deserialize<RawMessage>(mySbMsg);
                // TODO: Add to configuration file
                var connectionString = "";
                Console.WriteLine($"Message len: {mySbMsg.Length} Message: {mySbMsg}");
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var rows = await SaveToRawTable(raw, conn);
                    var hdlcMessage = ParseMessage(raw.Raw);

                    foreach (var data in hdlcMessage.Data)
                    {
                        rows = await SaveToDetailTable(conn, data, raw);
                    }

                    if (hdlcMessage.Data.Count > 0)
                    {
                        Console.WriteLine(JsonSerializer.Serialize(hdlcMessage));
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message); 
                Console.WriteLine(ex.Message);
            }
        }

        private async Task<int> SaveToRawTable(RawMessage raw, SqlConnection conn)
        {
            int rows;

            var sql = "Insert Into dbo.raw (MeasurementId, TimeStamp, Location, Raw, IsNew) " +
            $"Values('{raw.Id}', '{raw.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fffff")}', '{raw.Location}', '{ raw.Raw}', 1)";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                rows = await cmd.ExecuteNonQueryAsync();
            }

            return rows;
        }

        private async Task<int> SaveToDetailTable(SqlConnection conn, IHDLCData data, IRawMessage rawMessage)
        {
            int rows;

            var sql = "Insert Into dbo.Detail(MeasurementId, TimeStamp, Location, Name, ObisCode, Unit, ValueStr, ValueNum) " +
                $"Values ('{rawMessage.Id}', '{rawMessage.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fffff")}', '{rawMessage.Location}', '{data.Name}', '{data.ObisCode}', '{data.Unit}'";

            if (data.Value != -1)
            {
                sql += $",null, @ValueNum)";
            }
            else
            {
                sql += $",'{data.Name}',-1)";
            }
            Console.WriteLine(sql);

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (data.Value != -1)
                {
                    //cmd.Parameters.Add(new SqlParameter("@ValueNum", data.Value.ToString().Replace(",",".")));
                    //cmd.Parameters.AddWithValue("@ValueNum", data.Value);
                   // data.Value = 12.124M;
                    cmd.Parameters.Add("@ValueNum", SqlDbType.Float);
                    cmd.Parameters["@ValueNum"].Value = data.Value;
                }
                rows = await cmd.ExecuteNonQueryAsync();
            }

            return rows;
        }

        private IHDLCMessage ParseMessage(string messageAsRawString)
        {
            var parser = new Parser();
            messageAsRawString = messageAsRawString.Replace(" ", "");
            var messageByteArray = Enumerable.Range(0, messageAsRawString.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(messageAsRawString.Substring(x, 2), 16).ToString("X2"));

            var message = messageByteArray.Skip(1).Take(messageByteArray.Count() - 1).ToList();
            message.ForEach(x => Console.Write($"{x} "));
            Console.WriteLine();
            var hdlcMessage = parser.Parse(messageByteArray.Select(x => (byte)(Convert.ToInt32(x, 16))).ToList());

            return hdlcMessage;
        }
    }
}
