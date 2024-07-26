using ERPNextDotNet.Core.Builders;
using ERPNextDotNet.Core.JsonConverters;
using ERPNextDotNet.Core.Models;
using ERPNextDotNet.Core.Models.Accounting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
    private string _aPIKey;
    private string _aPISecret;
    private JsonSerializerOptions _jsonSerializerOptions;
    private string _baseUrl;
    private int _port;




    public ERPNextClient(HttpClient httpClient, ILogger<ERPNextClient>? logger = null)
    {
        _httpClient = httpClient;
        _logger = logger == null ? NullLogger.Instance : logger;
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
        _aPIKey = apiKey;
        _aPISecret = apiSecret;
    }
    /// <summary>
    /// Use this method to Setup Base Url and port of ERPNext server
    /// </summary>
    /// <param name="baseurl">base url on which ERPNext server is runnig</param>
    /// <param name="port">port on which ERPNext server is running</param>
    public void SetBaseUrl(string baseurl, int port = 447)
    {
        _baseUrl = baseurl;
        _port = port;
    }

    public async Task<string> GetUserAccountAsync()
    {
        var response = await GetDataFromMethodAsync<FunctionServerResponse<string>>(Constants.FrappeMethods.Auth.GetLoggedInUser);
        return response.Message;
    }

    /// <summary>
    /// Get List of Account (DoctType-Account) from ERP Next
    /// This is the Information that is shown in Chart of Account
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<Account>> GetAccountsAsync(IResourceRequestParamBuilder? paramsbuilder = null, CancellationToken cancellationToken = default)
    {
        paramsbuilder ??= new ResourceRequestParamBuilder();
        paramsbuilder.AddFields(["*"]);
        var accounts = await GetResourceListAsync<Account>(DefaultDocTypes.Accounting.Account, paramsbuilder, token: cancellationToken);
        return accounts;
    }
    public async Task<int> GetCountOfDocType(IResourceCountRequestParamBuilder paramsbuilder, CancellationToken token = default)
    {
        var resp = await GetDataFromMethodAsync<FunctionServerResponse<int>>(FrappeMethods.Desk.ReportView.GetCount, paramsbuilder?.Build(), token);
        return resp.Message;
    }
    public async Task<T> GetDataFromMethodAsync<T>(string methodName,
                                                   Dictionary<string, string>? methodargs = null,
                                                   CancellationToken cancellationToken = default)
    {
        var serverResponse = await GetRequestAsync($"{GetAPIBaseUrl()}{URLS.MethodSuffix}/{methodName}",
                                            queryParams: methodargs,
                                            token: cancellationToken).ConfigureAwait(false);
        if (serverResponse.IsSucess)
        {
            var data = JsonSerializer.Deserialize<T>(serverResponse.Data, _jsonSerializerOptions);
            return data!;
        }
        else
        {
            throw new Exception(serverResponse.Error);
        }
    }


    public async Task<List<T>> GetResourceListAsync<T>(string doctType,
                                                       IResourceRequestParamBuilder? paramsbuilder = null,
                                                       CancellationToken token = default)
    {
        var serverResponse = await GetRequestAsync($"{GetAPIBaseUrl()}{URLS.ResourceSuffix}/{doctType}",
                                            queryParams: paramsbuilder?.Build(),
                                            token: token);
        if (serverResponse.IsSucess)
        {
            var response = JsonSerializer.Deserialize<ResourceServerResponse<List<T>>>(serverResponse.Data, _jsonSerializerOptions);

            return response!.Data;
        }
        else
        {
            throw new Exception(serverResponse.Error);
        }
    }


    private string GetAPIBaseUrl()
    {
        string url;
        if (_port is 447 or 80)
        {
            url = _baseUrl;
        }
        else
        {
            url = $"{_baseUrl}:{_port}";
        }
        return $"{url}{URLS.APISuffix}";
    }
    private Task<ServerResponse> PostRequestAsync(string url, Dictionary<string, string>? queryParams = null, CancellationToken token = default) => SendRequestAsync(url, HttpMethod.Post, queryParams, token);
    private Task<ServerResponse> GetRequestAsync(string url, Dictionary<string, string>? queryParams = null, CancellationToken token = default) => SendRequestAsync(url, HttpMethod.Get, queryParams, token);


    public async Task<ServerResponse> SendRequestAsync(string url,
                                       HttpMethod httpMethod,
                                       Dictionary<string, string>? args = null,
                                       CancellationToken cancellationToken = default)
    {
        ServerResponse response = new();
        using var activity = Constants.ERPNextClientActivity.StartActivity();
        UriBuilder uriBuilder = new(url)
        {
            Query = args?.ToQueryString()
        };
        string fullUrl = uriBuilder.ToString();
        HttpRequestMessage httpRequestMessage = new(httpMethod, fullUrl);
        if (_usingAPIKey)
        {
            httpRequestMessage.Headers.TryAddWithoutValidation("Authorization", $"token {_aPIKey}:{_aPISecret}");
        }
        try
        {
            var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage, cancellationToken).ConfigureAwait(false);
            //var isSucess = httpResponseMessage.EnsureSuccessStatusCode();
            var serverResponse = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
            response.Data = serverResponse;
            response.IsSucess = true;
        }
        catch (Exception ex)
        {
            response.IsSucess = false;
            response.Error = ex.Message;
        }

        return response;
    }



    public Task<List<T>> GetDocListAsync<T>(string doctType, ResourceRequestParamBuilder? paramsbuilder = null, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }


}
