using AY.Samples.Common;
using Microsoft.Extensions.AI;
using System.ComponentModel;
using System.Reflection;

namespace AY.Samples.Console02
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //如何从一个工具类中批量收集公开方法并注册为可调用的工具

            var messages = new List<ChatMessage>
            {
                new ChatMessage(ChatRole.System,"你是出行助手，善于调用工具给出穿搭建议。"),
                new ChatMessage(ChatRole.User, "帮我查看今天北京的天气，并给出穿搭建议")
            };


            var travelTools = new TravelToolset();

            IList<AITool> batchRegisteredTools =
                typeof(TravelToolset)
                    .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    .Select(method => AIFunctionFactory.Create(
                        method,
                        travelTools,
                        name: method.Name.ToLowerInvariant(),
                        description: method.GetCustomAttribute<DescriptionAttribute>()?.Description))
                    .Cast<AITool>()
                    .ToList();

            foreach (var tool in batchRegisteredTools)
            {
                Console.WriteLine($"Registered tool: {tool.Name} - {tool.Description}");
            }



            // 可以与其它工具合并后交给 ChatOptions.Tools 使用
            ChatOptions batchOptions = new()
            {
                ToolMode = ChatToolMode.Auto,   // 自动决定是否调用工具，默认值为 Auto
                AllowMultipleToolCalls =true,   // 允许模型一次调用多个工具，默认 false
                Tools = batchRegisteredTools
            };


            var chatClient = AIClientHelper.GetDefaultChatClient();

            var datetimeTool = AIFunctionFactory.Create(() => DateTime.Now, "get_current_datetime", "获取当前的日期和时间");

            var client = chatClient.AsBuilder()
                
                .UseFunctionInvocation(configure: options =>
                {
                    options.AdditionalTools = [datetimeTool]; // 注册一些额外的工具，比如时间工具等
                    options.AllowConcurrentInvocation = true; // 允许模型并发调用多个函数，默认 false。- 示例场景：同时查询多个城市的天气
                    options.IncludeDetailedErrors = true; // 包含详细错误信息，默认 false
                    options.MaximumConsecutiveErrorsPerRequest = 3; // 每个请求允许的最大连续错误数，防止无限循环，默认 3次
                    options.MaximumIterationsPerRequest = 5; // 每个请求允许的最大迭代次数，防止无限循环，默认 40次
                    options.TerminateOnUnknownCalls = false; // 当模型调用了未知的函数时，是否终止对话
                    //自定义函数执行器，拦截并自定义所有函数调用的执行逻辑
                    options.FunctionInvoker = (context, cancellationToken) =>
                    {
                        var functionCall = context.Function;

                        //Console.WriteLine($"Invoking function: {functionCall.Name} with arguments: {functionCall.AdditionalProperties}");

                        return context.Function.InvokeAsync(context.Arguments, cancellationToken);
                    };
                })
                .Build();


            var response = await client.GetResponseAsync(messages, batchOptions);

             

            Console.WriteLine($"回答：{response.Text}");
            Console.WriteLine($"Token使用：{response.Usage?.TotalTokenCount ?? 0}");
            Console.WriteLine($"模型 ：{response.ModelId}");
        }


    }
}
