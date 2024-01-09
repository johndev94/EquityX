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
            if (string.IsNullOrEmpty(stocks))
            {
                throw new ArgumentException("Stocks parameter is null or empty", nameof(stocks));
            }

            var client = new HttpClient(); // Make this singleton instance instead?
            var request = new HttpRequestMessage(HttpMethod.Get, "https://yfapi.net/v6/finance/quote?region=US&lang=en&symbols=" + stocks);

            request.Headers.Add("X-API-KEY", API_KEY);

            try
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);

                try
                {
                    Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(responseBody);
                    return myDeserializedClass?.quoteResponse?.result ?? new List<Result>();


                }
                catch (Exception e)
                {
                    Console.WriteLine($"JSON Parsing exception: {e.Message}");
                    // Return an empty list or handle the exception as needed
                    return new List<Result>();
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request exception: {e.Message}");
                // Return an empty list or handle the exception as needed
                return new List<Result>();
            }
        }



        //public async Task<List<Result>> GetCrypto()
        //{
        //    var client = new HttpClient();
        //    var request = new HttpRequestMessage(HttpMethod.Get, "https://yfapi.net/v6/finance/quote?region=US&lang=en&symbols=ADA-USD,BTC-USD,BUSD-USD,CEL-USD,CNTR-USD,COMET-USD");

        //    request.Headers.Add("X-API-KEY", API_KEY);
        //    var task = client.SendAsync(request);
        //    var response = task.Result;
        //    //var response = await client.SendAsync(request);
        //    response.EnsureSuccessStatusCode();
        //    string responsebody = response.Content.ReadAsStringAsync().Result;
        //    Console.WriteLine(responsebody);


        //    Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(responsebody);


        //    return myDeserializedClass.quoteResponse.result;


        //}

    }
}
