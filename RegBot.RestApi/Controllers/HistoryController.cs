using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Common.Service.Interfaces;
using LiteDB;
using log4net;

namespace RegBot.RestApi.Controllers
{
    public class HistoryController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(nameof(EmailController));
        private static readonly string AppPath = HttpRuntime.BinDirectory; //HttpRuntime.AppDomainAppPath
        private readonly string connectionString;

        public HistoryController()
        {
            connectionString = Path.Combine(AppPath, ConfigurationManager.AppSettings["DbPath"]);
        }

        [HttpGet]
        [Route("History")]
        [ResponseType(typeof(IEnumerable<IAccountData>))]
        public async Task<IHttpActionResult> GetHistory()
        {
            using (var db = new LiteDatabase(connectionString))
            {
                return Ok(db.GetCollection<IAccountData>("AccountsData").FindAll().OrderByDescending(z => z.Id));
            }
        }
    }
}
