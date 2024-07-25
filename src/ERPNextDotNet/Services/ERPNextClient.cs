using ERPNextDotNet.Core.JsonConverters;
using ERPNextDotNet.Core.Models;
using ERPNextDotNet.Core.Models.Accounting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ERPNextDotNet.Services;
/// <summary>
/// ERPNextClient is a wrapper for ERPNext RestAPI <br/>
/// ERPNextClient exposes simple api to acess ERPNext data using C# objects
/// </summary>
public class ERPNextClient : IERPNextClient
{
    private readonly ILogger _logger;
    private readonly HttpClient _httpClient;

    private bool _usingAPIKey;
    private JsonSerializerOptions _jsonSerializerOptions;


    public ERPNextClient(ILogger<ERPNextClient> logger,
                         HttpClient httpClient) : this(httpClient)
    {
        _logger = logger;

    }


    public ERPNextClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _logger = NullLogger.Instance;
        _jsonSerializerOptions = new JsonSerializerOptions();
        _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        _jsonSerializerOptions.Converters.Add(new DateTimeConverterUsingDateTimeParseAsFallback());
        _jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    }

    /// <summary>
    /// Use this method to Setup API Ket and API secret which are required to interact with ERPNext
    /// </summary>
    /// <param name="apiKey">api key</param>
    /// <param name="apiSecret">api secret</param>
    public void SetAPIKeys(string apiKey, string apiSecret)
    {
        _usingAPIKey = true;
    }
    /// <summary>
    /// Use this method to Setup Base Url and port of ERPNext server
    /// </summary>
    /// <param name="baseurl">base url on which ERPNext server is runnig</param>
    /// <param name="port">port on which ERPNext server is running</param>
    public void SetBaseUrl(string baseurl, int port = 447)
    {

    }

    /// <summary>
    /// Get List of Accounts (DoctType-Accounts) from ERP Next
    /// This is the Information that is shown in Chart of Accounts
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<Accounts>> GetAccountsAsync(CancellationToken cancellationToken = default)
    {
        List<Accounts> accounts = await GetRequestAsync<Accounts>("http://localhost", token: cancellationToken);
        return accounts;
    }

    //public async Task Login(string UserName, string Password)
    //{

    //}
    private Task<List<T>> PostRequestAsync<T>(string url, Dictionary<string, string>? queryParams = null, CancellationToken token = default) where T : class => SendRequestAsync<T>(url, HttpMethod.Post, queryParams, token);
    private Task<List<T>> GetRequestAsync<T>(string url, Dictionary<string, string>? queryParams = null, CancellationToken token = default) where T : class => SendRequestAsync<T>(url, HttpMethod.Get, queryParams, token);


    public async Task<List<T>> SendRequestAsync<T>(string url,
                                       HttpMethod httpMethod,
                                       Dictionary<string, string>? args = null,
                                       CancellationToken cancellationToken = default) where T : class
    {
        using var activity = Constants.ERPNextClientActivity.StartActivity();
        UriBuilder uriBuilder = new(url)
        {
            Query = args?.ToQueryString()
        };
        HttpRequestMessage httpRequestMessage = new(httpMethod, uriBuilder.ToString());
        var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage, cancellationToken);
        var serverResponse = await httpResponseMessage.Content.ReadFromJsonAsync<BaseServerResponse<T>>(_jsonSerializerOptions, cancellationToken: cancellationToken);
        return serverResponse!.Data;
    }
}
