using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WhatsNewAttributes;

namespace LookUpWhatsNew
{
    class Program
    {
        private static readonly StringBuilder outputText = new StringBuilder(1000);
        private static DateTime backDateTo = new DateTime(2018, 8, 21);

        static void Main(string[] args)
        {
            Assembly theAssembly = Assembly.Load(new AssemblyName("VectorClass"));
            Attribute supprotsAttribute = theAssembly.GetCustomAttribute(typeof(SupportsWhatsNewAttribute));
            string name = theAssembly.FullName;
            AddToMessage("Assembly:" + name);
            //加载VectorClass程序集，验证它是否使用了SupportsWhatsNew特性标记
            //注意，在Vector类中，命名空间之前使用了[assembly: SupportsWhatsNew]标记
            if (supprotsAttribute == null)
            {
                AddToMessage("This assembly does not supprots whatsNew attributes");
                return;
            }
            else
            {
                AddToMessage("Defined Type:");
            }
            //获取该程序集中定义的所有类型
            IEnumerable<Type> types = theAssembly.ExportedTypes;
            foreach(Type definedType in types )
            {
                DisplayTypeInfo(definedType);
            }
            Console.WriteLine($"What\'s new since {backDateTo:D}");
            Console.WriteLine(outputText.ToString());
            Console.Read();
        }

        private static void DisplayTypeInfo(Type definedType)
        {
            if (definedType.GetTypeInfo().IsClass)
            {
                return;
            }
            AddToMessage("\nclass " + definedType.Name);
            //根据LastModifiedAttribute进行筛选
            IEnumerable<LastModifiedAttribute> attributes = definedType.GetTypeInfo()
                .GetCustomAttributes().OfType<LastModifiedAttribute>();

            if (attributes.Count() == 0)
            {
                AddToMessage("No changes to this class \n");
            }
            else
            {
                foreach(LastModifiedAttribute attribute in attributes)
                {
                    WriteAttributeInfo(attribute);
                }
            }

            AddToMessage("chanage to methods of this class:");
            //遍历该类型的所有成员方法
            foreach(MethodInfo method in definedType.GetTypeInfo().DeclaredMembers.OfType<MethodInfo>())
            {
                
                IEnumerable<LastModifiedAttribute> attributesToMethods = method.GetCustomAttributes()
                    .OfType<LastModifiedAttribute>();
                
                if(attributesToMethods.Count()>0)
                {
                    AddToOutput($"{method.ReturnType} {method.Name}()");
                    foreach(LastModifiedAttribute attribute in attributesToMethods)
                    {
                        WriteAttributeInfo(attribute);
                    }
                }
            }
        }

        private static void AddToOutput(string v)
        {
            outputText.Append("\n" + v);
        }

        private static void WriteAttributeInfo(LastModifiedAttribute attribute)
        {
            LastModifiedAttribute lastModifiedAttributeb = attribute as LastModifiedAttribute;
            if (lastModifiedAttributeb == null)
            {
                return;
            }

            DateTime modifiedDate = lastModifiedAttributeb.DateModified;
            if (modifiedDate < backDateTo)
            {
                return;
            }
            AddToOutput($"modified:{modifiedDate:D} :{lastModifiedAttributeb.Changes}");
            if (lastModifiedAttributeb.Issues != null)
            {
                AddToOutput($"Outstanding issues:{lastModifiedAttributeb.Issues}");
            }
        }

        private static void AddToMessage(string v)
        {
            Console.WriteLine(v);
        }
    }
}
