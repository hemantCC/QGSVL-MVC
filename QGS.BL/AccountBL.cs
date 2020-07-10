using System.Collections.Generic;
using QGS.DL;
using QGS.Entities.BusinessEntities;

namespace QGS.BL
{
    public class AccountBL
    {
        public AccountDL ObjectDL = new AccountDL();
        public void Register(UserVM obj)
        {
            ObjectDL.Register(obj);
        }

        public List<QuoteVM> GetAllQuotes()
        {
            List<QuoteVM> allquotes = ObjectDL.GetQuotes();
            return allquotes;
        }

        public void EditQuote(QuoteVM obj)
        {
            ObjectDL.EditQuote(obj);
        }

        public void DeleteQuote(int? id)
        {
            ObjectDL.DeleteQuote(id);
        }

    }
}
