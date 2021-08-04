using Conversion.Api.Utils;
using Conversion.Domain.Entities;
using Conversion.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Conversion.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<IActionResult> CreateExchange(Exchange exchange)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Exchange.Add(exchange);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new { exchange.Id }, exchange);
            }

            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetCurrencies()
        {
            await GetAllActualCurrency();
            var currencies = await _unitOfWork.Currency.GetAllByDate(DateTime.Now.Date);
            return Ok(currencies);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetExchanges()
        {
            var exchanges = await _unitOfWork.Exchange.All();

            return Ok(exchanges);
        }

        private async Task<IEnumerable<Currency>> GetAllActualCurrency()
        {
            var values = new GetCurrency();
            var currencies = await _unitOfWork.Currency.GetAllByDate(DateTime.Now.Date);
            if (!currencies.Any()) await _unitOfWork.Currency.AddCurrencies(values.GetActualCurrency());
            return await _unitOfWork.Currency.GetAllByDate(DateTime.Now.Date);
        }

        public async Task<IActionResult> GetConvertion(string sourceCurrCode, 
            string targetCurrCode, decimal IncomeAmount)
        {
            var convertion = await GetAllActualCurrency();
            var exchange = convertion.Where(x=>x.Code == )
            return Ok(exchange)
        }
    }
}
