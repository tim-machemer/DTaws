using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DealerTrackJsonTranslator.Code;

public class PostData
{
    private string _postvalue;
    private string _postparam = "href";
    private string _posturl = "https://aws1.selectfi.app/sample.php";
    public PostData(string postvalue)
    {
        _postvalue = postvalue;
    }

    public async Task<string> Fire()
    {
        
        string responseContent = "";
        
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Prepare the data to be sent
                    var data = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>(_postparam, _postvalue),
                        // Add more key-value pairs as needed
                    });

                    // Send the POST request
                    HttpResponseMessage response = await client.PostAsync(_posturl, data);

                    // Read the response
                    responseContent = await response.Content.ReadAsStringAsync();

                    // Output the response
                    Console.WriteLine("Response: " + Environment.NewLine + responseContent);

                    return responseContent;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return responseContent;
    }
}