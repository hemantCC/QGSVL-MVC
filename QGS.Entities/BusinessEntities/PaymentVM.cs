using System;

namespace QGS.Entities.BusinessEntities
{
    public class PaymentVM
    {
        public int QuoteId { get; set; }
        public Nullable<int> PaymentMethodId { get; set; }
        public string Address { get; set; }

    }
}
