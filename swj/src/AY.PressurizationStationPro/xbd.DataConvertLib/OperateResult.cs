using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xbd.DataConvertLib
{
    /// <summary>
    /// 操作结果类
    /// </summary>
    [Description("操作结果类")]
    public class OperateResult
    {
        /// <summary>
        /// 结果是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 错误描述
        /// </summary>
        public string Message { get; set; } = "UnKnown";
        /// <summary>
        /// 错误代号
        /// </summary>
        public int ErrorCode { get; set; } = 99999;

        public OperateResult()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isSuccess">操作是否成功</param>
        public OperateResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isSuccess">操作是否成功</param>
        /// <param name="message">结果信息</param>
        public OperateResult(bool isSuccess, string message)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isSuccess">操作是否成功</param>
        /// <param name="errorCode">错误代码</param>
        /// <param name="message">结果信息</param>
        public OperateResult(bool isSuccess, int errorCode, string message)
        {
            this.IsSuccess = isSuccess;
            this.ErrorCode = errorCode;
            this.Message = message;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">结果信息</param>
        public OperateResult(string message)
        {
            this.Message = message;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        /// <param name="message">结果信息</param>
        public OperateResult(int errorCode, string message)
        {
            this.ErrorCode = errorCode;
            this.Message = message;
        }



        /// <summary>
        /// 创建一个操作成功结果
        /// </summary>
        /// <returns></returns>
        public static OperateResult CreateSuccessResult()
        {
            return new OperateResult(true, 0, "Success");
        }

        /// <summary>
        /// 创建一个操作失败结果，带结果信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static OperateResult CreateFailResult(string message)
        {
            return new OperateResult(false, 99999, message);
        }

        /// <summary>
        /// 创建一个操作失败结果，不带结果信息
        /// </summary>
        /// <returns></returns>
        public static OperateResult CreateFailResult()
        {
            return new OperateResult(false, 99999, "UnKnown");
        }

        /// <summary>
        /// 创建带一个数据的操作成功结果
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">值</param>
        /// <returns>带一个数据的操作结果</returns>
        public static OperateResult<T> CreateSuccessResult<T>(T value)
        {
            return new OperateResult<T>(true, 0, "Success", value);
        }

        /// <summary>
        /// 创建带一个数据的操作失败结果
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="result">操作结果对象</param>
        /// <returns>带一个数据的操作结果</returns>
        public static OperateResult<T> CreateFailResult<T>(OperateResult result)
        {
            return new OperateResult<T>(false, result.ErrorCode, result.Message);
        }

        /// <summary>
        /// 创建带一个数据的操作失败结果
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="message">错误信息</param>
        /// <returns>带一个数据的操作结果</returns>
        public static OperateResult<T> CreateFailResult<T>(string message)
        {
            return new OperateResult<T>(false, 99999, message);
        }

        /// <summary>
        /// 创建带二个数据的操作成功结果
        /// </summary>
        /// <typeparam name="T1">类型1</typeparam>
        /// <typeparam name="T2">类型2</typeparam>
        /// <param name="value1">值1</param>
        /// <param name="value2">值2</param>
        /// <returns>带二个数据的操作结果</returns>
        public static OperateResult<T1, T2> CreateSuccessResult<T1, T2>(T1 value1, T2 value2)
        {
            return new OperateResult<T1, T2>(true, 0, "", value1, value2);
        }

        /// <summary>
        /// 创建带二个数据的操作失败结果
        /// </summary>
        /// <typeparam name="T1">类型1</typeparam>
        /// <typeparam name="T2">类型2</typeparam>
        /// <param name="result">操作结果</param>
        /// <returns>带二个数据的操作结果</returns>
        public static OperateResult<T1, T2> CreateFailResult<T1, T2>(OperateResult result)
        {
            return new OperateResult<T1, T2>(false, result.ErrorCode, result.Message);
        }

        /// <summary>
        /// 创建带二个数据的操作失败结果
        /// </summary>
        /// <typeparam name="T1">类型1</typeparam>
        /// <typeparam name="T2">类型2</typeparam>
        /// <param name="message">错误信息</param>
        /// <returns>带二个数据的操作结果</returns>
        public static OperateResult<T1, T2> CreateFailResult<T1, T2>(string message)
        {
            return new OperateResult<T1, T2>(false, 99999, message);
        }


        /// <summary>
        /// 创建带三个数据的操作成功结果
        /// </summary>
        /// <typeparam name="T1">类型1</typeparam>
        /// <typeparam name="T2">类型2</typeparam>
        /// <typeparam name="T3">类型3</typeparam>
        /// <param name="value1">值1</param>
        /// <param name="value2">值2</param>
        /// <param name="value3">值3</param>
        /// <returns>带一个数据的操作结果</returns>
        public static OperateResult<T1, T2, T3> CreateSuccessResult<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
        {
            return new OperateResult<T1, T2, T3>(true, 0, "Success", value1, value2, value3);
        }

        /// <summary>
        /// 创建带一个数据的操作成功结果
        /// </summary>
        /// <typeparam name="T1">类型1</typeparam>
        /// <typeparam name="T2">类型2</typeparam>
        /// <typeparam name="T3">类型3</typeparam>
        /// <param name="result">操作结果</param>
        /// <returns>带一个数据的操作结果</returns>
        public static OperateResult<T1, T2, T3> CreateFailResult<T1, T2, T3>(OperateResult result)
        {
            return new OperateResult<T1, T2, T3>(false, result.ErrorCode, result.Message);
        }

        /// <summary>
        /// 创建带一个数据的操作成功结果
        /// </summary>
        /// <typeparam name="T1">类型1</typeparam>
        /// <typeparam name="T2">类型2</typeparam>
        /// <typeparam name="T3">类型3</typeparam>
        /// <param name="message">错误信息</param>
        /// <returns>带一个数据的操作结果</returns>
        public static OperateResult<T1, T2, T3> CreateFailResult<T1, T2, T3>(string message)
        {
            return new OperateResult<T1, T2, T3>(false, 99999, message);
        }

        /// <summary>
        /// 创建带一个数据的操作成功结果
        /// </summary>
        /// <typeparam name="T1">类型1</typeparam>
        /// <typeparam name="T2">类型2</typeparam>
        /// <typeparam name="T3">类型3</typeparam>
        /// <typeparam name="T4">类型4</typeparam>
        /// <param name="value1">值1</param>
        /// <param name="value2">值2</param>
        /// <param name="value3">值3</param>
        /// <param name="value4">值4</param>
        /// <returns>带一个数据的操作结果</returns>
        public static OperateResult<T1, T2, T3, T4> CreateSuccessResult<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4)
        {
            return new OperateResult<T1, T2, T3, T4>(true, 0, "Success", value1, value2, value3, value4);
        }


        /// <summary>
        /// 创建带一个数据的操作成功结果
        /// </summary>
        /// <typeparam name="T1">类型1</typeparam>
        /// <typeparam name="T2">类型2</typeparam>
        /// <typeparam name="T3">类型3</typeparam>
        /// <typeparam name="T4">类型4</typeparam>
        /// <param name="result">操作结果</param>
        /// <returns>带一个数据的操作结果</returns>
        public static OperateResult<T1, T2, T3, T4> CreateFailResult<T1, T2, T3, T4>(OperateResult result)
        {
            return new OperateResult<T1, T2, T3, T4>(false, result.ErrorCode, result.Message);
        }

        /// <summary>
        /// 创建带一个数据的操作成功结果
        /// </summary>
        /// <typeparam name="T1">类型1</typeparam>
        /// <typeparam name="T2">类型2</typeparam>
        /// <typeparam name="T3">类型3</typeparam>
        /// <typeparam name="T4">类型4</typeparam>
        /// <param name="message">错误信息</param>
        /// <returns>带一个数据的操作结果</returns>
        public static OperateResult<T1, T2, T3, T4> CreateFailResult<T1, T2, T3, T4>(string message)
        {
            return new OperateResult<T1, T2, T3, T4>(false, 99999, message);
        }

        /// <summary>
        /// 创建带一个数据的操作成功结果
        /// </summary>
        /// <typeparam name="T1">类型1</typeparam>
        /// <typeparam name="T2">类型2</typeparam>
        /// <typeparam name="T3">类型3</typeparam>
        /// <typeparam name="T4">类型4</typeparam>
        /// <typeparam name="T5">类型5</typeparam>
        /// <param name="value1">值1</param>
        /// <param name="value2">值2</param>
        /// <param name="value3">值3</param>
        /// <param name="value4">值4</param>
        /// <param name="value5">值5</param>
        /// <returns>带一个数据的操作结果</returns>
        public static OperateResult<T1, T2, T3, T4, T5> CreateSuccessResult<T1, T2, T3, T4, T5>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
        {
            return new OperateResult<T1, T2, T3, T4, T5>(true, 0, "Success", value1, value2, value3, value4, value5);
        }

        /// <summary>
        /// 创建带一个数据的操作成功结果
        /// </summary>
        /// <typeparam name="T1">类型1</typeparam>
        /// <typeparam name="T2">类型2</typeparam>
        /// <typeparam name="T3">类型3</typeparam>
        /// <typeparam name="T4">类型4</typeparam>
        /// <typeparam name="T5">类型5</typeparam>
        /// <param name="result">操作结果</param>
        /// <returns>带一个数据的操作结果</returns>
        public static OperateResult<T1, T2, T3, T4, T5> CreateFailResult<T1, T2, T3, T4, T5>(OperateResult result)
        {
            return new OperateResult<T1, T2, T3, T4, T5>(false, result.ErrorCode, result.Message);
        }

        /// <summary>
        /// 创建带一个数据的操作成功结果
        /// </summary>
        /// <typeparam name="T1">类型1</typeparam>
        /// <typeparam name="T2">类型2</typeparam>
        /// <typeparam name="T3">类型3</typeparam>
        /// <typeparam name="T4">类型4</typeparam>
        /// <typeparam name="T5">类型5</typeparam>
        /// <param name="message">错误信息</param>
        /// <returns>带一个数据的操作结果</returns>
        public static OperateResult<T1, T2, T3, T4, T5> CreateFailResult<T1, T2, T3, T4, T5>(string message)
        {
            return new OperateResult<T1, T2, T3, T4, T5>(false, 99999, message);
        }
    }

    /// <summary>
    /// 带一个数据的操作结果类
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class OperateResult<T> : OperateResult
    {
        /// <summary>
        ///  数据
        /// </summary>
        public T Content { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public OperateResult() : base()
        {

        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isSuccess">操作是否成功</param>
        public OperateResult(bool isSuccess) : base(isSuccess)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">错误信息</param>
        public OperateResult(string message) : base(message)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        /// <param name="message">错误信息</param>
        public OperateResult(int errorCode, string message) : base(errorCode, message)
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="errorCode">错误代码</param>
        /// <param name="message">错误信息</param>
        public OperateResult(bool isSuccess, int errorCode, string message) : base(isSuccess, errorCode, message)
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="errorCode">错误代码</param>
        /// <param name="message">错误信息</param>
        /// <param name="content">数据</param>
        public OperateResult(bool isSuccess, int errorCode, string message, T content) : base(isSuccess, errorCode, message)
        {
            this.Content = content;
        }



    }

    /// <summary>
    /// 带二个数据的操作结果类
    /// </summary>
    /// <typeparam name="T1">类型1</typeparam>
    /// <typeparam name="T2">类型2</typeparam>
    public class OperateResult<T1, T2> : OperateResult
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public OperateResult() : base()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isSuccess">操作是否成功</param>
        public OperateResult(bool isSuccess) : base(isSuccess)
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">错误信息</param>
        public OperateResult(string message) : base(message)
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        /// <param name="message">错误信息</param>
        public OperateResult(int errorCode, string message) : base(errorCode, message)
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="errorCode">错误代码</param>
        /// <param name="message">错误信息</param>
        public OperateResult(bool isSuccess, int errorCode, string message) : base(isSuccess, errorCode, message)
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="errorCode">错误代码</param>
        /// <param name="message">错误信息</param>
        /// <param name="content1">数据1</param>
        /// <param name="content2">数据2</param>
        public OperateResult(bool isSuccess, int errorCode, string message, T1 content1, T2 content2) : base(isSuccess, errorCode, message)
        {
            this.Content1 = content1;
            this.Content2 = content2;
        }

        /// <summary>
        /// 数据1
        /// </summary>
        public T1 Content1 { get; set; }

        /// <summary>
        /// 数据2
        /// </summary>
        public T2 Content2 { get; set; }

    }

    /// <summary>
    /// 带三个数据的操作结果类
    /// </summary>
    /// <typeparam name="T1">类型1</typeparam>
    /// <typeparam name="T2">类型2</typeparam>
    /// <typeparam name="T3">类型3</typeparam>
    public class OperateResult<T1, T2, T3> : OperateResult
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public OperateResult() : base()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isSuccess">操作是否成功</param>
        public OperateResult(bool isSuccess) : base(isSuccess)
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">错误信息</param>
        public OperateResult(string message) : base(message)
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        /// <param name="message">错误信息</param>
        public OperateResult(int errorCode, string message) : base(errorCode, message)
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="errorCode">错误代码</param>
        /// <param name="message">错误信息</param>
        public OperateResult(bool isSuccess, int errorCode, string message) : base(isSuccess, errorCode, message)
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="errorCode">错误代码</param>
        /// <param name="message">错误信息</param>
        /// <param name="content1">数据1</param>
        /// <param name="content2">数据2</param>
        /// <param name="content3">数据3</param>
        public OperateResult(bool isSuccess, int errorCode, string message, T1 content1, T2 content2, T3 content3) : base(isSuccess, errorCode, message)
        {
            this.Content1 = content1;
            this.Content2 = content2;
            this.Content3 = content3;
        }

        /// <summary>
        /// 数据1
        /// </summary>
        public T1 Content1 { get; set; }

        /// <summary>
        /// 数据2
        /// </summary>
        public T2 Content2 { get; set; }

        /// <summary>
        /// 数据3
        /// </summary>
        public T3 Content3 { get; set; }

    }


    /// <summary>
    /// 带四个数据的操作结果类
    /// </summary>
    /// <typeparam name="T1">类型1</typeparam>
    /// <typeparam name="T2">类型2</typeparam>
    /// <typeparam name="T3">类型3</typeparam>
    /// <typeparam name="T4">类型4</typeparam>
    public class OperateResult<T1, T2, T3, T4> : OperateResult
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public OperateResult() : base()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isSuccess">操作是否成功</param>
        public OperateResult(bool isSuccess) : base(isSuccess)
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">错误信息</param>
        public OperateResult(string message) : base(message)
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        /// <param name="message">错误信息</param>
        public OperateResult(int errorCode, string message) : base(errorCode, message)
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="errorCode">错误代码</param>
        /// <param name="message">错误信息</param>
        public OperateResult(bool isSuccess, int errorCode, string message) : base(isSuccess, errorCode, message)
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="errorCode">错误代码</param>
        /// <param name="message">错误信息</param>
        /// <param name="content1">数据1</param>
        /// <param name="content2">数据2</param>
        /// <param name="content3">数据3</param>
        /// <param name="content4">数据4</param>
        public OperateResult(bool isSuccess, int errorCode, string message, T1 content1, T2 content2, T3 content3, T4 content4) : base(isSuccess, errorCode, message)
        {
            this.Content1 = content1;
            this.Content2 = content2;
            this.Content3 = content3;
            this.Content4 = content4;
        }

        /// <summary>
        /// 数据1
        /// </summary>
        public T1 Content1 { get; set; }

        /// <summary>
        /// 数据2
        /// </summary>
        public T2 Content2 { get; set; }

        /// <summary>
        /// 数据3
        /// </summary>
        public T3 Content3 { get; set; }

        /// <summary>
        /// 数据4
        /// </summary>
        public T4 Content4 { get; set; }

    }

    /// <summary>
    /// 带五个数据的操作结果类
    /// </summary>
    /// <typeparam name="T1">类型1</typeparam>
    /// <typeparam name="T2">类型2</typeparam>
    /// <typeparam name="T3">类型3</typeparam>
    /// <typeparam name="T4">类型4</typeparam>
    /// <typeparam name="T5">类型5</typeparam>
    public class OperateResult<T1, T2, T3, T4, T5> : OperateResult
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public OperateResult() : base()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isSuccess">操作是否成功</param>
        public OperateResult(bool isSuccess) : base(isSuccess)
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message">错误信息</param>
        public OperateResult(string message) : base(message)
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        /// <param name="message">错误信息</param>
        public OperateResult(int errorCode, string message) : base(errorCode, message)
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="errorCode">错误代码</param>
        /// <param name="message">错误信息</param>
        public OperateResult(bool isSuccess, int errorCode, string message) : base(isSuccess, errorCode, message)
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="errorCode">错误代码</param>
        /// <param name="message">错误信息</param>
        /// <param name="content1">数据1</param>
        /// <param name="content2">数据2</param>
        /// <param name="content3">数据3</param>
        /// <param name="content4">数据4</param>
        /// <param name="content5">数据5</param>
        public OperateResult(bool isSuccess, int errorCode, string message, T1 content1, T2 content2, T3 content3, T4 content4, T5 content5) : base(isSuccess, errorCode, message)
        {
            this.Content1 = content1;
            this.Content2 = content2;
            this.Content3 = content3;
            this.Content4 = content4;
            this.Content5 = content5;
        }

        /// <summary>
        /// 数据1
        /// </summary>
        public T1 Content1 { get; set; }

        /// <summary>
        /// 数据2
        /// </summary>
        public T2 Content2 { get; set; }

        /// <summary>
        /// 数据3
        /// </summary>
        public T3 Content3 { get; set; }

        /// <summary>
        /// 数据4
        /// </summary>
        public T4 Content4 { get; set; }

        /// <summary>
        /// 数据5
        /// </summary>
        public T5 Content5 { get; set; }


    }

}
