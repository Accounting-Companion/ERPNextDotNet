namespace ERPNextDotNet.Core;
public class Constants
{
    public static class RequestParams
    {
        public const string DocType = "doctype";

        public const string Fields = "fields";
        public const string Filters = "filters";
        public const string OrFilters = "or_filters";
        public const string OrderBy = "order_by";
        public const string LimitStart = "limit_start";
        public const string LimitPageLength = "limit";
        public const string Debug = "debug";
    }
    public static class FilterOperatorStrings
    {
        public const string EqualsOperator = "=";
        public const string NotEquals = "!=";
        public const string Like = "like";
        public const string NotLike = "not like";
        public const string In = "in";
        public const string NotIn = "not in";
        public const string Is = "is";
        public const string Greaterthan = ">";
        public const string Lessthan = "<";
        public const string GreaterthanEqualto = ">=";
        public const string LessthanEqualto = "<=";
        public const string Between = "Between";
        public const string Timespan = "Timespan";


        public const string Descendantsof = "descendants of";
        public const string DescendantsofInclusive = "descendants of (inclusive)";
        public const string NotDescendantsOf = "not descendants of";
        public const string AncestorsOf = "ancestors of";
        public const string NotAncestorsOf = "not ancestors of";
    }
}
