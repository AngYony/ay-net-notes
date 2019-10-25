using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Safety_Sample
{
    internal class SecurityDemo
    {
        /*
         * 如何从文件中读取访问控制列表（访问权限）
         */

        public static void Run(string[] args)
        {
            string filename = null;
            if (args.Length == 0)
            {
                return;
            }

            filename = args[0];

            using (FileStream stream = File.Open(filename, FileMode.Open))
            {
                //获取文件的访问控制列表（ACL）
                FileSecurity securityDescriptor = stream.GetAccessControl();
                //返回DACL
                /*
                 * GetAccessRules方法可以确定是否应使用继承的访问规则，
                 * 最后一个参数定义了应返回的安全标识符的类型，可能的类型有NTAccount和SecurityIdentifier,
                 * 这两个类都表示用户或组
                 * NTAccount类按名称查找安全对象
                 * SecurityIdentifier类按唯一的安全标识符查找安全对象
                 */
                AuthorizationRuleCollection rules = securityDescriptor.GetAccessRules(true, true, typeof(NTAccount));
                //返回SACL
                //securityDescriptor.GetAuditRules();
                //AuthorizationRule对象是ACE的.NET表示
                foreach (AuthorizationRule rule in rules)
                {
                    FileSystemAccessRule fileRule = rule as FileSystemAccessRule;
                    Console.WriteLine("Access type:" + fileRule.AccessControlType);
                    Console.WriteLine("Rights:" + fileRule.FileSystemRights);
                    Console.WriteLine("Identity:" + fileRule.IdentityReference.Value);
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// 设置访问权限
        /// </summary>
        /// <param name="filename"></param>
        private void WriteAcl(string filename)
        {
            NTAccount salesIdentity = new NTAccount("Sales");
            NTAccount developersIdentity = new NTAccount("Developers");
            NTAccount everyOneIdentity = new NTAccount("Everyone");
            //拒绝Sales组写入访问权限
            FileSystemAccessRule salesAce = new FileSystemAccessRule(salesIdentity, FileSystemRights.Write, AccessControlType.Deny);
            //给Everyone组提供了读取访问权限
            FileSystemAccessRule everyoneAce = new FileSystemAccessRule(everyOneIdentity, FileSystemRights.Read, AccessControlType.Allow);
            //给Developers组提供了全部控制权限
            FileSystemAccessRule developersAce = new FileSystemAccessRule(developersIdentity, FileSystemRights.FullControl, AccessControlType.Allow);

            FileSecurity securityDescriptor = new FileSecurity();
            securityDescriptor.SetAccessRule(everyoneAce);
            securityDescriptor.SetAccessRule(developersAce);
            securityDescriptor.SetAccessRule(salesAce);

            File.SetAccessControl(filename, securityDescriptor);
        }
    }
}