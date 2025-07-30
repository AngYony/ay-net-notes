






using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AY.CommunicationLib
{
    #region �򵥵Ļ����
    /// <summary>
    /// һ���򵥵Ļ���߳�ͬ�����������˻�Ԫ�û��ӻ�Ԫ�ں�ͬ������ʵ��
    /// </summary>

    public sealed class SimpleHybirdLock : IDisposable
    {

        #region IDisposable Support
        private bool disposedValue = false; // Ҫ����������

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: �ͷ��й�״̬(�йܶ���)��
                }

                // TODO: �ͷ�δ�йܵ���Դ(δ�йܵĶ���)������������������ս�����
                // TODO: �������ֶ�����Ϊ null��
                m_waiterLock.Close();

                disposedValue = true;
            }
        }

        // TODO: �������� Dispose(bool disposing) ӵ�������ͷ�δ�й���Դ�Ĵ���ʱ������ս�����
        // ~SimpleHybirdLock() {
        //   // ������Ĵ˴��롣���������������� Dispose(bool disposing) �С�
        //   Dispose(false);
        // }

        // ��Ӵ˴�������ȷʵ�ֿɴ���ģʽ��
        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        public void Dispose()
        {
            // ������Ĵ˴��롣���������������� Dispose(bool disposing) �С�
            Dispose(true);
            // TODO: ���������������������ս�������ȡ��ע�������С�
            // GC.SuppressFinalize(this);
        }
        #endregion

        /// <summary>
        /// ��Ԫ�û�ģʽ����ͬ����
        /// </summary>
        private Int32 m_waiters = 0;
        /// <summary>
        /// ��Ԫ�ں�ģʽ����ͬ����
        /// </summary>
        private AutoResetEvent m_waiterLock = new AutoResetEvent(false);

        /// <summary>
        /// ��ȡ��
        /// </summary>
        public void Enter()
        {
            if (Interlocked.Increment(ref m_waiters) == 1) return;//�û�������ʹ�õ�ʱ��ֱ�ӷ��أ���һ�ε���ʱ����
            //������������ʱ��ʹ���ں�ͬ��������
            m_waiterLock.WaitOne();
        }

        /// <summary>
        /// �뿪��
        /// </summary>
        public void Leave()
        {
            if (Interlocked.Decrement(ref m_waiters) == 0) return;//û�п��õ�����ʱ��
            m_waiterLock.Set();
        }

        /// <summary>
        /// ��ȡ��ǰ���Ƿ��ڵȴ�����
        /// </summary>
        public bool IsWaitting => m_waiters == 0;
    }
    #endregion
}
