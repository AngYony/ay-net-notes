using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AY.Shared.Core
{
    /// <summary>
    /// 当ObservableCollection在非UI线程修改时，自动切换到UI线程进行修改
    /// ObservableCollection 只负责集合修改通知，不负责对象内容修改通知（它不管集合元素内部的属性值），除非T实现了INotifyPropertyChanged
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SafeRangeObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        private readonly Dispatcher dispatcher;

        public SafeRangeObservableCollection()
        {
            dispatcher = Dispatcher.CurrentDispatcher;
        }

        private void Invoke(Action action)
        {
            if (dispatcher.CheckAccess())
                action();
            else
                dispatcher.Invoke(action);
        }

        public void AddRange(IEnumerable<T> items)
        {
            Invoke(() =>
            {
                foreach (var item in items)
                    Items.Add(item);

                OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Reset
                ));
            });
        }

        public void RemoveRange(IEnumerable<T> items)
        {
            Invoke(() =>
            {
                foreach (var item in items)
                    Items.Remove(item);

                OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Reset
                ));
            });
        }

        protected override void InsertItem(int index, T item)
            => Invoke(() => base.InsertItem(index, item));

        protected override void RemoveItem(int index)
            => Invoke(() => base.RemoveItem(index));

        protected override void ClearItems()
            => Invoke(() => base.ClearItems());

        protected override void SetItem(int index, T item)
            => Invoke(() => base.SetItem(index, item));
    }



    /* 用法：
     * 
     * public SafeRangeObservableCollection<BrowserGroup> BrowserGroups { get; }
     * = new SafeRangeObservableCollection<BrowserGroup>();
     * // 后台线程直接调用，无需 Dispatcher
     * Task.Run(() =>
     * {
     *     BrowserGroups.Add(new BrowserGroup());
     * });
     * // 批量添加
     * BrowserGroups.AddRange(bigList);
     */
}
