using System;
using System.Collections.Generic;
using System.Linq;
using QGS.Entities.BusinessEntities;
using QGS.Entities.DataEntities;


namespace QGS.DL
{
    public class MapConfig
    {

        public static CarVM CarDetailsToBusinessEntity(tblVehicleDetail tblVehicleDetails)
        {
            var car = new CarVM()
            {
                CarId = tblVehicleDetails.CarId,
                Image = tblVehicleDetails.Image,
                Type = tblVehicleDetails.Type,
                Brand = tblVehicleDetails.Brand,
                ModelName = tblVehicleDetails.ModelName,
                HorsePower_HP_ = tblVehicleDetails.HorsePower_HP_,
                Consumption_L_100Km_ = tblVehicleDetails.Consumption_L_100Km_,
                Co2Emission_g_Km_ = tblVehicleDetails.Co2Emission_g_Km_,
                FuelType = tblVehicleDetails.FuelType,
                Seating = tblVehicleDetails.Seating,
                Abs = tblVehicleDetails.Abs,
                Airbag = tblVehicleDetails.Airbag,
                Transmission = tblVehicleDetails.Transmission,
                Colour = tblVehicleDetails.Colour,
                Price = tblVehicleDetails.Price
            };
            using (var db = new DbQGSVLEntities())
            {
                var MainEquipment = db.tblEquipments.Where(x => x.CarId == tblVehicleDetails.CarId && x.EquipmentType == "Main").FirstOrDefault();
                var StandardEquipment = db.tblEquipments.Where(x => x.CarId == tblVehicleDetails.CarId && x.EquipmentType == "Standard").FirstOrDefault();
                var IncludedEquipment = db.tblIncludedServices.ToList();
                car.ServiceName = null;
                IList<Insurance> ins = db.tblInsurances.Include("tblInsurance").Select(
                    x => new Insurance()
                    {
                        InsuranceId = x.InsuranceId,
                        Term_Month_ = x.Term_Month_
                    }
                    ).ToList<Insurance>();
                IList<Mileage> mileages = db.tblMileages.Include("tblMileage").Select(
                   x => new Mileage()
                   {
                       MileageId = x.MileageId,
                       Mileage_K_ = x.Mileage_K_
                   }
                   ).ToList<Mileage>();
                IList<PayBackTime> payBackTimes = db.tblPaybackTimes.Include("tblPaybackTime").Select(
                    x => new PayBackTime()
                    {
                        PaybackTimeId = x.PaybackTimeId,
                        PaybackTime_Month_ = x.PaybackTime_Month_
                    }
                    ).ToList<PayBackTime>();
                car.insObj = ins;
                car.milObj = mileages;
                car.payObj = payBackTimes;
                if (MainEquipment != null)
                {
                    car.MainEquipment = MainEquipment.Features;
                }
                if (StandardEquipment != null)
                {
                    car.StandardEquipment = StandardEquipment.Features;
                }
                foreach (var item in IncludedEquipment)
                {
                    if (item.IsId == 1)
                    {
                        car.ServiceName = item.ServiceName;
                    }
                    else
                    {
                        car.ServiceName = car.ServiceName + ',' + item.ServiceName;
                    }
                }
            }
            return car;
        }


        public static tblQuote CarDetailsBusinessEntityToDataEntity(CarVM carVM)
        {

            tblQuote tblquote = new tblQuote
            {

                CarId = carVM.CarId,
                Date = DateTime.Now,
                InsuranceId = carVM.finalIns,
                MileageId = CarDL.mileage(carVM.final_mileage),
                TotalPrice = carVM.final_price,
                StatusId = 1,
                PaybackTimeId = CarDL.time(carVM.final_time),
            };

            using (var db = new DbQGSVLEntities())
            {
                var user = db.tblUsers.Where(x => x.UserEmail == carVM.email).FirstOrDefault();
                tblquote.UserId = user.UserId;
                user.isMainDriver = carVM.isMainDriver;
                db.SaveChanges();
            }

            return tblquote;
        }







        //public static UsrQuoteVM QuoteDetailsDataEntityToBusinessEntity(tblQuote tblQuote)
        //{
        //    UsrQuoteVM usrQuoteVM = new UsrQuoteVM()
        //    {
        //        UserId = tblQuote.UserId,
        //        Date=tblQuote.Date,
        //        TotalPrice=tblQuote.TotalPrice
        //    };

        //    using (var db = new DbQGSVLEntities())
        //    {
        //        var brand = db.tblVehicleDetails.Where(x => x.CarId == tblQuote.CarId).Select(x=>x.Brand).First();
        //        var modelName= db.tblVehicleDetails.Where(x => x.CarId == tblQuote.CarId).Select(x => x.ModelName).First();
        //        var termMonth= db.tblInsurances.Where(x => x.InsuranceId == tblQuote.InsuranceId).Select(x => x.Term_Month_).First();
        //        var mileage= db.tblMileages.Where(x => x.MileageId == tblQuote.MileageId).Select(x => x.Mileage_K_).First();
        //        var paybacktime= db.tblPaybackTimes.Where(x => x.PaybackTimeId == tblQuote.PaybackTimeId).Select(x => x.PaybackTime_Month_).First();
        //        var status= db.tblStatus.Where(x => x.StatusId == tblQuote.StatusId).Select(x => x.Status).First();
        //        usrQuoteVM.Brand = brand;
        //        usrQuoteVM.ModelName = modelName;
        //        usrQuoteVM.Term_Month_ = termMonth;
        //        usrQuoteVM.Mileage_K_ = mileage;
        //        usrQuoteVM.PaybackTime_Month_ = paybacktime;
        //        usrQuoteVM.Status = status;
        //    }    
        //    return usrQuoteVM;
        //}

        public static QuoteDetail QuoteDetailsDataEntityToBusinessEntity(tblQuote tblQuote)
        {
            QuoteDetail quoteDetail = new QuoteDetail();
            using (var db = new DbQGSVLEntities())
            {
                var carImage = db.tblVehicleDetails.Where(x => x.CarId == tblQuote.CarId).Select(x => x.Image).First();
                var fuelType = db.tblVehicleDetails.Where(x => x.CarId == tblQuote.CarId).Select(x => x.FuelType).First();
                var brand = db.tblVehicleDetails.Where(x => x.CarId == tblQuote.CarId).Select(x => x.Brand).First();
                var transmission = db.tblVehicleDetails.Where(x => x.CarId == tblQuote.CarId).Select(x => x.Transmission).First();
                //var milege= db.tblVehicleDetails.Where(x => x.CarId == tblQuote.CarId).Select(x => x.Consumption_L_100Km_).First();
                var hp = db.tblVehicleDetails.Where(x => x.CarId == tblQuote.CarId).Select(x => x.HorsePower_HP_).First();
                var co2emission = db.tblVehicleDetails.Where(x => x.CarId == tblQuote.CarId).Select(x => x.Co2Emission_g_Km_).First();
                var color = db.tblVehicleDetails.Where(x => x.CarId == tblQuote.CarId).Select(x => x.Colour).First();
                var modelName = db.tblVehicleDetails.Where(x => x.CarId == tblQuote.CarId).Select(x => x.ModelName).First();
                var termMonth = db.tblInsurances.Where(x => x.InsuranceId == tblQuote.InsuranceId).Select(x => x.Term_Month_).First();
                var mileage = db.tblMileages.Where(x => x.MileageId == tblQuote.MileageId).Select(x => x.Mileage_K_).First();
                var paybacktime = db.tblPaybackTimes.Where(x => x.PaybackTimeId == tblQuote.PaybackTimeId).Select(x => x.PaybackTime_Month_).First();
                var status = db.tblStatus.Where(x => x.StatusId == tblQuote.StatusId).Select(x => x.Status).First();
                quoteDetail.CarImage = carImage;
                quoteDetail.fuelType = fuelType;
                quoteDetail.Brand = brand;
                quoteDetail.transmission = transmission;
                quoteDetail.horsePower = hp;
                quoteDetail.co2Emission = co2emission;
                quoteDetail.color = color;
                quoteDetail.ModelName = modelName;
                quoteDetail.Term_Month_ = termMonth;
                quoteDetail.Mileage_K_ = mileage;
                quoteDetail.PaybackTime_Month_ = paybacktime;
                quoteDetail.Status = status;
                quoteDetail.TotalPrice = tblQuote.TotalPrice;
                quoteDetail.Date = tblQuote.Date;
            }
            return quoteDetail;
        }



        public static UsrQuoteVM UsrQuoteDetailsDataEntityToBusinessEntity(int usrId, List<QuoteDetail> quoteDetails)
        {
            var usrQuoteVM = new UsrQuoteVM()
            {
                UserId = usrId,
                quoteDetails = quoteDetails
            };
            //using (var db = new DbQGSVLEntities())
            //{
            //    IList<PaymentMethod> paymentMethods = db.tblPaymentMethods.Include("tblPaymentMethod").Select(
            //      x => new PaymentMethod()
            //      {
            //          PaymentMethodId=x.PaymentMethodId,
            //          Method=x.Method
            //      }
            //      ).ToList<PaymentMethod>();
            //      usrQuoteVM.paymentMethod = paymentMethods;
            //}


            return usrQuoteVM;
        }

        //public static tblPaymentDetail PaymentDetailsBusinessEntityToDataEntity(PaymentVM paymentObj)
        //{
        //    var tblPaymentDetail = new tblPaymentDetail()
        //    {
        //        QuoteId = 1028,
        //        PaymentMethodId = paymentObj.PaymentMethodId,
        //        PaymentDate = DateTime.Now,
        //        Address = paymentObj.Address
        //    };
        //    return tblPaymentDetail;
        //}

        public static PersonalDetailsVM PersonalDetailsDataEntityToBusinessEntity(string userEmail)
        {
            PersonalDetailsVM personalDetails = new PersonalDetailsVM();
            using (var db = new DbQGSVLEntities())
            {
                IList<MaritalStatusVM> maritalStatus = db.tblMaritalStatus.Include("tblMaritalStatu").Select(
                         x => new MaritalStatusVM()
                         {
                             Id = x.Id,
                             MaritalStatus = x.Name
                         }
                         ).ToList<MaritalStatusVM>();
                IList<NationalityVM> nationalities = db.tblNationalities.Include("tblNationality").Select(
                         x => new NationalityVM()
                         {
                             Id = x.Id,
                             Nationality = x.Nationality
                         }
                         ).ToList<NationalityVM>();
                IList<EmployementStatusVM> employementStatuses = db.tblEmployementStatus.Include("tblEmployementStatu").Select(
                         x => new EmployementStatusVM()
                         {
                             Id = x.Id,
                             EmpStatus = x.Name

                         }
                         ).ToList<EmployementStatusVM>();
                IList<PropertyStatusVM> propertyStatuses = db.tblPropertyStatus.Include("tblPropertyStatu").Select(
                         x => new PropertyStatusVM()
                         {
                             Id = x.Id,
                             PropertyStatus = x.Name

                         }
                         ).ToList<PropertyStatusVM>();
                IList<ContractTypeVM> contractTypes = db.tblContractTypes.Include("tblContractType").Select(
                         x => new ContractTypeVM()
                         {
                             Id = x.Id,
                             ContractType = x.Name

                         }
                         ).ToList<ContractTypeVM>();
                var user = db.tblUsers.Where(x => x.UserEmail == userEmail).FirstOrDefault();
                personalDetails.FirstName = user.FirstName;
                personalDetails.LastName = user.LastName;
                personalDetails.Contact = user.Contact;
                personalDetails.Prefix = user.Prefix;
                personalDetails.MaritalStatus = maritalStatus;
                personalDetails.Nationality = nationalities;
                personalDetails.EmployementStatus = employementStatuses;
                personalDetails.PropertyStatus = propertyStatuses;
                personalDetails.contractTypes = contractTypes;
            }
            return personalDetails;

        }

        public static bool PersonalDetailsBusinessEntityToDataEntityForUser(PersonalDetailsVM personalDetailsVM)
        {

            using (var db = new DbQGSVLEntities())
            {
                try
                {
                    var user = db.tblUsers.Where(x => x.UserEmail == personalDetailsVM.LoginUserEmail).FirstOrDefault();
                    user.DateOfBirth = personalDetailsVM.DateOfBirth;
                    user.MaritalStatus = personalDetailsVM.MaritalStatusVal;
                    user.RegistrationNumber = personalDetailsVM.RegistrationNo;
                    user.Address1 = personalDetailsVM.Address1;
                    user.Address2 = personalDetailsVM.Address2;
                    user.Address3 = personalDetailsVM.Address3;
                    user.PostCode = personalDetailsVM.PostCode;
                    user.City = personalDetailsVM.City;
                    user.Nationality = personalDetailsVM.NationalityVal;
                    user.LivedSince = personalDetailsVM.LivedSince;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }


            }


        }

        public static tblEmployementDetail AddPersonalDetailsBusinessEntityToDataEntityForEmpDetails(PersonalDetailsVM personalDetailsVM)
        {

            tblEmployementDetail tblEmployementDetail = new tblEmployementDetail()
            {
                EmployementStatus = personalDetailsVM.EmployementDetails.EmployementStatusVal,
                EmployeeName = personalDetailsVM.EmployementDetails.EmployeeName,
                ContractType = personalDetailsVM.EmployementDetails.ContractTypesVal,
                Address1 = personalDetailsVM.EmployementDetails.Address1,
                Address2 = personalDetailsVM.EmployementDetails.Address2,
                PostCode = personalDetailsVM.EmployementDetails.empPostCode,
                Country = personalDetailsVM.EmployementDetails.Country,
                City = personalDetailsVM.EmployementDetails.City,
                StartDate = personalDetailsVM.EmployementDetails.StartDate
            };
            using (var db = new DbQGSVLEntities())
            {
                var userId = db.tblUsers.Where(x => x.UserEmail == personalDetailsVM.LoginUserEmail).Select(x => x.UserId).FirstOrDefault();
                tblEmployementDetail.UserId = userId;
            }

            return tblEmployementDetail;
        }

        public static tblDocument ConvertDtoToDomain(PersonalDetailsVM personalDetailsVM)
        {
            using (var db = new DbQGSVLEntities())
            {
                var userId = db.tblUsers.Where(x => x.UserEmail == personalDetailsVM.LoginUserEmail).Select(x => x.UserId).FirstOrDefault();
                var quoteId = db.tblQuotes.Where(x => x.UserId == userId).OrderByDescending(x => x.QuoteId).Select(x => x.QuoteId).FirstOrDefault();

                tblDocument tblDocument = new tblDocument()
                {
                    DocumentType = 1,
                    DocumentPath = personalDetailsVM.SalarySlip,
                    QuoteId = quoteId
                };


                return tblDocument;
            }
        }


        public static bool UpdatePersonalDetailsBusinessEntityToDataEntityForEmpDetails(PersonalDetailsVM personalDetailsVM)
        {
            using (var db = new DbQGSVLEntities())
            {
                try
                {
                    var userId = db.tblUsers.Where(x => x.UserEmail == personalDetailsVM.LoginUserEmail).Select(x => x.UserId).FirstOrDefault();
                    var empDetails = db.tblEmployementDetails.Where(x => x.UserId == userId).FirstOrDefault();
                    empDetails.EmployementStatus = personalDetailsVM.EmployementDetails.EmployementStatusVal;
                    empDetails.EmployeeName = personalDetailsVM.EmployementDetails.EmployeeName;
                    empDetails.ContractType = personalDetailsVM.EmployementDetails.ContractTypesVal;
                    empDetails.Address1 = personalDetailsVM.EmployementDetails.Address1;
                    empDetails.Address2 = personalDetailsVM.EmployementDetails.Address2;
                    empDetails.PostCode = personalDetailsVM.EmployementDetails.empPostCode;
                    empDetails.Country = personalDetailsVM.EmployementDetails.Country;
                    empDetails.City = personalDetailsVM.EmployementDetails.City;
                    empDetails.StartDate = personalDetailsVM.EmployementDetails.StartDate;
                    db.SaveChanges();
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }


            }

        }

        public static tblRentDetail AddRentDetailsBusinessEntityToDataEntity(PersonalDetailsVM personalDetailsVM)
        {
            tblRentDetail tblRentDetail = new tblRentDetail()
            {
                NetMonthyIncome = personalDetailsVM.RentDetails.NetMonthlyIncome,
                RentalIncome = personalDetailsVM.RentDetails.RentalIncome,
                PropertyStatus = personalDetailsVM.RentDetails.PropertyStatus,
                CreditCost = personalDetailsVM.RentDetails.CreditCost,
                MonthyRent = personalDetailsVM.RentDetails.MonthyRent
            };
            using (var db = new DbQGSVLEntities())
            {
                var userId = db.tblUsers.Where(x => x.UserEmail == personalDetailsVM.LoginUserEmail).Select(x => x.UserId).FirstOrDefault();
                tblRentDetail.UserId = userId;
            }
            return tblRentDetail;
        }


        public static bool UpdateRentDetailsBusinessEntityToDataEntityForEmpDetails(PersonalDetailsVM personalDetailsVM)
        {
            try
            {
                using (var db = new DbQGSVLEntities())
                {
                    var userId = db.tblUsers.Where(x => x.UserEmail == personalDetailsVM.LoginUserEmail).Select(x => x.UserId).FirstOrDefault();
                    var rentDetails = db.tblRentDetails.Where(x => x.UserId == userId).FirstOrDefault();
                    rentDetails.NetMonthyIncome = personalDetailsVM.RentDetails.NetMonthlyIncome;
                    rentDetails.RentalIncome = personalDetailsVM.RentDetails.RentalIncome;
                    rentDetails.PropertyStatus = personalDetailsVM.RentDetails.PropertyStatus;
                    rentDetails.CreditCost = personalDetailsVM.RentDetails.CreditCost;
                    rentDetails.MonthyRent = personalDetailsVM.RentDetails.MonthyRent;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }


        }








    }
}

