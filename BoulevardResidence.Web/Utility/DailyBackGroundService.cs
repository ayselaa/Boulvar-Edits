using BoulevardResidence.Domain.Data;
using BoulevardResidence.Domain.Entity.DailyBackGround;
using BoulevardResidence.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BoulevardResidence.Web.Utility
{
    public class DailyBackgroundService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;
    
        public DailyBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var interval = TimeSpan.FromDays(1); 

            _timer = new Timer(DoWork, null, TimeSpan.Zero, interval); 
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {

            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<IApartmentCreatedService>();



                    await service.UpdatedApartmentsWithBackgroundService();
                }
            }
            catch (Exception ex)
            {
                throw new Exception( ex.Message);
            }
           
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
