using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using QGS.Entities.BusinessEntities;

namespace QGS.Controllers
{
    [Authorize(Roles = "User")]
    public class StepController : Controller
    {
        [HttpGet]
        // GET: Step
        public ActionResult DirectDebit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DirectDebit(BankDetailVM bankDetail)
        {
            var email = User.Identity.Name;
            HttpResponseMessage response = GlobalVariable.WebApiClient.PostAsJsonAsync("DirectDebit?username=" + email, bankDetail).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("Documents", "Step");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Documents()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Documents(DocumentsVM documentsVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(documentsVM);
                }
                DocumentPathVM DocumentPath = new DocumentPathVM();
                if (documentsVM.File1.ContentLength > 0)
                {
                    string _FileName1 = Path.GetFileNameWithoutExtension(documentsVM.File1.FileName);
                    string extension = Path.GetExtension(documentsVM.File1.FileName);
                    _FileName1 = _FileName1 + DateTime.Now.ToString("yymmssfff") + extension;
                    string _path1 = Path.Combine(Server.MapPath("~/App_Data/Documents"), _FileName1);
                    DocumentPath.FilePath1 = "~/App_Data/Documents" + _FileName1;
                    documentsVM.File1.SaveAs(_path1);

                    string _FileName2 = Path.GetFileNameWithoutExtension(documentsVM.File2.FileName);
                    string extension2 = Path.GetExtension(documentsVM.File2.FileName);
                    _FileName2 = _FileName2 + DateTime.Now.ToString("yymmssfff") + extension2;
                    string _path2 = Path.Combine(Server.MapPath("~/App_Data/Documents"), _FileName2);
                    DocumentPath.FilePath2 = "~/App_Data/Documents" + _FileName2;
                    documentsVM.File2.SaveAs(_path2);

                    string _FileName3 = Path.GetFileNameWithoutExtension(documentsVM.File3.FileName);
                    string extension3 = Path.GetExtension(documentsVM.File3.FileName);
                    _FileName3 = _FileName3 + DateTime.Now.ToString("yymmssfff") + extension3;
                    string _path3 = Path.Combine(Server.MapPath("~/App_Data/Documents"), _FileName3);
                    DocumentPath.FilePath3 = "~/App_Data/Documents" + _FileName3;
                    documentsVM.File3.SaveAs(_path3);
                }
                var email = User.Identity.Name;
                HttpResponseMessage response = GlobalVariable.WebApiClient.PostAsJsonAsync("Documents?username=" + email, DocumentPath).Result;
                return RedirectToAction("ESign", "Step");
            }
            catch
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult ESign()
        {
            return View();
        }
    }
}