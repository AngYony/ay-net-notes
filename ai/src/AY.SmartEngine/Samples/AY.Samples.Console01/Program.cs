using AY.Samples.Common;
using Microsoft.Extensions.AI;

namespace AY.Samples.Console01
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var chatService = AIClientHelper.GetDefaultChatClient("OpenAI", "gpt-5.2-medium");
            var mess = new ChatMessage(ChatRole.User, "Hello, who are you?");
            
            var response = await chatService.GetResponseAsync(mess);
            Console.WriteLine(response);
        }
    }
}
