using System.Diagnostics;

namespace ERPNextDotNet;
public static class Constants
{
    public const string ERPNextClientActivityName = "ERPNextClient";
    public static ActivitySource ERPNextClientActivity = new(ERPNextClientActivityName);

    public static class URLS
    {
        public const string APISuffix = "/api";
        public const string MethodSuffix = "/method";
        public const string ResourceSuffix = "/resource";
    }
    public static class DefaultDocTypes
    {
        public static class Accounting
        {
            public const string Account = "Account";
        }

    }
    public static class FrappeMethods
    {
        const string _frappePrefix = "frappe";
        public static class Auth
        {
            const string _authPrefix = $"{_frappePrefix}.auth";
            public const string GetLoggedInUser = $"{_authPrefix}.get_logged_user";
        }
        public static class Desk
        {
            const string _deskPrefix = $"{_frappePrefix}.desk";
            public static class ReportView
            {
                const string _reportViewPrefix = $"{_deskPrefix}.reportview";
                public const string GetCount = $"{_reportViewPrefix}.get_count";
            }
        }
    }

}

