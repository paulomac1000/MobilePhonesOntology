using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using MobilePhonesOntology.Models;

namespace MobilePhonesOntology.Helpers
{
    public static class DataDownloadHelper
    {
        private static string BrandUrl { get; } = "https://www.phonegg.com/brands/";

        public static void GetAllPhones()
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Brand> GetAllBrands()
        {
            using (var client = new WebClient())
            {
                var htmlCode = client.DownloadString(BrandUrl);

                const string regexPattern = "<a href=\"\\/brand\\/(?<url>.{1,30})\">(?<brand>[^<]+)<\\/a>";
                var matches = Regex.Matches(htmlCode, regexPattern, RegexOptions.Multiline);

                var brands = from Match match in matches
                    select new Brand
                    {
                        Id = Convert.ToInt32(Regex.Match(match.Groups["url"].Value, @"\d+").Value),
                        Url = match.Groups["url"].Value,
                        Name = match.Groups["brand"].Value
                    };

                return brands;
            }
        }
    }
}