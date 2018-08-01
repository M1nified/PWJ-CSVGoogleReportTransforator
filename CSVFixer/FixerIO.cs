using CSVFixer.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSVFixer
{
    class FixerIo
    {
        private string apiKey;

        private string baseCurrency = "EUR";

        private readonly RestClient httpClient = new RestClient("http://data.fixer.io/api/");

        private Dictionary<string, Dictionary<string, float>> cache = new Dictionary<string, Dictionary<string, float>>();

        public FixerIo(string apiKey)
        {
            this.apiKey = apiKey;
            httpClient.AddDefaultUrlSegment("access_key", this.apiKey);
        }

        public Dictionary<string, float> GetRatesForDate(DateTime dateTime)
        {
            var date = dateTime.ToString("yyyy-MM-dd");
            if(cache.ContainsKey(date))
            {
                var data = this.cache[date];
                return data;
            }
            else
            {
                var request = new RestRequest(date, Method.GET);
                request.AddParameter("base", this.baseCurrency);
                request.AddQueryParameter("access_key", this.apiKey);

                var z = request.ToString();

                RestResponse<CurrencyHistory> response = (RestResponse<CurrencyHistory>)httpClient.Execute<CurrencyHistory>(request);

                dynamic res = JsonConvert.DeserializeObject(response.Content);
                foreach(Newtonsoft.Json.Linq.JProperty item in res)
                {
                    if(item.Name == "rates")
                    {
                        response.Data.Rates = JsonConvert.DeserializeObject<Dictionary<string, float>>(item.Value.ToString());
                    }
                }
                var rates = response.Data.Rates;
                this.cache.Add(date, rates);
                return rates;
            }
        }

        public float GetCurrencyConversionRate(DateTime dateTime, string from, string to)
        {
            var rates = GetRatesForDate(dateTime);
            if (rates.ContainsKey(from) && rates.ContainsKey(to))
            {
                var rateFrom = rates[from];
                var rateTo = rates[to];
                var rate = rateTo / rateFrom;
                return rate;
            }
            return -1;
        }

        public float Convert(DateTime dateTime, string from, string to, float fromValue)
        {
            var rate = GetCurrencyConversionRate(dateTime, from, to);
            if(rate > 0)
            {
                var value = rate * fromValue;
                return value;
            }
            return -1;
        }
    }
}
