using Microsoft.AspNetCore.Authorization;

namespace AuthorizationElementary;

public class TimeAuthorizationRequirement : IAuthorizationRequirement
{
    public TimeOnly StartTime { get; }
    public TimeSpan Duration { get; }

    public TimeAuthorizationRequirement(TimeOnly startTime, TimeSpan duration)
    {
        StartTime = startTime;
        Duration = duration;
    }
}

public class TimeAuthorizationHandler : AuthorizationHandler<TimeAuthorizationRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TimeAuthorizationRequirement requirement)
    {
        var now = DateTime.Now;
        var start = new DateTime(now.Year, now.Month, now.Day, requirement.StartTime.Hour, requirement.StartTime.Minute, requirement.StartTime.Second);
        var end = start.Add(requirement.Duration);

        if(now >= start && now < end)
        {
            // 标记该授权要求已经由当前处理器验证通过
            // 一个要求可以和多个处理器关联，只要一个处理器验证通过即可
            // 授权上下文借此记录要求的检查情况
            context.Succeed(requirement);
        }
        else
        {
            // 强制使授权验证失败，即使所有规则都验证通过
            // 如果不想如此极端，仅表达当前处理器验证失败但不阻止其他处理器通过，直接return或return Task.CompletedTask即可
            //context.Fail();
            return;
        }
    }
}

public class MyResource
{
    public string Name { get; set; }
}

public class TimeAndNameAuthorizationHandler : AuthorizationHandler<TimeAuthorizationRequirement, MyResource>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TimeAuthorizationRequirement requirement, MyResource resource)
    {
        var now = DateTime.Now;
        var start = new DateTime(now.Year, now.Month, now.Day, requirement.StartTime.Hour, requirement.StartTime.Minute, requirement.StartTime.Second);
        var end = start.Add(requirement.Duration);

        if (now >= start && now < end && resource.Name == "Admin")
        {
            context.Succeed(requirement);
        }
    }
}

public class PermissionHandler : IAuthorizationHandler
{
    private ILogger<PermissionHandler> _logger;

    public PermissionHandler(ILogger<PermissionHandler> logger)
    {
        _logger = logger;
    }

    public async Task HandleAsync(AuthorizationHandlerContext context)
    {
        foreach(var requirement in context.PendingRequirements)
        {
            if(requirement is TimeAuthorizationRequirement timeTequirement)
            {
                var now = DateTime.Now;
                var start = new DateTime(now.Year, now.Month, now.Day, timeTequirement.StartTime.Hour, timeTequirement.StartTime.Minute, timeTequirement.StartTime.Second);
                var end = start.Add(timeTequirement.Duration);

                if (now >= start && now < end)
                {
                    context.Succeed(requirement);
                    _logger.LogInformation("授权通过。");
                }
            }
        }
    }
}
