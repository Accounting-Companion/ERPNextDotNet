using ERPNextDotNet.Core.JsonConverters;
using ERPNextDotNet.Core.Models;
using ERPNextDotNet.Core.Models.Accounting;
using ERPNextDotNet.Services;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ERPNextDotNet.Tests.Services.ERPNextClientTests;

public class BaseERPNextClientTests
{
    internal IERPNextClient _erpnextClient;
    internal FakeMessageHandler _handler;
    public BaseERPNextClientTests()
    {
        _handler = new();
        _erpnextClient = new ERPNextClient(new HttpClient(_handler));
        _erpnextClient.SetBaseUrl("http://localhost");
        _erpnextClient.SetAPIKeys("key", "secret");
    }

}