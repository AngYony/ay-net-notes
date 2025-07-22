using System;

namespace xbd.s7netplus
{
//#if NET_FULL
//    [Serializable]
//#endif

    /// <summary>
    /// 
    /// </summary>
    public class PlcException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public ErrorCode ErrorCode { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        public PlcException(ErrorCode errorCode) : this(errorCode, $"PLC communication failed with error '{errorCode}'.")
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="innerException"></param>
        public PlcException(ErrorCode errorCode, Exception innerException) : this(errorCode, innerException.Message,
            innerException)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        public PlcException(ErrorCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public PlcException(ErrorCode errorCode, string message, Exception inner) : base(message, inner)
        {
            ErrorCode = errorCode;
        }

#if NET_FULL

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected PlcException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
            ErrorCode = (ErrorCode)info.GetInt32(nameof(ErrorCode));
        }
#endif
    }
}