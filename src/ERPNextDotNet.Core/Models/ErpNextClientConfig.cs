using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPNextDotNet.Core.Models;
public class ErpNextClientConfig
{
    public string Url { get; set; }
    public bool IsTokenBased { get; set; }
    public string APIKey { get; set; }
    public string APISecret { get; set; }
}
