using System;
using System.Collections.Generic;

namespace QGS.Entities.BusinessEntities
{
    public class UsrQuoteVM
    {
        public int UserId { get; set; }
        public List<QuoteDetail> quoteDetails { get; set; }

    }

    public class QuoteDetail
    {
        public string Brand { get; set; }
        public string ModelName { get; set; }

        public string CarImage { get; set; }
        public string transmission { get; set; }
        public string fuelType { get; set; }
        public string horsePower { get; set; }
        public string co2Emission { get; set; }
        public string color { get; set; }
        public System.DateTime Date { get; set; }

        public Nullable<int> Term_Month_ { get; set; }
        public Nullable<int> Mileage_K_ { get; set; }
        public int PaybackTime_Month_ { get; set; }
        public Nullable<int> TotalPrice { get; set; }
        public string Status { get; set; }
    }


}
