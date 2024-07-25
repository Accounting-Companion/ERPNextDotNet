using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPNextDotNet.Core.Models;
public interface IERPNextBaseModel
{
    DateTime Creation { get; set; }
    DocStatus Docstatus { get; set; }
    int Idx { get; set; }
    DateTime Modified { get; set; }
    string ModifiedBy { get; set; }
    string Name { get; set; }
    string Owner { get; set; }
}
