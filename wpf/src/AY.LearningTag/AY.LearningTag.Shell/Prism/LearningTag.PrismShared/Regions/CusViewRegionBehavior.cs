using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTag.PrismShared.Regions
{
    public class CusViewRegionBehavior : RegionBehavior
    {
        protected override void OnAttach()
        {
            Region.ActiveViews.CollectionChanged += (s, e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    foreach (var view in e.NewItems)
                    {
                        // 这里可以添加自定义的逻辑，比如记录日志等
                        Console.WriteLine($"View added: {view}");
                    }
                }
                else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                {
                    foreach (var view in e.OldItems)
                    {
                        // 这里可以添加自定义的逻辑，比如记录日志等
                        Console.WriteLine($"View removed: {view}");
                    }
                }
            };
        }
    }
}
