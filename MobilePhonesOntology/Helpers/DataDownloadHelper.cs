using Antlr.Runtime;
using MobilePhonesOntology.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MobilePhonesOntology.Helpers
{
    public static class DataDownloadHelper
    {
        private static string BrandUrl { get; } = "https://www.phonegg.com/brands/";
        private static string GenerateApiTokenUrl { get; } = "https://fonoapi.freshpixl.com/token/generate";
        private static string RegisterApiTokenUrl { get; } = "https://fonoapi.freshpixl.com/token/newToken";
        private static string ApiUrl { get; } = "https://fonoapi.freshpixl.com/v1/getdevice";
        private static string Token { get; set; }

        public static async Task<IEnumerable<Phone>> GetAllPhones()
        {
            var brands = GetAllBrands().ToArray();

            var phoneNames = new List<Phone>();

            //var lockMe = new object();
            //var parallelLoopResult = Parallel.ForEach(brands, async brand =>
            //{
            //    var phonesSimpleByBrand = GetPhonesByBrand(brand);
            //    var phonesByBrand = new List<Phone>();

            //    foreach (var phoneSimple in phonesSimpleByBrand)
            //    {
            //        var phone = await GetPhone(phoneSimple.Model, phoneSimple.Brand);

            //        if (phone == null)
            //            continue;

            //        phonesByBrand.Add(phone);
            //    }

            //    lock (lockMe)
            //    {
            //        phoneNames.AddRange(phonesByBrand);
            //    }
            //});

            foreach (var brand in brands)
            {
                var phonesSimpleByBrand = GetPhonesByBrand(brand);
                var phonesByBrand = new List<Phone>();

                foreach (var phoneSimple in phonesSimpleByBrand)
                {
                    var phone = await GetPhone(phoneSimple.Model, phoneSimple.Brand);

                    if (phone == null)
                        continue;

                    phonesByBrand.Add(phone);
                }

                phoneNames.AddRange(phonesByBrand);
            }
            return phoneNames;
        }

        public static async Task<Phone> GetPhone(string model, string brand = null)
        {
            var token = await GetNewApiToken();

            var parameters = new Dictionary<string, string>
            {
                { "device", model },
                { "token", token }
            };

            if (!string.IsNullOrEmpty(brand))
                parameters.Add("brand", brand);

            var urlEncodedContent = new FormUrlEncodedContent(parameters);

            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                response = await client.PostAsync(ApiUrl, urlEncodedContent);
            }

            var responseString = await response.Content.ReadAsStringAsync();

            try
            {
                return JsonConvert.DeserializeObject<IEnumerable<Phone>>(responseString).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public static IEnumerable<PhoneSimple> GetAllSimplePhones()
        {
            var brands = GetAllBrands().ToList();

            var phoneNames = new List<PhoneSimple>();

            var lockMe = new object();
            Parallel.ForEach(brands, brand =>
            {
                var phonesByBrand = GetPhonesByBrand(brand);
                lock (lockMe)
                {
                    phoneNames.AddRange(phonesByBrand);
                }
            });

            return phoneNames;
        }

        public static IEnumerable<PhoneSimple> GetPhonesByBrand(Brand brand)
        {
            string htmlCode;

            using (var client = new WebClient())
            {
                htmlCode = client.DownloadString(brand.Url);
            }

            const string regexPattern = "<span>(?<model>[^<]+)<\\/span>";
            var matches = Regex.Matches(htmlCode, regexPattern, RegexOptions.Multiline);

            var phones = from Match match in matches
                         select new PhoneSimple { Brand = brand.Name, Model = match.Groups["model"].Value };

            return phones;
        }

        public static IEnumerable<Brand> GetAllBrands()
        {
            string htmlCode;

            using (var client = new WebClient())
            {
                htmlCode = client.DownloadString(BrandUrl);
            }

            const string regexPattern = "<a href=\"\\/brand\\/(?<url>.{1,30})\">(?<brand>[^<]+)<\\/a>";
            var matches = Regex.Matches(htmlCode, regexPattern, RegexOptions.Multiline);

            var brands = from Match match in matches
                         select new Brand
                         {
                             Id = Convert.ToInt32(Regex.Match(match.Groups["url"].Value, @"\d+").Value),
                             Url = "https://www.phonegg.com/brand/" + match.Groups["url"].Value,
                             Name = match.Groups["brand"].Value
                         };

            return brands;
        }

        public static async Task<string> GetNewApiToken()
        {
            if (!string.IsNullOrEmpty(Token)) return Token;

            string htmlCode;
            using (var client = new WebClient())
            {
                htmlCode = client.DownloadString(GenerateApiTokenUrl);
            }

            const string pattern = "_token = \"(?<token>[^\"]+)\"";
            var myRegex = new Regex(pattern, RegexOptions.IgnoreCase);
            var match = myRegex.Match(htmlCode);

            if (!match.Success) throw new MissingTokenException();

            var token = match.Groups["token"].Value;

            var parameters = new Dictionary<string, string>
            {
                { "ApiToken", token }
            };

            var urlEncodedContent = new FormUrlEncodedContent(parameters);

            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                response = await client.PostAsync(RegisterApiTokenUrl, urlEncodedContent);
            }

            var responseString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseString);

            if (apiResponse.Status != "success")
                throw new Exception(apiResponse.Message);

            Token = token;

            return Token;
        }
    }
}