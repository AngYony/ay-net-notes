using System;
using System.Collections.ObjectModel;

namespace SpecialCollections_Sample
{
    internal class ObservableDemo
    {
        public static void Run()
        {
            var data = new ObservableCollection<string>();
            data.CollectionChanged += Data_CollectionChanged;
            data.Add("one");
            data.Add("tow");
            data.Insert(1, "Three");
            data.Remove("one");
        }

        private static void Data_CollectionChanged(object sender,
            System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("action" + e.Action.ToString());

            if (e.OldItems != null)
            {
                Console.WriteLine("OldStartingIndex:" + e.OldStartingIndex);
                Console.WriteLine("old item(s):");
                foreach (var item in e.OldItems)
                {
                    Console.WriteLine(item);
                }
            }
            if (e.NewItems != null)
            {
                Console.WriteLine("NewStartingIndex:" + e.NewStartingIndex);
                Console.WriteLine("new items:");
                foreach (var item in e.NewItems)
                {
                    Console.WriteLine(item);
                }
            }
            Console.WriteLine();
        }
    }
}