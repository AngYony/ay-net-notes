using AY.Samples.Common;
using Microsoft.Extensions.AI;

namespace AY.Samples.Console01
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //用户请求
            var userRequest = "帮我订一张从北京到上海的票，后天下午出发。";


            // 基线提示：只包含任务和输入
            var baselinePrompt = $"""
请帮我识别用户意图。
用户请求是：{userRequest}
意图：
""";

            // 结构化提示：约束 JSON 输出格式
            var structuredPrompt = $$"""
你需要识别用户的铁路票务意图，选项仅包括：
* 订票意图
* 退票意图
* 咨询意图

请严格以如下 JSON 格式返回结果：
{
    "intention": "<订票意图/退票意图/咨询意图>",
    "reason": "<简要说明识别理由>"
}

用户请求：{{userRequest}}
意图：
""";

            // 少样本提示：提供 1-2 个示例
            var fewShotPrompt = $$"""
你需要识别用户的铁路票务意图，选项仅包括：
* 订票意图
* 退票意图
* 咨询意图

请严格以如下 JSON 格式返回结果：
{
    "intention": "<订票意图/退票意图/咨询意图>",
    "reason": "<简要说明识别理由>"
}

<example>
用户请求：我的票能退吗？
意图：
{
    "intention": "退票意图",
    "reason": "用户询问是否可以退票"
}
</example>

<example>
用户请求：请问高铁可以带宠物吗？
意图：
{
    "intention": "咨询意图",
    "reason": "用户咨询乘车规则，非订退票"
}
</example>

用户请求：{{userRequest}}
意图：
""";

            var chatService = AIClientHelper.GetDefaultChatClient(Common.Enums.ChatProvider.General, "gpt-5.2-medium");
            var mess = new ChatMessage(ChatRole.User, baselinePrompt);


            var chatOptions = new ChatOptions
            {
                Temperature = 0.1f,     //低随机性，保证稳定输出
                TopP = 0.9f,            //核采样概率
                MaxOutputTokens = 120,  //限制输出长度
            };

            var response = await chatService.GetResponseAsync(mess, chatOptions);

            Console.WriteLine($"回答：{response.Text}");
            Console.WriteLine($"Token使用：{response.Usage?.TotalTokenCount ?? 0}");
            Console.WriteLine($"模型 ：{response.ModelId}");




            //Console.WriteLine(response);

            //var chatClientMetadata = chatService.GetRequiredService<ChatClientMetadata>();
            //Console.Write(chatClientMetadata);
        }
    }
}
