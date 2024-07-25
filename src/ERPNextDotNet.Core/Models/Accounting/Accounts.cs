using ERPNextDotNet.Core.JsonConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ERPNextDotNet.Core.Models.Accounting;
public class Accounts : ERPNextBaseModel
{
    public string AccountCurrency { get; set; }

    public string AccountName { get; set; }

    public string AccountNumber { get; set; }

    public string AccountType { get; set; }

    public string BalanceMustBe { get; set; }

    public string Company { get; set; }
    [JsonConverter(typeof(BoolZeroOneConverter))]
    public bool Disabled { get; set; }

    [JsonConverter(typeof(BoolYesNoConverter))]
    public bool FreezeAccount { get; set; }

    [JsonConverter(typeof(BoolZeroOneConverter))]
    public bool IncludeInGross { get; set; }

    [JsonConverter(typeof(BoolZeroOneConverter))]
    public bool IsGroup { get; set; }

    public int Lft { get; set; }

    public string? OldParent { get; set; }

    public string? ParentAccount { get; set; }

    public string ReportType { get; set; }

    public int Rgt { get; set; }

    public string RootType { get; set; }

    public decimal TaxRate { get; set; }
}
