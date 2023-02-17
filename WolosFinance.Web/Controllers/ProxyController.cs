using AspNetCore.Proxy;
using Microsoft.AspNetCore.Mvc;

namespace WolosFinance.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProxyController : ControllerBase
    {
        private readonly ILogger<ProxyController> _logger;
        private readonly IConfiguration _configuration;

        public ProxyController(
            ILogger<ProxyController> logger
            , IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }


        [HttpGet("StockQuery")]
        public Task ProxyCatchAll()
        {
            var page = _configuration.GetSection("Stocks").GetValue<string>("Page");
            var apiKey = _configuration.GetSection("Stocks").GetValue<string>("ApiKey");

            // If you don't need the query string, then you can remove this.
            var queryString = this.Request.QueryString.Value;
            return this.HttpProxyAsync($"{page}{queryString}&{apiKey}");
        }

    }
}