using ERPNextDotNet.Core.Models.Accounting;

namespace ERPNextDotNet.Services;
public interface IERPNextClient
{
    Task<List<Accounts>> GetAccountsAsync(CancellationToken cancellationToken = default);
    Task<List<T>> SendRequestAsync<T>(string url, HttpMethod httpMethod, Dictionary<string, string>? args = null, CancellationToken cancellationToken = default) where T : class;
    void SetAPIKeys(string APIKey, string APISecret);
    void SetBaseUrl(string baseurl, int Port = 447);
}