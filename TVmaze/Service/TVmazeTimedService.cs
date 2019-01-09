using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TVmaze.Services.Interfaces;

namespace TVmaze.Service
{
    public class TVmazeTimedService : IHostedService, IDisposable
    {
        private readonly ILogger<TVmazeTimedService> _logger;
        private readonly ITVmazeService _tvmazeService;
        private CancellationToken _cancellationToken;

        private Timer _timer;
        private bool _updating = false;

        public TVmazeTimedService(ILogger<TVmazeTimedService> logger, ITVmazeService tvmazeService)
        {
            _logger = logger;
            _tvmazeService = tvmazeService;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start TVmaze Service");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(15));

            _cancellationToken = cancellationToken;

            return Task.CompletedTask;

        }

        private void DoWork(object state)
        {
            try
            {
                if (_updating)
                    return;

                _updating = true;
                _logger.LogInformation("Run TVmaze Service.");

                _tvmazeService.UpdateShows(_cancellationToken);

                _updating = false;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while updating shows");
            }



        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
