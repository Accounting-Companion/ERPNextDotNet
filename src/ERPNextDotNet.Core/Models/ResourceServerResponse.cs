using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPNextDotNet.Core.Models;
public class ServerResponse
{
    public bool IsSucess { get; set; }
    public string Data { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
}
public class ResourceServerResponse<T> 
{
    public T Data { get; set; }
}
public class FunctionServerResponse<T>
{
    public T Message { get; set; }
}