using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MBusReader.Code;
using MBusReader.Contracts;
using MessageParser.Code;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AzureServiceBus
{
    public class ReadQueueSaveToDb
    {
        private readonly ILogger<ReadQueueSaveToDb> _logger;
        private readonly IConfiguration _configuration;
        private readonly IRawRepository<RawData> _rawRepository;
        private readonly IDetailRepository<Detail> _detailRepository;

        public ReadQueueSaveToDb(ILogger<ReadQueueSaveToDb> log
            , IConfiguration configuration
            , IRawRepository<RawData> rawRepository
            , IDetailRepository<Detail> detailRepository) 
        {
            _logger = log;
            _configuration = configuration;
            _rawRepository = rawRepository;
            _detailRepository = detailRepository;
        }

        [FunctionName("ReadQueueSaveToDb")]
        public async Task Run(
        [ServiceBusTrigger("ams", "raw", Connection = "QueueConnection")] string mySbMsg
            ,CancellationToken cancellationToken)
        {
            try
            {
                var raw = JsonSerializer.Deserialize<RawData>(mySbMsg);
                _rawRepository.Add(raw);
                var result = await _rawRepository.SaveChangesAsync(cancellationToken);
                
                var hdlcMessage = ParseMessage(raw.Raw);
                foreach (var data in hdlcMessage.Data)
                {
                    var detail = new Detail()
                    {
                        MeasurementId = raw.MeasurementId,
                        TimeStamp = raw.TimeStamp,
                        Location = raw.Location,
                        ObisCode = data.ObisCode,
                        Unit = data.Unit,
                        Name = data.Name,
                        ValueStr = data.Name,
                        ValueNum = data.Value
                    };
                    
                    _detailRepository.Add(detail);
                    
                    Console.WriteLine(JsonSerializer.Serialize(detail));
                }
                
                result += await _detailRepository.SaveChangesAsync(cancellationToken);

                // var connectionString = _configuration.GetConnectionString("SQLAZURECONNSTR_AMS");
                //
                // using (SqlConnection conn = new SqlConnection(connectionString))
                // {
                //     conn.Open();
                //     // var rows = await SaveToRawTable(raw, conn);
                //     var hdlcMessage1 = ParseMessage(raw.Raw);
                //
                //     foreach (var data in hdlcMessage1.Data)
                //     {
                //         var rows = await SaveToDetailTable(conn, data, raw);
                //     }
                //
                //     if (hdlcMessage.Data.Count > 0)
                //     {
                //         Console.WriteLine(JsonSerializer.Serialize(hdlcMessage));
                //     }
                // }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message); 
                Console.WriteLine(ex.Message);
            }
        }

        private async Task<int> SaveToRawTable(RawData raw, SqlConnection conn)
        {
            int rows;

            var sql = "Insert Into dbo.raw (MeasurementId, TimeStamp, Location, Raw, IsNew) " +
            $"Values('{raw.MeasurementId}', '{raw.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fffff")}', '{raw.Location}', '{ raw.Raw}', 1)";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                rows = await cmd.ExecuteNonQueryAsync();
            }

            return rows;
        }

        private async Task<int> SaveToDetailTable(SqlConnection conn, IHDLCData data, RawData rawMessage)
        {
            int rows;

            var sql = "Insert Into dbo.Detail(Id,MeasurementId, TimeStamp, Location, Name, ObisCode, Unit, ValueStr, ValueNum) " +
                $"Values ('{Guid.NewGuid()}', '{rawMessage.MeasurementId}', '{rawMessage.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss.fffff")}', '{rawMessage.Location}', '{data.Name}', '{data.ObisCode}', '{data.Unit}'";

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
