using ERPNextDotNet;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ERPNextDotNet;
public static class Utils
{
    public static string ToQueryString(this Dictionary<string, string> srcDict)
    {
        NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(string.Empty);
        foreach (var item in srcDict)
        {
            nameValueCollection.Add(item.Key, item.Value);
        }
        return nameValueCollection.ToString()!;
    }
}
