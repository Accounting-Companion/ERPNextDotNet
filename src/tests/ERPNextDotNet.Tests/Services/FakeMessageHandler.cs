using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPNextDotNet.Tests.Services;
public class FakeMessageHandler : HttpMessageHandler
{
    private static FakeMessageHandler _fakeMessageHandler;
    private static string? _respJson;

    private FakeMessageHandler()
    {

    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_respJson != null)
        {
            return Task.FromResult(new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.OK, Content = new StringContent(_respJson, Encoding.UTF8, "application/json") });
        }
        else
        {
            return Task.FromResult(new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.OK });
        }
    }

    public static FakeMessageHandler Instance { get => _fakeMessageHandler ??= new(); }


    public static void SetResponseJson(string json)
    {
        _respJson = json;
    }
}
