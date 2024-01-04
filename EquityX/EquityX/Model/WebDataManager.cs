using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Update;

namespace WebAPITest
{


    public class WebDataManager
    {

        private string API_KEY = "MbYT4MIBRB412pQPXHrRx77V9QfxhKTt8XLgA9L2\r\n";

        public async Task<List<Result>> GetQuote(string quote)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://yfapi.net/v6/finance/quote?region=US&lang=en&symbols="+quote);
            request.Headers.Add("X-API-KEY", API_KEY);
            var task = client.SendAsync(request);
            var response = task.Result;
            //var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responsebody = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(responsebody);


            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(responsebody);


            return myDeserializedClass.quoteResponse.result;

            //Root myDeserializedClass = JsonSerializer.Deserialize(<Root>(responsebody);
        }

    }
}
