using log4net;
using PuppeteerService;
using ScenarioService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace RegBot.RestApi.Controllers
{
    public class ScenarioController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(nameof(ScenarioController));
        private readonly IChromiumSettings _chromiumSettings;
        public ScenarioController(IChromiumSettings chromiumSettings)
        {
            _chromiumSettings = chromiumSettings;
        }

        [HttpPost]
        [Route("yandexSearch")]
        [ResponseType(typeof(List<string>))]
        public async Task<IHttpActionResult> PostYandexSearch(string query, int pageCount = 3)
        {
            List<string> results;
            try
            {
                var searchEngine = new YandexSearch(_chromiumSettings);
                results = await searchEngine.RunScenario(new[] { query }, pageCount);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            return Ok(results);
        }

        [HttpPost]
        [Route("googleSearch")]
        [ResponseType(typeof(List<string>))]
        public async Task<IHttpActionResult> PostGoogleSearch(string query, int pageCount = 3)
        {
            List<string> results;
            try
            {
                var searchEngine = new GoogleSearch(_chromiumSettings);
                results = await searchEngine.RunScenario(new[] { query }, pageCount);
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            return Ok(results);
        }

        [HttpPost]
        [Route("whois")]
        [ResponseType(typeof(List<string>))]
        public async Task<IHttpActionResult> PosWhois(string domain)
        {
            List<string> results;
            try
            {
                var engine = new NicRuWhois(_chromiumSettings);
                results = await engine.RunScenario(new[] { domain });
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                return InternalServerError();
            }
            return Ok(results);
        }
    }
}
