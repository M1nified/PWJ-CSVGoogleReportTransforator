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

        private Dictionary<string, string> cache = new Dictionary<string, string>();

        FixerIo(string apiKey)
        {
            this.apiKey = apiKey;
            httpClient.AddDefaultUrlSegment("access_key", this.apiKey);
        }

        void GetRatesForDate(DateTime dateTime)
        {
            var date = dateTime.Year + "-" + dateTime.Month + "-" + dateTime.Day;
            if(cache.ContainsKey(date))
            {

            }
            else
            {
                var request = new RestRequest(date, Method.POST);
                request.AddParameter("base", this.baseCurrency);
            }
        }
    }
}
