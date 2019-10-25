using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safety_Sample
{
    class DataProtectionDemo
    {
        private const string readOption = "-r";
        private const string writeOption = "-w";
        private static readonly string[] options = { readOption, writeOption };

        static void Run(string[] args)
        {
            if (args.Length != 2 || args.Intersect(options).Count() != 1)
            {
                ShowUsage();
                return;
            }

            string fileName = args[1];
            MySafe safe = InitProtection();
            switch(args[0]){
                case writeOption:
                    Write(safe,fileName);
                    break;
                case readOption:
                    Read(safe, fileName);
                    break;
                default:
                    ShowUsage();
                    break;
            }
        }
        private static MySafe InitProtection()
        {
            throw new NotImplementedException();
        }



        private static void Read(MySafe safe, string fileName)
        {
            throw new NotImplementedException();
        }

        private static void Write(MySafe safe, string fileName)
        {
            throw new NotImplementedException();
        }

       

        private static void ShowUsage()
        {
            throw new NotImplementedException();
        }
    }


    public class MySafe
    {
        private IDataProtector _protector;
        public MySafe(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("MySafe.abc.def");
        }

        public string Encrypt(string input) => _protector.Protect(input);

        public string Decrypt(string encrypted) => _protector.Unprotect(encrypted);
    }
}
