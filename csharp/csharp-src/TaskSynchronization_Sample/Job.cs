using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSynchronization_Sample
{
    class Job
    {
        private SharedState _sharedState;
        public Job(SharedState sharedState)
        {
            this._sharedState = sharedState;
        }

        //public void DoTheJob()
        //{
        //    for (int i = 0; i < 50000; i++)
        //    {
        //        _sharedState.State += 1;
        //    }
        //}


        public void DoTheJob()
        {
            for (int i = 0; i < 50000; i++)
            {
                lock (_sharedState)
                {
                    _sharedState.State += 1;
                }
            }
        }
    }
}
