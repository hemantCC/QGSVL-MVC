using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using QGS.Entities.BusinessEntities;

namespace QGS.Controllers
{

    public class CarController : Controller
    {

        public ActionResult HomeCarDetails()
        {

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:50179/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.ConnectionClose = true;
                var response = client.GetAsync("AllCarDetails");
                var result = response.Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var cars = result.Content.ReadAsAsync<List<CarVM>>().Result;
                    return View(cars);
                }
                return View();
            }
        }

        [HttpPost]
        public ActionResult HomeCarDetails(FormCollection form)
        {
            string[] keys = form.AllKeys;
            List<string> selectedCarList = new List<string>();
            List<string> selectedTypesList = new List<string>();
            List<string> selectedFuelList = new List<string>();
            List<string> selectedTransmissionList = new List<string>();
            List<string> selectedRange = new List<string>();

            foreach (string key in keys)
            {
                string[] temp = key.Split(' ');
                if (temp[0] == "brand")
                {
                    List<string> tempList = temp.ToList();
                    tempList.Remove(temp[0]);
                    selectedCarList.Add(temp[1]);
                }
                if (temp[0] == "type")
                {
                    List<string> tempList = temp.ToList();
                    tempList.Remove(temp[0]);
                    selectedTypesList.Add(temp[1]);
                }
                if (temp[0] == "fuelType")
                {
                    List<string> tempList = temp.ToList();
                    tempList.Remove(temp[0]);
                    selectedFuelList.Add(temp[1]);
                }
                if (temp[0] == "transmission")
                {
                    List<string> tempList = temp.ToList();
                    tempList.Remove(temp[0]);
                    selectedTransmissionList.Add(temp[1]);
                }
                if (temp[0] == "range")
                {
                    List<string> tempList = temp.ToList();
                    tempList.Remove(temp[0]);
                    selectedRange.Add(temp[1]);
                }
            }
            TempData["selectedCarList"] = selectedCarList;
            TempData["selectedTypesList"] = selectedTypesList;
            TempData["selectedFuelList"] = selectedFuelList;
            TempData["selectedTransmissionList"] = selectedTransmissionList;
            TempData["selectedRange"] = selectedRange;

            return RedirectToAction("CarDetails");
        }

        [HttpGet]
        public ActionResult CarDetails()
        {
            ViewData["selectedCarList"] = TempData.Peek("selectedCarList");
            ViewData["selectedTypesList"] = TempData.Peek("selectedTypesList");
            ViewData["selectedFuelList"] = TempData.Peek("selectedFuelList");
            ViewData["selectedTransmissionList"] = TempData.Peek("selectedTransmissionList");
            ViewData["selectedRange"] = TempData.Peek("selectedRange");
            //TempData.Remove("selectedCarList"); 


            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:50179/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.ConnectionClose = true;
                var response = client.GetAsync("AllCarDetails");
                var result = response.Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var cars = result.Content.ReadAsAsync<List<CarVM>>().Result;
                    return View(cars);
                }
                return View();
            }
        }

        [Authorize(Roles ="User")]
        [HttpGet]
        public ActionResult GetCarDetails(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50179/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.ConnectionClose = true;
                var response = client.GetAsync("GetCarDetail/" + id);
                var result = response.Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var one_car = result.Content.ReadAsAsync<CarVM>().Result;
                    return View(one_car);
                }
                return View();
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public ActionResult MyQuote(string usr_email)
        {
            ViewBag.Error = "Currently no quotes are available";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50179/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.ConnectionClose = true;
                var response = client.GetAsync("GetQuoteDetails?usr_email=" + usr_email);
                var result = response.Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var quoteVM = result.Content.ReadAsAsync<UsrQuoteVM>().Result;
                    return View(quoteVM);
                }
                return View();
            }
        }

        [Authorize(Roles = "User")]
        public JsonResult SelectedCar(CarVM car)
        {
            TempData["car"] = car;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50179/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.ConnectionClose = true;
                var response = client.PostAsJsonAsync("PostQuoteAdd", car);
                var result = response.Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return Json(new { success = true, responseText = "Your message successfuly sent!" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false, responseText = "your Quote could not be saved at this time" }, JsonRequestBehavior.AllowGet);

        }


        [Authorize(Roles = "User")]
        [HttpGet]
        public ActionResult CreditCheck()
        {

            var userEmail = User.Identity.Name;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50179/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.ConnectionClose = true;
                var response = client.GetAsync("PersonalDetails?userEmail=" + userEmail);
                var result = response.Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var personalDetails = result.Content.ReadAsAsync<PersonalDetailsVM>().Result;
                    return View(personalDetails);
                }
                return View();
            }

        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public ActionResult CreditCheck(FormCollection form,HttpPostedFileBase SlipFile)
        {
            HttpPostedFileBase Slip = SlipFile;
            PersonalDetailsVM personalDetailsVM = new PersonalDetailsVM()
            {
                LoginUserEmail = User.Identity.Name,
                DateOfBirth = DateTime.Parse(form.Get("dob")),
                MaritalStatusVal = Int32.Parse(form.Get("MaritalStatusVal")),
                NationalityVal = Int32.Parse(form.Get("NationalityVal")),
                RegistrationNo = Int32.Parse(form.Get("RegistrationNo")),
                UserEmail = form.Get("UserEmail"),
                Address1 = form.Get("Address1"),
                Address2 = form.Get("Address2"),
                Address3 = form.Get("Address3"),
                City = form.Get("City"),
                PostCode = Int32.Parse(form["PostCode"]),
                LivedSince = DateTime.Parse(form.Get("liveddate")),
                EmployementDetails = new EmployementDetailsVM()
                {
                    EmployeeName = form.Get("EmployeeName"),
                    EmployementStatusVal = Int32.Parse(form.Get("EmployementStatusVal")),
                    ContractTypesVal = Int32.Parse(form.Get("ContractTypesVal")),
                    Address1 = form.Get("empAddress1"),
                    Address2 = form.Get("empAddress2"),
                    Country = form.Get("empCountry"),
                    City = form.Get("empCity"),
                    empPostCode = Int32.Parse(form.Get("empPostCode")),
                    StartDate = DateTime.Parse(form.Get("startdate"))
                },
                RentDetails = new RentDetailsVM()
                {
                    NetMonthlyIncome = Int32.Parse(form.Get("NetMonthlyIncome")),
                    RentalIncome = Int32.Parse(form.Get("RentalIncome")),
                    CreditCost = Int32.Parse(form.Get("CreditCost")),
                    MonthyRent = Int32.Parse(form.Get("MonthlyRent")),
                    PropertyStatus = Int32.Parse(form.Get("PropertyStatusVal"))
                }
            };

            if (Slip.ContentLength > 0)
            {
                string _FileName1 = Path.GetFileNameWithoutExtension(Slip.FileName);
                string extension = Path.GetExtension(Slip.FileName);
                _FileName1 = _FileName1 + DateTime.Now.ToString("yymmssfff") + extension;
                string _path1 = Path.Combine(Server.MapPath("~/App_Data/Documents"), _FileName1);
                personalDetailsVM.SalarySlip = "~/App_Data/Documents" + _FileName1;
                Slip.SaveAs(_path1);
            }
                using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50179/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.ConnectionClose = true;
                var response = client.PostAsJsonAsync("AddPersonalDetails", personalDetailsVM);
                var result = response.Result;
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction("EndOfCreditCheck");
                }
            }
            return RedirectToAction("ErrorPage");
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public ActionResult EndOfCreditCheck()
        {

            ViewData["car"] = TempData.Peek("car");
            if (ViewData["car"] == null)
            {
                ViewBag.Error = "Currently quote is not available";
            }
            return View();
        }

        public ViewResult FAQs()
        {
            return View();
        }
    }
}