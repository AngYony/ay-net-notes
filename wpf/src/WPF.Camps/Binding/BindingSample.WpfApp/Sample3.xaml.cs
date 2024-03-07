﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BindingSample.WpfApp
{
    /// <summary>
    /// Sample3.xaml 的交互逻辑
    /// </summary>
    public partial class Sample3 : Window
    {
        public Sample3()
        {
            InitializeComponent();
            this.DataContext = new Sample3Model();
        }
    }


    public class Sample3Model
    {
        public MyCommand ShowCommand { get; set; }

        public Sample3Model()
        {
            ShowCommand = new MyCommand(Show);
        }

        public void Show()
        {
            MessageBox.Show("点击了按钮！");
        }
    }

   public class MyCommand : ICommand
    {
        private readonly Action action;

        public MyCommand(Action action)
        {
            this.action = action;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }
    }
}
