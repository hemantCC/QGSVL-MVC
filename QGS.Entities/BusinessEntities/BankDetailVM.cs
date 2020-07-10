using System;

namespace QGS.Entities.BusinessEntities
{
    public class BankDetailVM
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> QuoteId { get; set; }
        public string IBAN { get; set; }
    }
}
