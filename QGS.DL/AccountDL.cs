using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using QGS.Entities.BusinessEntities;
using QGS.Entities.DataEntities;

namespace QGS.DL
{
    public class AccountDL
    {

        public void Register(UserVM obj)
        {
            using (var context = new DbQGSVLEntities())
            {
                tblUser DomainModel = new tblUser()
                {
                    UserId = obj.UserId,
                    UserEmail = obj.UserEmail,
                    Password = obj.Password,
                    Prefix = obj.Prefix,
                    FirstName = obj.FirstName,
                    LastName = obj.LastName,
                    Contact = obj.Contact,
                    RoleId = obj.RoleId

                };
                context.tblUsers.Add(DomainModel);
                context.SaveChanges();
            }

        }

        public List<QuoteVM> GetQuotes()
        {
            using (var context = new DbQGSVLEntities())
            {
                List<QuoteVM> AllQuotes = new List<QuoteVM>();
                var tblQuotes = context.tblQuotes.Include(t => t.tblInsurance).Include(t => t.tblMileage).Include(t => t.tblUser).Include(t => t.tblVehicleDetail).Include(t => t.tblStatu).Include(t => t.tblPaybackTime).ToList();
                foreach (var qt in tblQuotes)
                {
                    var quote = new QuoteVM()
                    {
                        QuoteId = qt.QuoteId,
                        UserId = qt.tblUser.UserId,
                        CarId = qt.CarId,
                        InsuranceId = qt.InsuranceId,
                        MileageId = qt.MileageId,
                        StatusId = qt.StatusId,
                        UserEmail = qt.tblUser.UserEmail,
                        Brand = qt.tblVehicleDetail.Brand,
                        ModelName = qt.tblVehicleDetail.ModelName,
                        Date = qt.Date,
                        PaybackTimeId = qt.tblPaybackTime.PaybackTimeId,
                        PaybackTime_Month_ = qt.tblPaybackTime.PaybackTime_Month_,
                        Term_Month_ = qt.tblInsurance.Term_Month_,
                        Mileage_K_ = qt.tblMileage.Mileage_K_,
                        TotalPrice = qt.TotalPrice,
                        Status = qt.tblStatu.Status,
                        Contact = qt.tblUser.Contact
                    };
                    AllQuotes.Add(quote);
                }
                return AllQuotes;
            }

        }


        public void EditQuote(QuoteVM obj)
        {
            using (var context = new DbQGSVLEntities())
            {
                var recordDomain = new tblQuote()
                {
                    QuoteId = obj.QuoteId,
                    UserId = obj.UserId,
                    CarId = obj.CarId,
                    InsuranceId = obj.InsuranceId,
                    MileageId = obj.MileageId,
                    StatusId = obj.StatusId,
                    Date = obj.Date,
                    PaybackTimeId = obj.PaybackTimeId,
                    TotalPrice = obj.TotalPrice,
                };
                var record = context.tblQuotes.Find(obj.QuoteId);
                record.StatusId = recordDomain.StatusId;
                context.Entry(record).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteQuote(int? id)
        {
            using (var context = new DbQGSVLEntities())
            {
                tblQuote tblQuote = context.tblQuotes.Find(id);
                context.tblQuotes.Remove(tblQuote);
                context.SaveChanges();
            }

        }

    }
}
