using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSVFixer
{
    class StringNumberExtractor
    {
        public string Extract(string input)
        {
            var regex = new Regex(@"([\d,.]+)");

            var match = regex.Match(input);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return null;
        }
    }
}
