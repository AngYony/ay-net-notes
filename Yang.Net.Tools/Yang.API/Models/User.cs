namespace Yang.API.Models
{
    public class User : IUserService
    {

        // 使用属性注入而不是构造函数注入，注意由于先执行构造函数，其次才执行属性的声明，因此不能在构造函数中使用属性，这也是属性注入的局限性
                public IStudentService StudentService { get; set; }   

        public string Name { get; set; }
        public User()
        {
            // 由于注入服务时，指明了允许属性注入，因此此处不需要公共构造函数进行赋值既可使用StudentService
            Name = $"User:{Guid.NewGuid().ToString()}";
        }

        public string GetCode(){
            return  StudentService.Code; 
        }
    }
}
