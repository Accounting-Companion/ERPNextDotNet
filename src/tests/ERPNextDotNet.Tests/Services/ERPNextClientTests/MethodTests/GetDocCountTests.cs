using ERPNextDotNet.Core.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPNextDotNet.Tests.Services.ERPNextClientTests.MethodTests;
public class GetDocCountTests : BaseERPNextClientTests
{
    public GetDocCountTests()
    {
    }

    [Fact]
    public async Task TestGetDocCount()
    {
        string json = await Utils.ReadFromFile("Methods/ReportCountResponse");
        _handler.SetResponseJson(json);

        var count = await _erpnextClient.GetCountOfDocType(new ResourceCountRequestParamBuilder(Constants.DefaultDocTypes.Accounting.Account));

        Assert.Equal(10, count);
    }
}
