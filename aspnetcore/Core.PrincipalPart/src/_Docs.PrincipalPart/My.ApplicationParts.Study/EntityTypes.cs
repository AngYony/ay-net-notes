using System.Collections.Generic;
using System.Reflection;

namespace My.ApplicationParts.Study
{
    public class EntityTypes
    {
        public static IReadOnlyList<TypeInfo> MyTypes = new List<TypeInfo>(){
            typeof(Student).GetTypeInfo(),
            typeof(Teacher).GetTypeInfo()
        };

        //模拟两个类型
        public class Student { }

        public class Teacher { }
    }
}