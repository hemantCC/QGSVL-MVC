using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using QGS.BL;
using QGS.Entities.BusinessEntities;
using QGS.Entities.DataEntities;

namespace QGS.WebAPI.Controllers
{
    public class AdministratorController : ApiController
    {
        public AccountBL ObjectBL = new AccountBL();
        [Route("GetAllQuotes")]
        public List<QuoteVM> GetAllQuotes()
        {
            List<QuoteVM> allquotes = ObjectBL.GetAllQuotes();
            return allquotes;
        }

        [Route("GetQuote/{id}")]
        public QuoteVM GetRecord(int? id)
        {
            using (var context = new DbQGSVLEntities())
            {
                tblQuote record = context.tblQuotes.Find(id);

                var recordVM = new QuoteVM()
                {
                    QuoteId = record.QuoteId,
                    UserId = record.tblUser.UserId,
                    CarId = record.CarId,
                    InsuranceId = record.InsuranceId,
                    MileageId = record.MileageId,
                    StatusId = record.StatusId,
                    UserEmail = record.tblUser.UserEmail,
                    Brand = record.tblVehicleDetail.Brand,
                    ModelName = record.tblVehicleDetail.ModelName,
                    Date = record.Date,
                    PaybackTime_Month_ = record.tblPaybackTime.PaybackTime_Month_,
                    Term_Month_ = record.tblInsurance.Term_Month_,
                    Mileage_K_ = record.tblMileage.Mileage_K_,
                    TotalPrice = record.TotalPrice,
                    Status = record.tblStatu.Status,
                    Contact = record.tblUser.Contact
                };
                return recordVM;
            }
        }

        [HttpPost]
        [Route("EditQuote")]
        public IHttpActionResult EditQuote(QuoteVM obj)
        {
            if (obj != null)
            {
                ObjectBL.EditQuote(obj);
                return Ok();
            }
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("DeleteQuote/{id}")]
        public IHttpActionResult DeleteQuote(int? id)
        {

            if (id != null)
            {
                ObjectBL.DeleteQuote(id);
                return Ok();
            }
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("GetStatus")]
        public List<StatusVM> GetStatus()
        {
            using (var db = new DbQGSVLEntities())
            {
                List<tblStatu> status = db.tblStatus.ToList();
                List<StatusVM> statusVM = new List<StatusVM>();
                foreach (var item in status)
                {
                    StatusVM statusvm = new StatusVM()
                    {
                        StatusId = item.StatusId,
                        Status = item.Status
                    };
                    statusVM.Add(statusvm);
                }
                return statusVM;
            }
        }

    }
}
