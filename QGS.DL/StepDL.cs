using System.Linq;
using QGS.Entities.BusinessEntities;
using QGS.Entities.DataEntities;

namespace QGS.DL
{
    public class StepDL
    {
        public void DirectDebit(string username, BankDetailVM bankDetailVM)
        {

            using (var context = new DbQGSVLEntities())
            {
                var userId = context.tblUsers.Where(x => x.UserEmail == username).Select(x => x.UserId).FirstOrDefault();
                var quoteId = context.tblQuotes.Where(x => x.UserId == userId).OrderByDescending(x => x.QuoteId).Select(x => x.QuoteId).FirstOrDefault();

                tblBankDetail DomainBankDetail = new tblBankDetail()
                {
                    Id = bankDetailVM.Id,
                    UserId = bankDetailVM.UserId,
                    QuoteId = quoteId,
                    IBAN = bankDetailVM.IBAN
                };
                var UserId = context.tblUsers.Where(x => x.UserEmail == username).Select(x => x.UserId).FirstOrDefault();
                DomainBankDetail.UserId = UserId;
                context.tblBankDetails.Add(DomainBankDetail);
                context.SaveChanges();
            }
        }

        public void Documents(string username, DocumentPathVM documentPath)
        {
            using (var context = new DbQGSVLEntities())
            {
                var userId = context.tblUsers.Where(x => x.UserEmail == username).Select(x => x.UserId).FirstOrDefault();
                var quoteId = context.tblQuotes.Where(x => x.UserId == userId).OrderByDescending(x => x.QuoteId).Select(x => x.QuoteId).FirstOrDefault();
                tblDocument Document1 = new tblDocument()
                {
                    DocumentType = 2,
                    DocumentPath = documentPath.FilePath1,
                    QuoteId = quoteId
                };
                tblDocument Document2 = new tblDocument()
                {
                    DocumentType = 3,
                    DocumentPath = documentPath.FilePath2,
                    QuoteId = quoteId
                };
                tblDocument Document3 = new tblDocument()
                {
                    DocumentType = 3,
                    DocumentPath = documentPath.FilePath3,
                    QuoteId = quoteId
                };
                context.tblDocuments.Add(Document1);
                context.tblDocuments.Add(Document2);
                context.tblDocuments.Add(Document3);
                context.SaveChanges();
            }
        }

    }
}
