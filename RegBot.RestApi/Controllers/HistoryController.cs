using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Common.Service.Interfaces;
using LiteDB;
using log4net;

namespace RegBot.RestApi.Controllers
{
    public class HistoryController : ControllerBase
    {
        private static readonly ILog Log = LogManager.GetLogger(nameof(EmailController));


        [HttpGet]
        [Route("History")]
        [ResponseType(typeof(IEnumerable<IAccountData>))]
        public async Task<IHttpActionResult> GetHistory()
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                return Ok(db.GetCollection<IAccountData>("AccountsData").FindAll().OrderByDescending(z => z.Id));
            }
        }
    }
}
