using ERPNextDotNet.Core.Models.Accounting;

namespace ERPNextDotNet.Core.Models;
public class ERPNextBaseModel : IERPNextBaseModel
{
    public DateTime Creation { get; set; }

    public int Idx { get; set; }

    public DateTime Modified { get; set; }

    public string ModifiedBy { get; set; }

    public string Name { get; set; }

    public string Owner { get; set; }

    public DocStatus Docstatus { get; set; }
}
public enum DocStatus
{
    Draft = 0,
    Submitted = 1,
    Cancelled = 2
}
