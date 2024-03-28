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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BindingSample.WpfApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Sample1().ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new Sample2().ShowDialog(); 
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new Sample3().ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new Sample4().ShowDialog();
        }

        private void Button_Click_2_2(object sender, RoutedEventArgs e)
        {
            new Sample2_2().ShowDialog();
        }
    }
}