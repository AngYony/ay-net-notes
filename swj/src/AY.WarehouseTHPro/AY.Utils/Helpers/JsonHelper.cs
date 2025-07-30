using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.Utils
{
    public class JsonHelper
    {
        public static string SerializeObject<T>(T obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T DeserializeObject<T>(string info)
        {
            try
            {
                return (T)JsonConvert.DeserializeObject(info, typeof(T));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T DeepCopy<T>(T obj)
        {
            return DeserializeObject<T>(SerializeObject(obj));
        }
    }
}
