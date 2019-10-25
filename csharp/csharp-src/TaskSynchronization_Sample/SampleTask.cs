using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSynchronization_Sample
{
    class SampleTask
    {
        public void RaceCondition(object obj)
        {
            Trace.Assert(obj is StateObject, "obj必须是StateObject类型");
            StateObject state = obj as StateObject;
            int i = 0;
            while (true)
            {
                lock (state)
                {
                    state.Changestate(i++);
                }
            }
        }

        public void RaceConditions()
        {
            var state = new StateObject();
            for (int i = 0; i < 2; i++)
            {
                Task.Run(()=> { new SampleTask().RaceCondition(state); });
            }
        }

        public SampleTask() { }

        private StateObject _s1;
        private StateObject _s2;
        public SampleTask(StateObject s1,StateObject s2)
        {
            _s1 = s1;
            _s2 = s2;
        }
        public void Deadlock1()
        {
            int i = 0;
            while (true)
            {
                lock (_s1)
                {
                    lock (_s2)
                    {
                        _s1.Changestate(i);
                        _s2.Changestate(i++);
                        Console.WriteLine("still running,"+i);
                    }
                }
            }
        }

        public void Deadlock2()
        {
            int i = 0;
            while(true)
            {
                lock (_s2)
                {
                    lock (_s1)
                    {
                        _s1.Changestate(i);
                        _s2.Changestate(i++);
                        Console.WriteLine("still running,"+i);
                    }
                }
            }
        }
    }
}
