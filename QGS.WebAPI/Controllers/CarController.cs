using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using QGS.BL;
using QGS.Entities.BusinessEntities;

namespace QGS.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CarController : ApiController
    {
        CarBL carBL = new CarBL();

        [Route("AllCarDetails")]
        [HttpGet]
        public IHttpActionResult GetAllCarDetails()
        {
            List<CarVM> cars = carBL.GetAllCars();
            if (cars.Count == 0)
            {
                return NotFound();
            }
            return Ok(cars);
        }

        [Route("GetCarDetail/{id}")]
        [HttpGet]
        public IHttpActionResult GetCarDetail(int id)
        {
            CarVM car = carBL.GetCar(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        [Route("PostQuoteAdd")]
        [HttpPost]
        public IHttpActionResult QuoteAddDb(CarVM carVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            if (carBL.QuoteDetailsAdd(carVM))
            {
                return Ok("added");
            }


            return BadRequest("Invalid data.");
        }

        [Route("GetQuoteDetails")]
        [HttpGet]
        public IHttpActionResult GetQuoteDetails(string usr_email)
        {
            if (ModelState.IsValid)
            {
                var usrQuoteVM = carBL.QuoteDetailsFetch(usr_email);
                if (!(usrQuoteVM == null))
                {
                    return Ok(usrQuoteVM);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest("Invalid data.");
        }

        //[Route("PostPaymentAdd")]
        //public IHttpActionResult PostPaymentAdd(PaymentVM paymentObj)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Invalid data.");
        //    }

        //    if (carBL.PaymentDetailsAdd(paymentObj))
        //    {
        //        return Ok("added");
        //    }
        //    return BadRequest("Invalid data.");
        //}
        [Route("PersonalDetails")]
        [HttpGet]
        public IHttpActionResult PersonalDetails(string userEmail)
        {
            PersonalDetailsVM personalDetails = carBL.PersonalDetails(userEmail);
            return Ok(personalDetails);
        }

        [Route("AddPersonalDetails")]
        [HttpPost]
        public IHttpActionResult AddPersonalDetails(PersonalDetailsVM personalDetailsVM)
        {

            if (carBL.AddPersonalDetailsBL(personalDetailsVM))
            {
                return Ok("added");
            }
            return BadRequest("Invalid data.");

        }





    }
}

