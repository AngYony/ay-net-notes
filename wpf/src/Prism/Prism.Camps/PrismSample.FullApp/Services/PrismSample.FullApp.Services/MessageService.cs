using PrismSample.FullApp.Services.Interfaces;

namespace PrismSample.FullApp.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
