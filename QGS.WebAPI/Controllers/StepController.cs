using System;
using System.Web.Http;
using QGS.BL;
using QGS.Entities.BusinessEntities;

namespace QGS.WebAPI.Controllers
{
    public class StepController : ApiController
    {
        private StepBL stepBL = new StepBL();

        [HttpPost]
        [Route("DirectDebit")]
        public IHttpActionResult DirectDebit([FromUri]string username, [FromBody]BankDetailVM bankDetailVM)
        {
            try
            {
                stepBL.DirectDebit(username, bankDetailVM);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("Documents")]
        public IHttpActionResult Documents([FromUri]string username, [FromBody]DocumentPathVM documentPath)
        {

            stepBL.Documents(username, documentPath);
            return Ok();


        }
    }
}
