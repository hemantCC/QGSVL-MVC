using System.Collections.Generic;
using System.Linq;
using QGS.Entities.BusinessEntities;
using QGS.Entities.DataEntities;

namespace QGS.DL
{
    public class CarDL
    {
        public int GetCarCount()
        {
            using (var db = new DbQGSVLEntities())
            {
                var totalCars = db.tblVehicleDetails.Count();
                return totalCars;
            }
        }

        public List<CarVM> GetAllCars()
        {
            using (var db = new DbQGSVLEntities())
            {

                List<tblVehicleDetail> cars = db.tblVehicleDetails.ToList();
                List<CarVM> carVMs = new List<CarVM>();
                foreach (var car in cars)
                {
                    carVMs.Add(MapConfig.CarDetailsToBusinessEntity(car));
                }
                return carVMs;
            }
        }

        public CarVM GetCar(int id)
        {
            using (var db = new DbQGSVLEntities())
            {
                tblVehicleDetail car = db.tblVehicleDetails.Find(id);
                CarVM carVM = MapConfig.CarDetailsToBusinessEntity(car);
                return carVM;
            }
        }

        public PersonalDetailsVM PersonalDetails(string userEmail)
        {
            using (var db = new DbQGSVLEntities())
            {
                PersonalDetailsVM personalDetails = MapConfig.PersonalDetailsDataEntityToBusinessEntity(userEmail);
                return personalDetails;
            }

        }



        public static int mileage(int final_mileage)
        {
            using (var db = new DbQGSVLEntities())
            {
                var v = db.tblMileages.Where(x => x.Mileage_K_ == final_mileage).Select(x => x.MileageId).First();
                return v;
            }
        }


        public static int time(int final_time)
        {
            using (var db = new DbQGSVLEntities())
            {
                var v = db.tblPaybackTimes.Where(x => x.PaybackTime_Month_ == final_time).Select(x => x.PaybackTimeId).First();
                return v;
            }
        }

        public bool QuoteDetailsAdd(CarVM carVM)
        {
            using (var db = new DbQGSVLEntities())
            {
                if (carVM.final_insage >= 20 && carVM.final_insage <= 40)
                {
                    carVM.finalIns = 1;
                }
                else if (carVM.final_insage >= 40 && carVM.final_insage <= 60)
                {
                    carVM.finalIns = 2;
                }
                else if (carVM.final_insage >= 60 && carVM.final_insage <= 80)
                {
                    carVM.finalIns = 3;
                }
                else
                {
                    carVM.finalIns = 4;
                }

                db.tblQuotes.Add(MapConfig.CarDetailsBusinessEntityToDataEntity(carVM));
                db.SaveChanges();
            }
            return true;
        }

        //public List<UsrQuoteVM> QuoteDetailsFetch(int usrId)
        //{
        //    using (var db = new DbQGSVLEntities())
        //    {
        //        var tblQuote=new tblQuote();
        //        var quotesIds = db.tblQuotes.Where(x => x.UserId == usrId).Select(x => x.QuoteId).ToList();
        //        var usrquotesVM = new List<UsrQuoteVM>();
        //        foreach (var item in quotesIds)
        //        {
        //            tblQuote = db.tblQuotes.Where(x => x.QuoteId == item).First();
        //            usrquotesVM.Add(MapConfig.QuoteDetailsDataEntityToBusinessEntity(tblQuote));
        //        }
        //        return (usrquotesVM);
        //    }
        //}

        public UsrQuoteVM QuoteDetailsFetch(string usr_email)
        {
            using (var db = new DbQGSVLEntities())
            {
                var tblQuote = new tblQuote();
                var usr_id = db.tblUsers.Where(x => x.UserEmail == usr_email).Select(x => x.UserId).FirstOrDefault();
                var quotesIds = db.tblQuotes.Where(x => x.UserId == usr_id).Select(x => x.QuoteId).ToList();
                var quoteDetails = new List<QuoteDetail>();
                foreach (var item in quotesIds)
                {
                    tblQuote = db.tblQuotes.Where(x => x.QuoteId == item).First();
                    quoteDetails.Add(MapConfig.QuoteDetailsDataEntityToBusinessEntity(tblQuote));
                }
                var usrquotesVM = MapConfig.UsrQuoteDetailsDataEntityToBusinessEntity(usr_id, quoteDetails);
                return (usrquotesVM);
            }
        }

        //public bool PaymentDetailsAdd(PaymentVM paymentObj)
        //{
        //        using (var db = new DbQGSVLEntities())
        //        {
        //            db.tblPaymentDetails.Add(MapConfig.PaymentDetailsBusinessEntityToDataEntity(paymentObj));
        //            db.SaveChanges();
        //        }
        //        return true;
        //}
        public bool AddPersonalDetailsDL(PersonalDetailsVM personalDetailsVM)
        {
            var status = false;
            using (var db = new DbQGSVLEntities())
            {
                if (personalDetailsVM.SalarySlip != null)
                {
                    tblDocument tblDocument = MapConfig.ConvertDtoToDomain(personalDetailsVM);
                    db.tblDocuments.Add(tblDocument);
                    db.SaveChanges();
                }
                var userId = db.tblUsers.Where(x => x.UserEmail == personalDetailsVM.LoginUserEmail).Select(x => x.UserId).FirstOrDefault();

                status = MapConfig.PersonalDetailsBusinessEntityToDataEntityForUser(personalDetailsVM);
                if (status)
                {
                    var isUserExists = db.tblEmployementDetails.Where(x => x.UserId == userId).FirstOrDefault();
                    if (isUserExists == null)
                    {
                        db.tblEmployementDetails.Add(MapConfig.AddPersonalDetailsBusinessEntityToDataEntityForEmpDetails(personalDetailsVM));
                        if (!(db.SaveChanges() > 0))
                        {
                            status = false;
                        }
                    }
                    else
                    {
                        if (status)
                        {
                            status = MapConfig.UpdatePersonalDetailsBusinessEntityToDataEntityForEmpDetails(personalDetailsVM);
                        }

                    }

                }

                if (status)
                {
                    var isUserExists = db.tblRentDetails.Where(x => x.UserId == userId).FirstOrDefault();
                    if (isUserExists == null)
                    {
                        db.tblRentDetails.Add(MapConfig.AddRentDetailsBusinessEntityToDataEntity(personalDetailsVM));
                        if (!(db.SaveChanges() > 0))
                        {
                            status = false;
                        }
                    }
                    else
                    {
                        if (status)
                        {
                            status = MapConfig.UpdateRentDetailsBusinessEntityToDataEntityForEmpDetails(personalDetailsVM);
                        }

                    }
                }

                return status;
            }

        }
    }

}
