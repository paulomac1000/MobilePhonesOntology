using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace MobilePhonesOntology.Helpers
{
    public static class DataDownloadHelper
    {
        private static string BrandUrl { get; } = "https://www.phonegg.com/brands/";

        public static void GetAllPhones()
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<string> GetAllBrands()
        {
            using (var client = new WebClient())
            {
                var htmlCode = client.DownloadString(BrandUrl);

                const string regexPattern = "<a href=\"\\/brand\\/.{1,30}\">(?<brand>[^<]+)<\\/a>";
                var matches = Regex.Matches(htmlCode, regexPattern, RegexOptions.Multiline);
                var brands =  (from Match match in matches select match.Groups["brand"].Value).ToList();

                return brands;
            }
        }
    }
}