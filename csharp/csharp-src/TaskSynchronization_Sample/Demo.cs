using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSynchronization_Sample
{
    class Demo2
    {
        public void DoThis()
        {
            lock (this)
            {

            }

        }

        public void DoThat()
        {
            lock (this)
            {

            }
        }


        private object _syncRoot = new object();
        public void DoThis2()
        {
            lock (_syncRoot)
            {

            }

        }

        public void DoThat2()
        {
            lock (_syncRoot)
            {

            }
        }

    }

    

    public class Demo
    {
        public virtual bool IsSynchronized => false;

        public static Demo Synchronized(Demo d)
        {
            if (!d.IsSynchronized)
            {
                return new SynchronizedDemo(d);
            }
            return d;
        }

        public virtual void DoThis()
        {

        }

        public virtual void DoThat()
        {

        }


        class SynchronizedDemo : Demo
        {
            private object _syncRoot = new object();
            private Demo _d;
            public SynchronizedDemo(Demo d)
            {
                _d = d;
            }
            public override bool IsSynchronized => true;

            public override void DoThis()
            {
                lock (_syncRoot)
                {
                    _d.DoThis();
                }
            }

            public override void DoThat()
            {
                lock (_syncRoot)
                {
                    _d.DoThat();
                }
            }
        }


       
    }

}
