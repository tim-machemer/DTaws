using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Xunit;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using DealerTrackJsonTranslator.Code;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace HelloWorld.Tests
{
  public class FunctionTest
  {
    private static readonly HttpClient client = new HttpClient();

    private static async Task<string> GetCallingIP()
    {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", "AWS Lambda .Net Client");

            var stringTask = client.GetStringAsync("http://checkip.amazonaws.com/").ConfigureAwait(continueOnCapturedContext:false);

            var msg = await stringTask;
            return msg.Replace("\n","");
    }

    [Fact]
    public async Task TestHelloWorldFunctionHandler()
    {
            var request = new APIGatewayProxyRequest();
            var context = new TestLambdaContext();
            string location = GetCallingIP().Result;
            Dictionary<string, string> body = new Dictionary<string, string>
            {
                { "message", "hello world" },
                { "location", location },
            };

            var expectedResponse = "/deals/01H2TGWGWTQ2F5KHD6DEVC866X/partner-dealers/1234/credit-apps/lenders/DT6/decisions/latest";

            var function = new Function();
            
            string path = $@"Data{Path.DirectorySeparatorChar}sample.json";
            string s = File.ReadAllText(path);

            Translator.Root myRoot = null;
            byte[] byteArray = Encoding.UTF8.GetBytes(s);
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                var deserializer = new DefaultLambdaJsonSerializer();
                myRoot = deserializer.Deserialize<Translator.Root>(stream);
                // Use the deserialized object
            }

            var response = await function.FunctionHandler(myRoot, context);

            Console.WriteLine($"Lambda Response: \n{response}");
            Console.WriteLine($"Expected Response: \n{expectedResponse}");

            Assert.Contains(expectedResponse, response);
            }
  }
}