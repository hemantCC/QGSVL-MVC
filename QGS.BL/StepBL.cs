using QGS.DL;
using QGS.Entities.BusinessEntities;

namespace QGS.BL
{
    public class StepBL
    {
        private StepDL stepDL = new StepDL();
        public void DirectDebit(string username, BankDetailVM bankDetailVM)
        {
            stepDL.DirectDebit(username, bankDetailVM);
        }

        public void Documents(string username, DocumentPathVM documentPath)
        {
            stepDL.Documents(username, documentPath);
        }
    }
}
