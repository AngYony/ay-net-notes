using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcServiceDemo.Services
{

    public class WeiHuDaiService : WeiHuDai.WeiHuDaiBase
    {
        private readonly ILogger<WeiHuDaiService> _logger;
        public WeiHuDaiService(ILogger<WeiHuDaiService> logger)
        {
            _logger = logger;
        }

        public override Task<WeiHuDaiDtoList> GetWeiHuDaiList(WeiHuDaiListFrom request, ServerCallContext context)
        {
            var dto = new WeiHuDaiDtoList();
            dto.WhdDto.Add(new WeiHuDaiDto { Name = "张三", ChangDu = 100 });
            dto.WhdDto.Add(new WeiHuDaiDto { Name = "李四", ChangDu = 200 });

            return Task.FromResult(dto);
        }
    }

}
