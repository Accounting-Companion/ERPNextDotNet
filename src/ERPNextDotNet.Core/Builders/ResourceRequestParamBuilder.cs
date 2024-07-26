namespace ERPNextDotNet.Core.Builders;


public class ResourceRequestParamBuilder : IResourceRequestParamBuilder
{
    Dictionary<string, string> _requestParams = [];
    public ResourceRequestParamBuilder()
    {

    }
    public IResourceRequestParamBuilder AddFilters(List<ResourceFilter> resourceFilters, bool isOrFilter = false)
    {
        _requestParams.Add(isOrFilter ? Constants.RequestParams.OrFilters : Constants.RequestParams.Filters, $"[{string.Join(", ", resourceFilters.Select(c => c.ToString()))}]");
        return this;
    }
    public IResourceRequestParamBuilder AddFields(List<string> fields)
    {
        if (!_requestParams.ContainsKey(Constants.RequestParams.Fields))
        {
            _requestParams.Add(Constants.RequestParams.Fields, $"[{string.Join(", ", fields.Select(c => $"\"{c}\""))}]");
            return this;
        }
        else
        {
            throw new Exception($"Fields is already added - {_requestParams[Constants.RequestParams.OrderBy]}");
        }
    }
    public IResourceRequestParamBuilder AddOrderBy(List<OrderBy> orderbyFields)
    {
        if (!_requestParams.TryGetValue(Constants.RequestParams.OrderBy, out string? value))
        {
            _requestParams.Add(Constants.RequestParams.OrderBy, $"[{string.Join(", ", orderbyFields)}]");
            return this;
        }
        else
        {
            throw new Exception($"OrderBy is already added - {value}");
        }
    }
    public IResourceRequestParamBuilder AddOrderBy(OrderBy orderbyField)
    {
        SafeAdd(Constants.RequestParams.OrderBy, orderbyField.ToString());
        return this;
    }

    public IResourceRequestParamBuilder AddStart(int startnum)
    {
        SafeAdd(Constants.RequestParams.LimitStart, startnum.ToString());
        return this;
    }
    public IResourceRequestParamBuilder AddPageLimit(int pageLimit)
    {
        SafeAdd(Constants.RequestParams.LimitPageLength, pageLimit.ToString());
        return this;
    }
    /// <summary>
    /// Enabling Debug will make server return the executed query and execution time under Exc property in response
    /// </summary>
    public IResourceRequestParamBuilder AddEnableDebug()
    {
        SafeAdd(Constants.RequestParams.Debug, "True");
        return this;
    }

    private void SafeAdd(string key, string value)
    {
        if (!_requestParams.TryGetValue(key, out string? existingValue))
        {
            _requestParams.Add(key, value);
        }
        else
        {
            throw new Exception($"OrderBy is already added - {existingValue}");
        }
    }

    public Dictionary<string, string> Build()
    {
        return _requestParams;
    }

    public static implicit operator Dictionary<string, string>?(ResourceRequestParamBuilder? builder)
    {
        if (builder == null) return null;
        return builder._requestParams;
    }
}

public class ResourceCountRequestParamBuilder : IResourceCountRequestParamBuilder
{
    Dictionary<string, string> _requestParams = [];

    public ResourceCountRequestParamBuilder(string docType)
    {
        _requestParams.Add(Constants.RequestParams.DocType, docType);
    }

    public IResourceCountRequestParamBuilder AddFilters(List<ResourceFilter> resourceFilters, bool isOrFilter = false)
    {
        _requestParams.Add(isOrFilter ? Constants.RequestParams.OrFilters : Constants.RequestParams.Filters, $"[{string.Join(", ", resourceFilters.Select(c => c.ToString()))}]");
        return this;
    }
    public Dictionary<string, string> Build()
    {
        return _requestParams;
    }

}
public struct ResourceFilter
{
    public ResourceFilter(string fieldName, FilterOperator filterOperator, string filterValue)
    {
        FieldName = fieldName;
        FilterOperator = filterOperator switch
        {
            Builders.FilterOperator.Equals => Constants.FilterOperatorStrings.EqualsOperator,
            Builders.FilterOperator.NotEquals => Constants.FilterOperatorStrings.NotEquals,
            Builders.FilterOperator.Like => Constants.FilterOperatorStrings.Like,
            Builders.FilterOperator.NotLike => Constants.FilterOperatorStrings.NotLike,
            Builders.FilterOperator.In => Constants.FilterOperatorStrings.In,
            Builders.FilterOperator.NotIn => Constants.FilterOperatorStrings.NotIn,
            Builders.FilterOperator.Is => Constants.FilterOperatorStrings.Is,
            Builders.FilterOperator.Greaterthan => Constants.FilterOperatorStrings.Greaterthan,
            Builders.FilterOperator.Lessthan => Constants.FilterOperatorStrings.Lessthan,
            Builders.FilterOperator.GreaterthanEqualto => Constants.FilterOperatorStrings.GreaterthanEqualto,
            Builders.FilterOperator.LessthanEqualto => Constants.FilterOperatorStrings.LessthanEqualto,
            Builders.FilterOperator.Between => Constants.FilterOperatorStrings.Between,
            Builders.FilterOperator.Timespan => Constants.FilterOperatorStrings.Timespan,
            Builders.FilterOperator.Descendantsof => Constants.FilterOperatorStrings.Descendantsof,
            Builders.FilterOperator.DescendantsofInclusive => Constants.FilterOperatorStrings.DescendantsofInclusive,
            Builders.FilterOperator.NotDescendantsOf => Constants.FilterOperatorStrings.NotDescendantsOf,
            Builders.FilterOperator.AncestorsOf => Constants.FilterOperatorStrings.AncestorsOf,
            Builders.FilterOperator.NotAncestorsOf => Constants.FilterOperatorStrings.NotAncestorsOf,
            _ => Constants.FilterOperatorStrings.EqualsOperator,
        };
        FilterValue = filterValue;
    }
    public ResourceFilter(string fieldName, string filterOperator, string filterValue)
    {
        FieldName = fieldName;
        FilterOperator = filterOperator;
        FilterValue = filterValue;
    }

    public string FieldName { get; }
    public string FilterOperator { get; }
    public string FilterValue { get; }

    public override readonly string ToString()
    {
        return $"[\"{FieldName}\", \"{FilterOperator}\", \"{FilterValue}\"]";
    }
}

public struct OrderBy
{
    public OrderBy(string fieldName, bool isAscending)
    {
        FieldName = fieldName;
        IsAscending = isAscending;
    }

    public string FieldName { get; }
    public bool IsAscending { get; }

    public override readonly string ToString()
    {
        return $"{FieldName} {(IsAscending ? "asc" : "dsc")}";
    }
}
public enum FilterOperator
{
    Equals,
    NotEquals,
    Like,
    NotLike,
    In,
    NotIn,
    Is,
    Greaterthan,
    Lessthan,
    GreaterthanEqualto,
    LessthanEqualto,
    Between,
    Timespan,


    Descendantsof,
    DescendantsofInclusive,
    NotDescendantsOf,
    AncestorsOf,
    NotAncestorsOf,
}

