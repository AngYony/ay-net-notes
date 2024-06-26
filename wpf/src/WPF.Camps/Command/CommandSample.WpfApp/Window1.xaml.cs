﻿using System.Windows;
using System.Windows.Input;

namespace CommandSample.WpfApp
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(this.nameTextBox.Text);
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string name = this.nameTextBox.Text;
            if (e.Parameter.ToString() == "Teacher")
            {
                this.listBoxNewItems.Items.Add("Teacher:" + name);
            }
            if (e.Parameter.ToString() == "Student")
            {
                this.listBoxNewItems.Items.Add("Student:" + name);
            }
        }
    }
}