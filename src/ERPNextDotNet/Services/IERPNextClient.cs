using ERPNextDotNet.Core.Builders;
using ERPNextDotNet.Core.Models;
using ERPNextDotNet.Core.Models.Accounting;

namespace ERPNextDotNet.Services;
public interface IERPNextClient
{
    Task<List<Account>> GetAccountsAsync(IResourceRequestParamBuilder? paramsbuilder = null, CancellationToken cancellationToken = default);
    
   
    

    
    Task<string> GetUserAccountAsync();

    Task<int> GetCountOfDocType(IResourceCountRequestParamBuilder paramsbuilder, CancellationToken token = default);
    Task<List<T>> GetDocListAsync<T>(string doctType, ResourceRequestParamBuilder? paramsbuilder = null, CancellationToken token = default);


    Task<T> GetDataFromMethodAsync<T>(string methodName, Dictionary<string, string> methodargs, CancellationToken cancellationToken = default);

    Task<ServerResponse> SendRequestAsync(string url, HttpMethod httpMethod, Dictionary<string, string>? args = null, CancellationToken cancellationToken = default);
    void SetAPIKeys(string apiKey, string apiSecret);
    void SetBaseUrl(string baseurl, int port = 447);
}