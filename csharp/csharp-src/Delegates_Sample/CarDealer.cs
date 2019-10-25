using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates_Sample
{
    public class CarInfoEventArgs : EventArgs
    {
        public string Car { get; }
        public CarInfoEventArgs(string car)
        {
            this.Car = car;
        }
    }
    public  class CarDealer
    {
        public event EventHandler<CarInfoEventArgs> NewCarInfo;

        public void NewCar(string car)
        {
            Console.WriteLine("调用NewCar()方法");
            NewCarInfo?.Invoke(this, new CarInfoEventArgs(car));
        }
    }

    public class Consumer
    {
        private string _name;
        public Consumer(string name)
        {
            _name = name;
        }
        public void NewCarIsHere(object sender,CarInfoEventArgs e)
        {
            Console.WriteLine($"{_name}:car {e.Car} is new");
        }
    }


    public class Consumer_Program
    {
        public static void Run()
        {
            var dealer = new CarDealer();

            var daniel = new Consumer("Daniel");
            dealer.NewCarInfo += daniel.NewCarIsHere;
            dealer.NewCar("Mercedes");

            var sebastion = new Consumer("Sebastian");
            dealer.NewCarInfo += sebastion.NewCarIsHere;
            dealer.NewCar("Ferrari");

            dealer.NewCarInfo -= sebastion.NewCarIsHere;
            dealer.NewCar("Red Bull Racing");
        }
    }
}
