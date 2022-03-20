namespace Yang.Shape
{

    /// <summary>
    /// 如何封装带有泛型参数和实际类型的方法
    /// </summary>
    public class Class1
    {
        public string GetMyType<T>() where T:class
        {
            return GetMyType(typeof(T));
        }

        public string GetMyType(Type type){
            return type.Name;
        }


        public void Run(){
            // 执行上述方法
            this.GetMyType<object>();
            //或者
            this.GetMyType(typeof(string));

           
        }
    }
}