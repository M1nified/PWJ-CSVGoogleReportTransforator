using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVFixer.Models
{
    public class CurrencyHistory
    {
        public bool Success { get; set; }
        public string Timestamp { get; set; }
        public bool Historical { get; set; }
        [RestSharp.Deserializers.DeserializeAs(Name = "base")]
        public string BaseCurrency { get; set; }
        public string Date { get; set; }
        public Dictionary<string, float> Rates = new Dictionary<string, float>();
    }
}
