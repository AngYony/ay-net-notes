using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }




        private CancellationTokenSource _cts;


        private async void button1_Click(object sender, EventArgs e)
        {



            try
            {

                _cts = new CancellationTokenSource();
                var token = _cts.Token;
                await Task.Run(() =>
            {

                int wy = 0;
                for (int i = 0; i < 100; i++)
                {
                    {
                        Task.Delay(1 * 1000).Wait();
                        Action action = () => { label1.Text = (++wy).ToString(); };
                        label1.Invoke(action);
                    }
                }

            }, token);


            }
            catch (OperationCanceledException ex)
            {

                throw;
            }




            //CancellationToken token = ctSource.Token;

            //try
            //{









            //try
            //{
            //    CancellationTokenSource ctSource = new CancellationTokenSource();
            //CancellationToken token = ctSource.Token;

            //TaskFactory factory = new TaskFactory(token);


            //    Task task = factory.StartNew(() =>
            //{
            //    int i = 0;
            //    while (true)
            //    {

            //        if (i > 5)
            //        {
            //            ctSource.Cancel();
            //            break;
            //        }

            //        Task.Delay(1 * 1000).Wait();

            //        Action action = () => { label1.Text = (++i).ToString(); };

            //        label1.Invoke(action);
            //    }

            //}, token);












            //}
            //catch (TaskCanceledException ex)
            //{

            //    throw;
            //}


        }

        private void button2_Click(object sender, EventArgs e)
        {
            _cts.Cancel();

        }

        private void button3_Click(object sender, EventArgs e)
        {

            CancellationTokenSource ctSource3 = new CancellationTokenSource();

            Task.Run(() =>
            {
                {
                    try
                    {


                        int i = 0;
                        while (true)
                        {
                            if (i > 5)
                            {
                                ctSource3.Cancel();
                                ctSource3.Token.ThrowIfCancellationRequested();
                            }

                            Task.Delay(1 * 1000).Wait();

                            Action action = () => { label1.Text = (++i).ToString(); };

                            label1.Invoke(action);
                        }

                    }
                    catch (OperationCanceledException ex)
                    {

                        //取消异常捕获
                    }

                }
            });


        }
    }
}
