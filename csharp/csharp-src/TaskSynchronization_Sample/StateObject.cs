using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSynchronization_Sample
{
    class StateObject
    {
        private int _state = 5;

        private object _sync = new object();
        public void Changestate(int loop)
        {
            lock (_sync)
            {
                if (_state == 5)
                {
                    _state++;
                    Trace.Assert(_state == 6, $"在循环{loop}之后发生竞争条件");
                }

                _state = 5;
            }
        }

         
    }
}
