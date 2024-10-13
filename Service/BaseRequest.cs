using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

//用于表示一个基本的 HTTP 请求
namespace Memo.Service
{
    /// <summary>
    /// 1. BaseRequest 类
    /// 作用：
    /// BaseRequest 类用于封装 HTTP 请求的基本信息，包括请求的方法、路由、内容类型和参数等。这使得创建和发送请求更加简洁和一致。
    /// 
    /// 2. 属性
    /// Method：
    /// 类型：Method
    /// 描述：表示 HTTP 请求的方法（例如 GET、POST、PUT、DELETE 等）。这是 RestSharp 库中的一个枚举类型，通常用于定义请求类型。
    /// 
    /// Route：
    /// 类型：string
    /// 描述：表示请求的路由或 URL 路径。它通常是 API 的端点，如 "/api/todos"。
    /// 
    /// ContentType：
    /// 类型：string
    /// 描述：表示请求的内容类型。默认值为 "application/json"，这通常用于向 API 发送 JSON 数据。可以根据需要修改为其他内容类型（例如 "application/x-www-form-urlencoded"）。
    /// 
    /// Parameter：
    /// 类型：object
    /// 描述：表示请求的参数。这可以是任何类型的对象，通常用于传递要发送到服务器的数据。例如，对于 POST 请求，您可能会将请求体作为 JSON 对象传递。
    /// </summary>
    public class BaseRequest
    {
        public Method Method { get; set; }
        public string Route { get; set; }
        public string ContentType { get; set; } = "application/json";
        public object Parameter { get; set; }
    }
}
