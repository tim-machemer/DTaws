using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using DealerTrackJsonTranslator.Code;
    
// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.

namespace HelloWorld
{

    public class Function
    {

        //private static readonly HttpClient client = new HttpClient();

        /*private static async Task<string> GetCallingIP()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", "AWS Lambda .Net Client");

            var msg = await client.GetStringAsync("http://checkip.amazonaws.com/").ConfigureAwait(continueOnCapturedContext:false);

            return msg.Replace("\n","");
        }*/

        [assembly: LambdaSerializer(typeof(Translator.Root))]
        public async Task<string> FunctionHandler( Translator.Root input, ILambdaContext context)
        {

            string pubKeyPath = @$"Data{Path.DirectorySeparatorChar}public_key.pem";
            string pub_key = File.ReadAllText(pubKeyPath);
            SslLib ssl = new SslLib(input.detail.eventDetailHref, pub_key);

            Console.WriteLine("Encrtyped as bse 64" + ssl.ReturnValueBase64());

            string privKeyPath = @$"Data{Path.DirectorySeparatorChar}private_key.pem";
            string priv_key = File.ReadAllText(privKeyPath);
            SslLibDecryptBytes sslD = new SslLibDecryptBytes(ssl.ReturnValueBytes(), priv_key);
            Console.WriteLine("Decrypted from byte array: " + sslD.ReturnValue());

            SslLibDecrypt ssl64 = new SslLibDecrypt(ssl.ReturnValueBase64(), priv_key);
            Console.WriteLine("Decrypted from base 64 string: " + ssl64.ReturnValue());

            PostData pd = new(ssl.ReturnValueBase64());
            var s = await pd.Fire();


            return s;
        }
    }
}
