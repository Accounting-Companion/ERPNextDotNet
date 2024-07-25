using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPNextDotNet.Core.Models;
public class BaseServerResponse<T> where T : class
{
    public List<T> Data { get; set; }
}
