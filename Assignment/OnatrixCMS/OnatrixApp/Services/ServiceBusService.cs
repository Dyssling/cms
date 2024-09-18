using Azure.Messaging.ServiceBus;
using System.Diagnostics;

namespace OnatrixApp.Services
{
    public class ServiceBusService
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSender _sender;

        public ServiceBusService(IConfiguration configuration)
        {
            _client = new ServiceBusClient(configuration.GetSection("ServiceBusConnection").Value);
            _sender = _client.CreateSender(configuration.GetSection("ServiceBusQueue").Value);
        }

        public async Task PublishAsync(string message)
        {
            try
            {
                var payload = new ServiceBusMessage(message);

                await _sender.SendMessageAsync(payload);
            }

            catch (Exception ex){ Debug.WriteLine(ex.Message); }
        }
    }
}
