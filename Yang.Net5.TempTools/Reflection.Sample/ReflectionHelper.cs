using AspectCore.Extensions.Reflection;
using Namotion.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Reflection.Sample
{
    public class ReflectionHelper
    {



        public async Task InvokeMethodAsync(string methodName, params object[] methodArgValue)
        {
            object businessInstance = await GetBusinessInstance(null);
            if (businessInstance == null) return;
            Type[] methodArgType = methodArgValue.Select(p => p.GetType()).ToArray();
            var currentMethod = businessInstance.GetType().GetMethod(methodName, methodArgType);
            //如果调用的是带有泛型类型的方法，使用：MakeGenericMethod()
            await (Task)currentMethod.Invoke(businessInstance, methodArgValue);
        }

        public async Task<T> InvokeMethodAsync<T>(string methodName, params object[] methodArgValue)
        {
            object businessInstance = await GetBusinessInstance(null);
            if (businessInstance == null) return default(T);
            Type[] methodArgType = methodArgValue.Select(p => p.GetType()).ToArray();
            var currentMethod = businessInstance.GetType().GetMethod(methodName, methodArgType);
            //如果调用的是带有泛型类型的方法，使用：MakeGenericMethod()
            return await (Task<T>)currentMethod.Invoke(businessInstance, methodArgValue);
        }


        public async Task<List<T>> InvokeMethodReturnListAsync<T>(string methodName, params object[] methodArgValue)
        {
            object businessInstance = await GetBusinessInstance(null);
            if (businessInstance == null) return default(List<T>);
            Type[] methodArgType = methodArgValue.Select(p => p.GetType()).ToArray();
            var currentMethod = businessInstance.GetType().GetMethod(methodName, methodArgType);
            //如果调用的是带有泛型类型的方法，使用：MakeGenericMethod()
            //比较类型： currentMethod.ReturnType.Equals(myType)
            dynamic task = currentMethod.Invoke(businessInstance, methodArgValue);
            List<T> result = (List<T>)(await task);
            return result;
        }


        //根据Type类型完整名称获取Type
        private Type GetTypeByTypeFullName(Type assemblyType, string typeFullName)
        {
            Assembly assembly = assemblyType.Assembly;
            var resultType = assembly.GetType(typeFullName);
            if (resultType == null)
            {
                throw new Exception("无效的类型名称：" + typeFullName);
            }
            return resultType;
        }
        //根据Type类型名称获取Type
        private Type GetTypeByTypeName(Type assemblyType, string typeName)
        {
            Type resultType = assemblyType.Assembly.GetExportedTypes()
            .FirstOrDefault(t => string.Equals(t.GetReflector().Name, typeName, StringComparison.InvariantCultureIgnoreCase));
            if (resultType == null)
            {
                throw new Exception("无效的类型名称：" + typeName);
            }
            return resultType;
        }

        public async Task<object> SaveAsync(string methodName, string entityFullName, string iBusinessFullName, IDictionary<string, string>[] data)
        {
            Type entityClassType = GetTypeByTypeFullName(typeof(BaseEntity), entityFullName);
            Type iBusinessType = GetTypeByTypeFullName(typeof(IWyMetadata<>), iBusinessFullName);

            //根据实体类型创建对应的泛型集合，并返回该集合的类型
            var (listType, values) = CreateGenericTypeListInstance(entityClassType);
            foreach (var item in data)
            {
                //生成实体对象
                object entityObj = System.Activator.CreateInstance(entityClassType);
                //根据键值对传入的数据，对实体进行赋值
                foreach (string propertyName in item.Keys)
                {
                    if (entityObj.HasProperty(propertyName))
                    {
                        var propertyInfo = entityClassType.GetProperty(propertyName);
                        //todo:类型转换方法
                        var newValue = ConvertData(propertyInfo.PropertyType, item[propertyName], out string message);
                        if (!string.IsNullOrWhiteSpace(message))
                        {
                            throw new Exception(message);
                        }

                        try
                        {
                            //给实体对象的属性进行赋值
                            propertyInfo.SetValue(entityObj, newValue);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"属性{propertyInfo.Name}赋值失败，类型：{propertyInfo.PropertyType.Name}，接收值：{item[propertyName]}");
                        }
                    }
                    else
                    {
                        throw new Exception($"属性不匹配：{entityFullName}中不包含属性{propertyName}");
                    }
                }
                //将实体添加到生成的集合中
                AddGenericTypeList(listType, values, entityObj);
            }
            //todo:调用写入方法，传入实体集合
            return values;
           
        }

        private object ConvertData(Type type, object value, out string message)
        {
            message = string.Empty;
            //无论value是否有值，都先转换为string，在做判断
            string srcValue = Convert.ToString(value);
            bool hasValue = !string.IsNullOrWhiteSpace(srcValue);
            if (hasValue)
            {
                if (type.Equals(typeof(int)) || type.Equals(typeof(int?)))
                {
                    if (int.TryParse(srcValue, out int result))
                    {
                        return result;
                    }
                }
                else if (type.Equals(typeof(decimal)) || type.Equals(typeof(decimal?)))
                {
                    if (decimal.TryParse(srcValue, out decimal result))
                    {
                        return result;
                    }
                }
            }
            else
            {
                //如果是泛型并且是可以为null的泛型，没有值的情况下，直接返回null
                if (type.IsGenericType && Type.Equals(type.GetGenericTypeDefinition(), typeof(Nullable<>)))
                {
                    return null;
                }

                if (type.Equals(typeof(int)))
                    return default(int);
                else if (type.Equals(typeof(decimal)))
                    return default(decimal);
            }

            message = $"数据类型不匹配，目标类型：{type.FullName}传入的值：{value}";
            return null;
        }



        //生成指定Type的List泛型集合
        private (Type, object) CreateGenericTypeListInstance(Type entityClassType)
        {
            var listType = typeof(List<>).MakeGenericType(entityClassType);
            object values = System.Activator.CreateInstance(listType);
            return (listType, values);
        }


        //向泛型集合中添加元素
        private void AddGenericTypeList(Type listType, object list, object itemValue)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.Public;
            MethodInfo method = listType.GetMethod("Add", flag);
            method.Invoke(list, new object[] { itemValue });
        }


        // 根据泛型类型名称获取具体实现的接口的实例
        private async Task<object> GetBusinessInstance(IServiceProvider _Provider)
        {
            string entityName = nameof(Student);
            Type currentEntityType = GetTypeByTypeName(typeof(BaseEntity), entityName);

            //动态创建泛型类型
            var genericEntityType = typeof(IWyMetadata<>).MakeGenericType(currentEntityType);

            //获取所有实现了IWyMetadata<T>的接口
            var currentIBusinessType = typeof(IStudentBusiness).Assembly.GetExportedTypes()
            .FirstOrDefault(t => t.IsInterface && t.IsAssignableTo(genericEntityType));// t.GetTypeInfo().ImplementedInterfaces.Any(i => i.Equals(genericEntityType)));
            object businessInstance = null;
            if (currentIBusinessType != null)
            {
                businessInstance = _Provider.GetService(currentIBusinessType);
            }
            return businessInstance;

            // 如果要获取泛型类型
            //currentIBusinessType.GenericTypeArguments[0];
        }
    }


    public class BaseEntity
    {

    }


    public class Student : BaseEntity
    {
        public string Name { get; set; }
    }

    public interface IStudentBusiness : IWyMetadata<Student>
    {
    }
    public class StudentBusiness : IStudentBusiness
    {
        public async Task<List<Student>> GetListAsync()
        {
            var data = new List<Student>() { new Student { Name = "张三" } };
            return await Task.FromResult(data);
        }
    }

    public interface IWyMetadata<T> where T : class
    {
        Task<List<T>> GetListAsync();
    }


}
