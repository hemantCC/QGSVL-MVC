using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using QGS.Entities.BusinessEntities;
using QGS.Entities.DataEntities;

namespace QGS.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdministratorController : Controller
    {

        DbQGSVLEntities db = new DbQGSVLEntities();
        [Authorize(Roles = "admin")]
        // GET: Administrator
        public ActionResult Index()
        {
            List<QuoteVM> AllQuotes;
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("GetAllQuotes").Result;
            AllQuotes = response.Content.ReadAsAsync<List<QuoteVM>>().Result;
            return View(AllQuotes);

        }

        [Authorize(Roles = "Admin")]
        // GET: Administrator/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("GetQuote/" + id.ToString()).Result;
            QuoteVM Quote = response.Content.ReadAsAsync<QuoteVM>().Result;
            if (Quote == null)
            {
                return HttpNotFound();
            }

            HttpResponseMessage status = GlobalVariable.WebApiClient.GetAsync("GetStatus").Result;
            List<StatusVM> finalStatus = status.Content.ReadAsAsync<List<StatusVM>>().Result;
            ViewBag.StatusId = new SelectList(finalStatus, "StatusId", "Status", Quote.StatusId);
            return View(Quote);
        }

        // POST: Administrator/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuoteVM obj)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.PostAsJsonAsync("EditQuote/", obj).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
            }

            HttpResponseMessage status = GlobalVariable.WebApiClient.GetAsync("GetStatus").Result;
            List<StatusVM> finalStatus = status.Content.ReadAsAsync<List<StatusVM>>().Result;
            ViewBag.StatusId = new SelectList(finalStatus, "StatusId", "Status", obj.StatusId);
            return View(obj);
        }

        // GET: Administrator/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("GetQuote/" + id.ToString()).Result;
            QuoteVM Quote = response.Content.ReadAsAsync<QuoteVM>().Result;
            if (Quote == null)
            {
                return HttpNotFound();
            }
            return View(Quote);
        }

        // POST: Administrator/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("DeleteQuote/" + id.ToString()).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            return View("Error");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult ChangeStatus(int statusId, int Id)
        {
            tblQuote record = db.tblQuotes.Find(Id);
            record.StatusId = statusId;
            db.Entry(record).State = EntityState.Modified;
            db.SaveChanges();
            return View("Index");
        }
    }

}
