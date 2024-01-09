using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Update;

namespace EquityX.Model
{


    public class WebDataManager
    {

        private string API_KEY = "MbYT4MIBRB412pQPXHrRx77V9QfxhKTt8XLgA9L2";

        public async Task<List<Result>> GetStock(string stocks)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://yfapi.net/v6/finance/quote?region=US&lang=en&symbols="+stocks);
            
            request.Headers.Add("X-API-KEY", API_KEY);
            var task = client.SendAsync(request);
            var response = task.Result;
            //var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responsebody = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(responsebody);


            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(responsebody);


            return myDeserializedClass.quoteResponse.result;

           
        }

        public async Task<List<Result>> GetCrypto()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://yfapi.net/v6/finance/quote?region=US&lang=en&symbols=ADA-USD,BTC-USD,BUSD-USD,CEL-USD,CNTR-USD,COMET-USD");

            request.Headers.Add("X-API-KEY", API_KEY);
            var task = client.SendAsync(request);
            var response = task.Result;
            //var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responsebody = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(responsebody);


            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(responsebody);


            return myDeserializedClass.quoteResponse.result;


        }

    }
}
