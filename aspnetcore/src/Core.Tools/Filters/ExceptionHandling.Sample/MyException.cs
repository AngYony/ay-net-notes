using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling.Sample
{
    public class MyException : Exception
    {

        public MyException(string? message, Exception innerException) : base(message, innerException)
        {
            
        }

        //protected MyException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{ 
        
        
        //}


        //[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        //public override void GetObjectData(SerializationInfo info,
        // StreamingContext context)
        //{
        //    // Change the case of two properties, and then use the
        //    // method of the base class.
        //    HelpLink = HelpLink.ToLower();
        //    Source = Source.ToUpperInvariant();

        //    base.GetObjectData(info, context);
        //}
    }


}
