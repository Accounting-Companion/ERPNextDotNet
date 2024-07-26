using ERPNextDotNet.Core.Models.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPNextDotNet.Tests.Services.ERPNextClientTests.GetDocsTests.Accounts;
public class GetAccountsTests : BaseERPNextClientTests
{
    public GetAccountsTests()
    {
    }


    [Fact]
    public async Task TestGetAccountsWhenSucess()
    {
        // Set Response
        string json = await Utils.ReadFromFile("Accounting/Accounts");
        _handler.SetResponseJson(json);

        // Test
        List<Core.Models.Accounting.Account> accounts = await _erpnextClient.GetAccountsAsync();
        Assert.NotNull(accounts);
        Assert.Equal(20, accounts.Count);
    }
}
