using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace SpecialCollections_Sample
{
    internal class Account
    {
        public string Name { get; }
        public decimal Amount { get; }

        public Account(string name, decimal amount)
        {
            this.Name = name;
            this.Amount = amount;
        }
    }

    internal class Account_Program
    {
        public static void Run()
        {
            var accounts = new List<Account>
            {
                new Account("图书",424.2m),
                new Account("文具",1243.5m),
                new Account("篮球",243.3m)
            };

            ImmutableList<Account> immutableAccounts = accounts.ToImmutableList();

            immutableAccounts.ForEach(a => Console.WriteLine(a.Name + "--" + a.Amount));
        }

        public static void Run2()
        {
            //使用构建器和不变的集合

            var accounts = new List<Account>
            {
                new Account("图书",424.2m),
                new Account("文具",1243.5m),
                new Account("篮球",243.3m)
            };

            ImmutableList<Account> immutableAccounts = accounts.ToImmutableList();
            ImmutableList<Account>.Builder builder = immutableAccounts.ToBuilder();
            for (int i = 0; i < builder.Count; i++)
            {
                Account a = builder[i];
                if (a.Amount > 1000)
                {
                    builder.Remove(a);
                }
            }

            ImmutableList<Account> overdrawnAccounts = builder.ToImmutable();
            overdrawnAccounts.ForEach(b => Console.WriteLine(b.Name + "=" + b.Amount));
        }
    }
}