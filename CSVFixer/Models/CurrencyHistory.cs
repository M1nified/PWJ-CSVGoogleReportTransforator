using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVFixer.Models
{
    class CurrencyHistory
    {
        public bool success;
        public string timestamp;
        public bool historical;
        [DeserializeAs(Name = "base")]
        public string baseCurrency { get; set; }
        public string date;
        public Dictionary<string, float> rates = new Dictionary<string, float>();
    }
}
