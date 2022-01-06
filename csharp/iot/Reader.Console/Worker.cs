using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MBusReader.Code;
using MBusReader.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Reader.Console
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Worker> _logger;

        public Worker(IConfiguration configuration, ILogger<Worker> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Running ...");
            var fileName = $"data{Path.DirectorySeparatorChar}binary-{DateTime.Now.ToString("yyyy-MM-dd-HH-mm")}.dat";
            _logger.LogInformation($"Filename: {fileName}");
            _logger.LogInformation("Starting reading data ...!");
            
            Stream stream = new FileStream(fileName,FileMode.Create);
            ISettingsSerial serialSettings = new SettingsSerial();
            if (System.OperatingSystem.IsWindows())
            {
                serialSettings.PortName = "com3";
            }
            
            IMBusReader mbusReader = new ReliableMBusReader(stream, serialSettings);
            // IMBusReader mbusReader = new MBusReader.Code.MBusReader(stream, serialSettings);
            mbusReader.Run(true);
            
            return Task.CompletedTask;
        }
    }
}