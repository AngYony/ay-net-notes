namespace Autofac.Sample.IocServices
{
    public interface IocTagScope
    {

    }
    public interface IocTagSington
    {

    }
    public interface IocTagTransient
    {

    }


    public interface IIocService :IocTagScope
    {
        string GetStr();
    }

    public class IocService : IIocService
    {
        string str = string.Empty;
        public IocService()
        {
            str= Guid.NewGuid().ToString();
        }
        public string GetStr()
        {
            return str;
        }
    }
}
