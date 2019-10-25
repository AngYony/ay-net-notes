using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSynchronization_Sample
{
    class SharedState
    {
        public int State { get; set; }
        //private int _state = 0;
        //private object _syncRoot = new object();
        //public int State
        //{
        //    get
        //    {
        //        lock (_syncRoot)
        //        {
        //            return _state;
        //        }
        //    }

        //    set
        //    {
        //        lock (_syncRoot)
        //        {
        //            _state = value;
        //        }
        //    }
        //}


        //private int _state = 0;
        //private object _syncRoot = new object();
        //public int State { get; private set; } = 0;
        //public int IncrementState()
        //{
        //    lock (_syncRoot)
        //    {
        //        return ++State;
        //    }
        //}
    }
}
