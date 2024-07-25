using System.Diagnostics;

namespace ERPNextDotNet;
public static class Constants
{
    public const string ERPNextClientActivityName = "ERPNextClient";
    public static ActivitySource ERPNextClientActivity = new (ERPNextClientActivityName);
}
