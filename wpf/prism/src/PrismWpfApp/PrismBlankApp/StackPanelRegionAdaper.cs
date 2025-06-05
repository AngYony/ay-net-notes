using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PrismBlankApp
{
    public class StackPanelRegionAdaper : RegionAdapterBase<StackPanel>
    {
        public StackPanelRegionAdaper(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, StackPanel regionTarget)
        {
            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    foreach(FrameworkElement item in e.NewItems)
                    {
                        regionTarget.Children.Add(item);
                    }
                }

            };
        }

        protected override IRegion CreateRegion()
        {
            return new Region();
        }
    }
}
