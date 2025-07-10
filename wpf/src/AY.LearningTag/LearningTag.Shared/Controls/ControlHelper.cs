using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LearningTag.Shared.Controls
{
    public class ControlHelper
    {
        public static T FindControl<T>(System.Windows.DependencyObject parent) where T : System.Windows.DependencyObject
        {
            if (parent == null) return null;
            for (int i = 0; i < System.Windows.Media.VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = System.Windows.Media.VisualTreeHelper.GetChild(parent, i);
                if (child is T control)
                {
                    return control;
                }
                var result = FindControl<T>(child);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

         

        public static T FindControlByChild<T>(DependencyObject child) where T : DependencyObject
        {
            if (child == null) return null;

            var parent = VisualTreeHelper.GetParent(child);
            if (parent != null)
            {
                var p = parent as T;
                if (p != null)
                {
                    return p;
                }
                return FindControlByChild<T>(parent);

            }
            return null;
        }



        public static List<T> FindControls<T>(System.Windows.DependencyObject parent) where T : System.Windows.DependencyObject
        {
            List<T> controls = new List<T>();
            if (parent == null) return controls;
            for (int i = 0; i < System.Windows.Media.VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = System.Windows.Media.VisualTreeHelper.GetChild(parent, i);
                if (child is T control)
                {
                    controls.Add(control);
                }
                controls.AddRange(FindControls<T>(child));
            }
            return controls;
        }

        public static void SetControlVisibility(System.Windows.DependencyObject parent, System.Windows.Visibility visibility)
        {
            if (parent == null) return;
            for (int i = 0; i < System.Windows.Media.VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = System.Windows.Media.VisualTreeHelper.GetChild(parent, i);
                if (child is System.Windows.UIElement element)
                {
                    element.Visibility = visibility;
                }
                SetControlVisibility(child, visibility);
            }
        }

        public static void SetControlEnabled(System.Windows.DependencyObject parent, bool isEnabled)
        {
            if (parent == null) return;
            for (int i = 0; i < System.Windows.Media.VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = System.Windows.Media.VisualTreeHelper.GetChild(parent, i);
                if (child is System.Windows.UIElement element)
                {
                    element.IsEnabled = isEnabled;
                }
                SetControlEnabled(child, isEnabled);
            }
        }
         
        
    }
}
