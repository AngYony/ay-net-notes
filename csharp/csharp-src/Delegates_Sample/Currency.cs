using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates_Sample
{
    //定义一个结构，属值类型
    struct Currency
    {
        public uint Dollars;
        public ushort Cents;
        public Currency(uint dollars,ushort cents)
        {
            this.Dollars = dollars;
            this.Cents = cents;
        }
        public override string ToString()
        {
            
            
            return $"${Dollars}.{Cents}"; 
        }

        public static string GetCurrencyUnit() => "Dollar";

        public static explicit operator Currency(float value)
        {
            checked
            {
                uint dollars = (uint)value;
                ushort cents = (ushort)((value - dollars) * 100);
                return new Currency(dollars, cents);

            }
        }

        public static implicit operator float(Currency value)=> value.Dollars+(value.Cents/100.0f);

        public static implicit operator Currency(uint value)=>new Currency(value,0);

        public static implicit operator uint(Currency value)=>value.Dollars;

    }

    public class Currency_Program
    {
        private delegate string GetAString();
        public static void Run()
        {
            #region 验证委托可以指定不同实例上引用的不同方法，不用考虑方法是否静态，只要方法的签名匹配委托定义即可
            int x = 40;
            GetAString firstStringMethod = x.ToString;
            Console.WriteLine($"string is {firstStringMethod()}");

            var balance = new Currency(34, 1);
            firstStringMethod = balance.ToString;
            Console.WriteLine($"string is {firstStringMethod()}");

            firstStringMethod = new GetAString(Currency.GetCurrencyUnit);
            Console.WriteLine($"string is {firstStringMethod()}");
            #endregion
        }
    }
}
