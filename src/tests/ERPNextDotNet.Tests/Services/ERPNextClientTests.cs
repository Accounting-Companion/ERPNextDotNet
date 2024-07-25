using ERPNextDotNet.Core.JsonConverters;
using ERPNextDotNet.Core.Models;
using ERPNextDotNet.Core.Models.Accounting;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ERPNextDotNet.Tests.Services;

public class ERPNextClientTests
{
    ERPNextDotNet.Services.ERPNextClient _erpnextClient;
    JsonSerializerOptions _jsonSerializerOptions;
    public ERPNextClientTests()
    {
        _erpnextClient = new(new HttpClient(FakeMessageHandler.Instance));
     
    }

    [Fact]
    public async Task TestGetAccountsWhenSucess()
    {
        // Set Response
        string json = await Utils.ReadFromFile("Accounting/accounts");
        FakeMessageHandler.SetResponseJson(json);

        // Test
        List<Core.Models.Accounting.Accounts> accounts = await _erpnextClient.GetAccountsAsync();
        Assert.NotNull(accounts);
        Assert.Equal(20, accounts.Count);
    }
}