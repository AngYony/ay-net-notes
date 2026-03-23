using Microsoft.Extensions.AI;
using OpenAI;
using Azure.AI.OpenAI;
using Anthropic;
using System.ClientModel;
using Microsoft.Extensions.Logging;
using AY.Samples.Common.Enums;

namespace AY.Samples.Common
{
    public static class AIClientHelper
    {
        /// <summary>
        /// 获取默认的聊天客户端，默认使用 AzureOpenAI 和 gpt-4o 模型
        /// AzureOpenAI: gpt-4o
        /// DeepSeek: deepseek-chat, deepseek-reasoner
        /// Qwen: qwen-max, qwen-plus, qwen-flash
        /// Anthropic: glm-4.5-air
        /// </summary>
        public static IChatClient GetDefaultChatClient(ChatProvider provider = ChatProvider.General, string model = "gpt-5.2-medium", bool enableLogging = false)
        {
            IChatClient chatClient = provider switch
            {
                ChatProvider.AzureOpenAI => GetAzureOpenAIClient(enableLogging).GetChatClient(model).AsIChatClient(),
                ChatProvider.DeepSeek => GetDeepSeekClient(enableLogging).GetChatClient(model).AsIChatClient(),
                ChatProvider.Qwen => GetQwenClient(enableLogging).GetChatClient(model).AsIChatClient(),
                ChatProvider.Anthropic => GetAnthropicClient(enableLogging).AsIChatClient(defaultModelId: model),
                ChatProvider.General => GetAIClient(Keys.OpenAIEndpoint, Keys.OpenAIApiKey, enableLogging).GetChatClient(model).AsIChatClient(),
                _ => GetAIClient(Keys.OpenAIEndpoint, Keys.OpenAIApiKey, enableLogging).GetChatClient(model).AsIChatClient(),
            };

            return chatClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enableLogging"></param>
        /// <returns></returns>
        public static AnthropicClient GetAnthropicClient(bool enableLogging = false)
        {
            var client = new AnthropicClient()
            {
                ApiKey = Keys.ZhipuApiKey,
                BaseUrl = Keys.ZhipuEndpoint
            };

            return client;
        }


        /// <summary>
        /// Azure OpenAI 服务
        /// </summary>
        /// <param name="enableLogging"></param>
        /// <returns></returns>
        public static AzureOpenAIClient GetAzureOpenAIClient(bool enableLogging = false)
        {
            var endpoint = Keys.AzureOpenAIEndpoint;
            var apiKey = Keys.AzureOpenAIApiKey;

            var clientOptions = new AzureOpenAIClientOptions();

            if (enableLogging)
            {
                var clientLoggingOptions = NLogHelper.CreateClientLoggingOptions();

                clientOptions.ClientLoggingOptions = clientLoggingOptions;
            }

            var client = new AzureOpenAIClient(new Uri(endpoint), new ApiKeyCredential(apiKey), clientOptions);

            return client;
        }

        public static IEmbeddingGenerator<string, Embedding<float>> GetAzureOpenAIEmbeddingGenerator()
        {
            var client = GetAzureOpenAIClient();
            return client.GetEmbeddingClient("text-embedding-3-small").AsIEmbeddingGenerator();
        }


        public static OpenAIClient GetDeepSeekClient(bool enableLogging = false)
            => GetAIClient(Keys.DeepSeekEndpoint, Keys.DeepSeekApiKey, enableLogging);

        public static OpenAIClient GetQwenClient(bool enableLogging = false) =>
            GetAIClient(Keys.QwenEndpoint, Keys.QwenApiKey, enableLogging);



        /// <summary>
        /// 对于兼容 OpenAI API 的服务都可以使用
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="apiKey"></param>
        /// <param name="enableLogging"></param>
        /// <returns></returns>
        private static OpenAIClient GetAIClient(string endpoint, string apiKey, bool enableLogging = false)
        {
            OpenAIClientOptions clientOptions = new OpenAIClientOptions();
            clientOptions.Endpoint = new Uri(endpoint);

            if (enableLogging)
            {
                var clientLoggingOptions = NLogHelper.CreateClientLoggingOptions();

                clientOptions.ClientLoggingOptions = clientLoggingOptions;
            }

            // 创建自定义的OpenAI客户端
            OpenAIClient aiClient = new(new ApiKeyCredential(apiKey), clientOptions);
            return aiClient;

        }
    }
}
