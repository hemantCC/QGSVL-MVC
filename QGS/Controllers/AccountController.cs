using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using QGS.Entities.BusinessEntities;

namespace QGS.Controllers
{
    public class AccountController : Controller
    {

        [Authorize(Roles = "user")]
        public ActionResult Home()
        {
            return View();
        }
        // GET: Account
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl = null)
        {

            TempData["ReturnUrl"] = returnUrl;
            //Redirect to Home if already logged in
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("admin"))
                {
                    return RedirectToAction("Index", "Administrator");
                }
                else if (User.IsInRole("user"))
                {
                    return RedirectToAction("Login", "Account");
                }

            }

            return View();


        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UserVM obj)
        {
            string ReturnUrl = (string)TempData["ReturnUrl"];
            
            try
            {
                if (obj == null)
                {
                    TempData["LoginFail"] = "Fail";
                    return View();
                }
                else
                {
                    string EncPass = EncryptData(obj.Password, "aqwe");
                    obj.Password = EncPass;
                    HttpResponseMessage response = GlobalVariable.WebApiClient.PostAsJsonAsync("Login", obj).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        FormsAuthentication.SetAuthCookie(obj.UserEmail, false);
                        TempData["LoginSuccess"] = "Success";

                        if (ReturnUrl != null)
                        {
                            if (ReturnUrl == "/car/MyQuote?usr_email=")
                            {
                                ReturnUrl = ReturnUrl + obj.UserEmail;
                                return Redirect(ReturnUrl);
                            }
                            if (ReturnUrl == "1")
                            {
                                return RedirectToAction("CarDetails", "Car");
                            }
                            return Redirect(ReturnUrl);
                        }

                        return RedirectToAction("HomeCarDetails", "Car");

                    }

                    TempData["LoginFail"] = "Fail";
                    return View();
                }

            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(UserVM obj)
        {
            try
            {
                bool v = false;
                if (ModelState.IsValid)
                {
                    HttpResponseMessage MailAlreadyExists = GlobalVariable.WebApiClient.GetAsync("GetAllMails").Result;
                    string[] mailalreadyexists = MailAlreadyExists.Content.ReadAsAsync<string[]>().Result;
                    foreach (var email in mailalreadyexists)
                    {
                        if (email == obj.UserEmail)
                        {
                            v = true;
                            break;
                        }
                    }
                    if (v)
                    {
                        TempData["MailAlreadyExists"] = "True";
                        return View();
                    }
                    else
                    {
                        string EncPass = EncryptData(obj.Password, "aqwe");
                        obj.Password = EncPass;
                        obj.RoleId = 2;
                        HttpResponseMessage response = GlobalVariable.WebApiClient.PostAsJsonAsync("Register", obj).Result;
                        TempData["RegistrationSuccess"] = "success";
                        return RedirectToAction("Login");
                    }

                }
                TempData["RegistrationFail"] = "Fail";
                return View();
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            TempData["LogoutSuccess"] = "Logout";
            return RedirectToAction("HomeCarDetails", "Car");
        }

        //Password Encryption Method
        public string EncryptData(string textData, string Encryptionkey)
        {
            RijndaelManaged objrij = new RijndaelManaged();
            //set the mode for operation of the algorithm
            objrij.Mode = CipherMode.CBC;
            //set the padding mode used in the algorithm.
            objrij.Padding = PaddingMode.PKCS7;
            //set the size, in bits, for the secret key.
            objrij.KeySize = 0x80;
            //set the block size in bits for the cryptographic operation.
            objrij.BlockSize = 0x80;
            //set the symmetric key that is used for encryption & decryption.
            byte[] passBytes = Encoding.UTF8.GetBytes(Encryptionkey);
            //set the initialization vector (IV) for the symmetric algorithm
            byte[] EncryptionkeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            int len = passBytes.Length;
            if (len > EncryptionkeyBytes.Length)
            {
                len = EncryptionkeyBytes.Length;
            }
            Array.Copy(passBytes, EncryptionkeyBytes, len);
            objrij.Key = EncryptionkeyBytes;
            objrij.IV = EncryptionkeyBytes;
            //Creates symmetric AES object with the current key and initialization vector IV.
            ICryptoTransform objtransform = objrij.CreateEncryptor();
            byte[] textDataByte = Encoding.UTF8.GetBytes(textData);
            //Final transform the test string.
            return Convert.ToBase64String(objtransform.TransformFinalBlock(textDataByte, 0, textDataByte.Length));
        }
    }
}