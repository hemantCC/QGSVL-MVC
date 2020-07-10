using System;

namespace QGS.Entities.BusinessEntities
{
    public class QuoteVM
    {
        public int QuoteId { get; set; }
        public Nullable<int> CarId { get; set; }
        public DateTime Date { get; set; }
        public Nullable<int> InsuranceId { get; set; }
        public Nullable<int> MileageId { get; set; }
        public Nullable<int> TotalPrice { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<int> PaybackTimeId { get; set; }



        public int UserId { get; set; }
        public Nullable<int> Term_Month_ { get; set; }
        public Nullable<int> Mileage_K_ { get; set; }
        public string Brand { get; set; }
        public string ModelName { get; set; }
        public string Status { get; set; }
        public string UserEmail { get; set; }
        public string Contact { get; set; }

        public Nullable<int> PaybackTime_Month_
        {
            get; set;
        }
    }
}
