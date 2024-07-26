using ERPNextDotNet.Core.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPNextDotNet.Tests.Services.ERPNextClientTests.MethodTests;

public class GetLoggedInUserTests : BaseERPNextClientTests
{
    [Fact]
    public async Task TestGetDocCount()
    {
        string json = await Utils.ReadFromFile("Methods/GetLoggedinUser");
        _handler.SetResponseJson(json);

        var userMail = await _erpnextClient.GetUserAccountAsync();

        Assert.Equal("tally@integration.com", userMail);
    }
}
