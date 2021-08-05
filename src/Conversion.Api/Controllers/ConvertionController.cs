using Conversion.Domain.Entities;
using Conversion.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Conversion.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConvertionController : ControllerBase
    {
        private readonly ILogger<ConvertionController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ConvertionController(
            ILogger<ConvertionController> logger,
            IUnitOfWork unitOfWork
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateExchange([FromBody] Exchange exchange)
        {
            if (ModelState.IsValid)
            {
                //define income data
                var definedEx= await DefineExchange(exchange);

                //create new object in db
                await _unitOfWork.Exchange.Add(definedEx);
                await _unitOfWork.CompleteAsync();

                return Ok(true);
            }

            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetCurrencies()
        {
            //get data from db by current date
            var currencies = await _unitOfWork.Currency.GetByDateAll(DateTime.Now.Date);

            //check if data empty
            if (!currencies.Any())
            {
                //get latest data fron bank api
                var latestCurrency = GetLatestCurrency();

                //check if it's latest data
                var isLatest = await _unitOfWork.Currency.IsLatestData(latestCurrency[0].Date);
                if (isLatest)
                {
                    //if data from api newer than data from db, add new data
                    await _unitOfWork.Currency.AddCurrencies(latestCurrency);
                    await _unitOfWork.CompleteAsync();
                }
                //if data from api the same as data from db, return existing data from db by date
                return Ok(await _unitOfWork.Currency.GetByDateAll(latestCurrency[0].Date));
            }
            return Ok(currencies);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetExchanges([FromBody] History history)
        {
            var exchanges = await _unitOfWork.Exchange.GetSortedExchange(history);

            return Ok(exchanges);
        }

        //Get Data from NBU bank api
        private Currency[] GetLatestCurrency()
        {
            //link of the api which returns latest data (all currencies by latest date)
            const string url = "https://cbu.uz/ru/arkhiv-kursov-valyut/json/";
            string json = null;
            Currency[] values = null;

            try
            {
                //trying connecting to NBU bank api & get latest data
                using (var wc = new WebClient())
                {
                    json = wc.DownloadString(url);
                }

                //deserialize data from api to Currency object array
                values = JsonConvert.DeserializeObject<Currency[]>(json,
                    new IsoDateTimeConverter { DateTimeFormat = "dd.MM.yyyy" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{api} Get Latest Currency method error");
            }

            return values;
        }

        //define data method
        private async Task<Exchange> DefineExchange(Exchange exchange)
        {
            var newEx = new Exchange();
            newEx.Date = exchange.Date;
            newEx.FromCurrency = await _unitOfWork.Currency.GetById(exchange.FromCurrency.CurrencyId);
            newEx.ToCurrency = await _unitOfWork.Currency.GetById(exchange.ToCurrency.CurrencyId);
            newEx.IncomeAmount = exchange.IncomeAmount;
            newEx.OutcomeAmount = exchange.OutcomeAmount;
            return newEx;
        }
    }
}
