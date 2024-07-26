
namespace ERPNextDotNet.Core.Builders;

public interface IResourceRequestParamBuilder
{
    IResourceRequestParamBuilder AddEnableDebug();
    IResourceRequestParamBuilder AddFields(List<string> fields);
    IResourceRequestParamBuilder AddFilters(List<ResourceFilter> resourceFilters, bool isOrFilter = false);
    IResourceRequestParamBuilder AddOrderBy(List<OrderBy> orderbyFields);
    IResourceRequestParamBuilder AddOrderBy(OrderBy orderbyField);
    IResourceRequestParamBuilder AddPageLimit(int pageLimit);
    IResourceRequestParamBuilder AddStart(int startnum);
    Dictionary<string, string> Build();
}
public interface IResourceCountRequestParamBuilder
{

    IResourceCountRequestParamBuilder AddFilters(List<ResourceFilter> resourceFilters, bool isOrFilter = false);
    Dictionary<string, string> Build();
}