using System;
using System.Collections.Generic;

namespace Generics_Sample
{
    public interface IAccount
    {
        decimal Balance { get; }
        string Name { get; }
    }

    //静态类不能被实例化
    public static class Algorithms
    {
        public static decimal Accumulate<TAccount>(IEnumerable<TAccount> source)
            where TAccount : IAccount
        {
            decimal sum = 0;
            foreach (TAccount a in source)
            {
                sum += a.Balance;
            }
            return sum;
        }

        public static TSum Accumulate<TAccount, TSum>(IEnumerable<TAccount> source,
            Func<TAccount, TSum, TSum> action
            ) where TAccount : IAccount where TSum : struct
        {
            TSum sum = default(TSum);
            foreach (TAccount item in source)
            {
                sum = action(item, sum);
            }
            return sum;
        }

        public static decimal AccumulateSimple(IEnumerable<Account> source)
        {
            decimal sum = 0;
            foreach (Account a in source)
            {
                sum += a.Balance;
            }
            return sum;
        }
    }

    public class Account : IAccount
    {
        public Account(string name, decimal balance)
        {
            Name = name;
            Balance = balance;
        }

        public decimal Balance { get; private set; }
        public string Name { get; }
    }
}