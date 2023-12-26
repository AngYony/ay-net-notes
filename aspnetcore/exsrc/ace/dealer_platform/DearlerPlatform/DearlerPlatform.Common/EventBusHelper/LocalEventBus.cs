using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DearlerPlatform.Common.EventBusHelper
{
    public class LocalEventBus<T>
        where T : class
    {
        public delegate Task LocalEventHandler(T t);
        public event LocalEventHandler localEventHandler;
        public async Task Publish(T t)
        {
            await localEventHandler(t);
        }

    }
}