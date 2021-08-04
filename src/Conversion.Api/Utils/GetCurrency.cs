using Conversion.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Net;

namespace Conversion.Api.Utils
{
    public class GetCurrency
    {
        private const string url = "https://cbu.uz/ru/arkhiv-kursov-valyut/json/";
        public Currency[] GetActualCurrency()
        {
            string json = null;
            using (var wc = new WebClient())
            {
                json = wc.DownloadString(url);
            }
            Currency[] values = JsonConvert.DeserializeObject<Currency[]>(json,
                new IsoDateTimeConverter { DateTimeFormat = "dd.MM.yyyy" });
            return values;
        }

        public Currency[] GetCurrencyByDate(DateTime dateTime)
        {
            var shortDate = dateTime.ToString("yyyy-MM-dd");
            string json = null;
            using (var wc = new WebClient())
            {
                json = wc.DownloadString($"{url}all/{shortDate}");
            }
            Currency[] values = JsonConvert.DeserializeObject<Currency[]>(json,
                new IsoDateTimeConverter { DateTimeFormat = "dd.MM.yyyy" });
            return values;
        }
    }
}
