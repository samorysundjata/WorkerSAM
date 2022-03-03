using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WorkerSAM
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("O serviço está iniciando.");

            stoppingToken.Register(() => _logger.LogInformation("Tarefa de segundo plano está parando."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Executando tarefa: {time}", DateTimeOffset.Now);
                await Task.Delay(10000, stoppingToken);
            }

            _logger.LogInformation("O serviço está parando.");
        }

    }
}
