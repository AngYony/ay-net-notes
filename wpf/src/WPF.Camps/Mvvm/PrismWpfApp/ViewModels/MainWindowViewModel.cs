using PrismWpfApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrismWpfApp.ViewModels
{
    public class MainWindowViewModel : NotificationObject
    {
        private double _input1;

        public double Input1
        {
            get { return _input1; }
            set
            {
                _input1 = value;
                this.RaisePropertyChanged(nameof(Input1));
            }
        }

        private double _input2;

        public double Input2
        {
            get { return _input2; }
            set
            {
                _input2 = value;
                this.RaisePropertyChanged(nameof(Input2));
            }
        }

        private double _result;

        public double Result
        {
            get { return _result; }
            set
            {
                _result = value;
                this.RaisePropertyChanged(nameof(Result));
            }
        }

        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }

        private void Add(object parameter)
        {
            this.Result = this.Input1 + this.Input2;
        }

        public MainWindowViewModel()
        {
            this.AddCommand = new DelegateCommand();
            this.AddCommand.ExecuteAction = new Action<object>(Add);

            this.SaveCommand = new DelegateCommand()
            {
                ExecuteAction = new Action<object>(Save)
            };


        }

        private void Save(object parameter)
        {
            MessageBox.Show("保存成功");
        }
    }
}
