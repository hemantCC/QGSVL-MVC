using System.Collections.Generic;
using QGS.DL;
using QGS.Entities.BusinessEntities;

namespace QGS.BL
{
    public class CarBL
    {
        CarDL carDL = new CarDL();
        public List<CarVM> GetAllCars()
        {
            int totalCars = carDL.GetCarCount();
            if (totalCars > 0)
            {
                List<CarVM> cars = carDL.GetAllCars();
                return cars;
            }
            else
            {
                return null;
            }
        }
        public CarVM GetCar(int id)
        {
            CarVM car = carDL.GetCar(id);
            if (car != null)
            {
                return car;
            }
            else
            {
                return null;
            }
        }
        public bool QuoteDetailsAdd(CarVM carVM)
        {
            return (carDL.QuoteDetailsAdd(carVM));
        }

        public UsrQuoteVM QuoteDetailsFetch(string usr_email)
        {

            return (carDL.QuoteDetailsFetch(usr_email));
        }

        public PersonalDetailsVM PersonalDetails(string userEmail)
        {
            return (carDL.PersonalDetails(userEmail));
        }

        public bool AddPersonalDetailsBL(PersonalDetailsVM personalDetailsVM)
        {
            return carDL.AddPersonalDetailsDL(personalDetailsVM);
        }



        //public bool PaymentDetailsAdd(PaymentVM paymentObj)
        //{
        //    return (carDL.PaymentDetailsAdd(paymentObj));
        //}
    }
}
