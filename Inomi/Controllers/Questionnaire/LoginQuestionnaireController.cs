using DataLayer;
using Hangfire;
using Inomi.Models.Questionnaire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Security;

namespace Inomi.Controllers.Questionnaire
{
    public class LoginQuestionnaireController : Controller
    {
        private InomiEntities db = new InomiEntities();

        // GET: Register/Create
        public ActionResult Register()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("TermsAndConditions", "HomeQuestionnaire");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult Register([Bind(Include = "Id,Name,Email,Age,Gender,Mobile,Address")] Register register)
        {
            if (ModelState.IsValid)
            {
                tblQuestionnaireStudentRegister tblQuestionnaireStudentRegister = new tblQuestionnaireStudentRegister();
                tblQuestionnaireStudentRegister.Name = register.Name;
                tblQuestionnaireStudentRegister.Email = register.Email;
                tblQuestionnaireStudentRegister.Age = register.Age;
                tblQuestionnaireStudentRegister.Gender = register.Gender;
                tblQuestionnaireStudentRegister.Mobile = register.Mobile;
                tblQuestionnaireStudentRegister.Address = register.Address;
                tblQuestionnaireStudentRegister.IsActive = "1";

                bool status = CheckEmail(tblQuestionnaireStudentRegister);

                if (status == true)
                {
                    return Json(new { Status = false, Error = "Email Already Exists." });
                }
                else
                {
                    db.tblQuestionnaireStudentRegisters.Add(tblQuestionnaireStudentRegister);

                    tblQuestionnaireLogin tblQuesrionnaireLogin = new tblQuestionnaireLogin();
                    tblQuesrionnaireLogin.UserName = tblQuestionnaireStudentRegister.Email;
                    tblQuesrionnaireLogin.Password = Membership.GeneratePassword(12, 2);
                    tblQuesrionnaireLogin.IsAdmin = "0";
                    db.tblQuestionnaireLogins.Add(tblQuesrionnaireLogin);

                    db.SaveChanges();

                    BackgroundJob.Enqueue(() => SendMail(tblQuesrionnaireLogin.UserName, tblQuesrionnaireLogin.UserName, tblQuesrionnaireLogin.Password, tblQuestionnaireStudentRegister.Name));

                    return Json(new { Status = true });
                }
            }
            else
            {
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        return Json(new { Status = false, Error = error.ErrorMessage });
                    }
                }
                return Json(new { Status = false });
            }

        }

        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("TermsAndConditions", "HomeQuestionnaire");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult Login([Bind(Include = "UserName,Password")] Login tblQuesrionnaireLogin)
        {
            tblQuestionnaireLogin temp = db.tblQuestionnaireLogins.Where(x => x.UserName == tblQuesrionnaireLogin.UserName && x.Password == tblQuesrionnaireLogin.Password).FirstOrDefault();

            if (temp == null)
            {
                return Json(new { Status = false });
            }
            else
            {
                tblQuestionnaireStudentRegister regis = db.tblQuestionnaireStudentRegisters.Where(x => x.Email == tblQuesrionnaireLogin.UserName).FirstOrDefault();
                FormsAuthentication.SetAuthCookie(tblQuesrionnaireLogin.UserName.Trim(), false);
                return Json(new { Status = true, IsAdmin = temp.IsAdmin });
            }

        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login", "LoginQuestionnaire");
        }

        public bool CheckEmail(tblQuestionnaireStudentRegister tblQuestionnaireStudentRegister)
        {
            tblQuestionnaireStudentRegister temp = db.tblQuestionnaireStudentRegisters.Where(x => x.Email == tblQuestionnaireStudentRegister.Email).FirstOrDefault();
            if (temp == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public JsonResult ForgotPassword(string Username)
        {
            tblQuestionnaireLogin tblQuesrionnaireLogin = db.tblQuestionnaireLogins.Where(x => x.UserName == Username).FirstOrDefault();
            if (tblQuesrionnaireLogin != null)
            {
                tblQuestionnaireStudentRegister tblQuestionnaireStudentRegister = db.tblQuestionnaireStudentRegisters.Where(x => x.Email == Username).FirstOrDefault();
                BackgroundJob.Enqueue(() => SendMail(tblQuesrionnaireLogin.UserName, tblQuesrionnaireLogin.UserName, tblQuesrionnaireLogin.Password, tblQuestionnaireStudentRegister.Name));

                return Json(new { Status = true });
            }
            else
            {
                return Json(new { Status = false });
            }

        }

        public static bool SendMail(string toMail, string UserName, string Password, string Name)
        {
            try
            {
                string emailBody = "<!DOCTYPE html>" +
                                        "<html lang='en'>" +
                                          "<head>" +
                                            "<meta charset='utf-8'/>" +
                                            "<meta name='viewport' content='width=device-width, initial-scale=1'/>" +
                                            "<title>A Message to our Customers</title>" +
                                            "<link  href='https://fonts.googleapis.com/css?family=Roboto:400,500,700&display=swap' rel='stylesheet'  />" +
                                          "</head>" +
                                          "<body style='background-color: #fff;margin: 0;padding: 0;font-family: roboto, sans-serif;padding: 0;font-weight: 400;'>" +
                                            "<table border ='0' " +
                                              "cellpadding='0'" +
                                              "cellspacing ='0'" +
                                              "style = 'background: #fefefe url(https://www.teamcomputers.com/repositry/edm/html-edm/inomi/images//bg.png) no-repeat; font-size: 1rem;line-height: 1.4;max-width: 650px;width: 100%;margin: 0 auto;border-collapse: collapse;border: 1px solid #e5e5e5;' >" +
                                              "<tr border-spacing='0' cellspacing='0'>" +
                                                "<td border-spacing='0' cellspacing='0'> " +
                                                  "<table border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse;' >" +
                                                   "<tr>" +
                                                      "<td style='padding: 1rem 1.8rem;'>" +
                                                        "<a href='http://www.inomi.in/' target='_blank'>" +
                                                            "<img src=cid:logopath alt='inomi logo' />" +
                                                        "</a>" +
                                                      "</td>" +
                                                    "</tr>" +
                                                    "<tr>" +
                                                      "<td valign='center' style='position: relative;'></td>" +
                                                     "</tr>" +
                                                    "<tr>" +
                                                      "<td style='padding: 2rem 1.8rem 3.5rem; letter-spacing: 0.2px; '>" +
                                                         "<p  style='font-size: 18px;font-weight: 700;color: #1b2d4e;margin-bottom: 0;'>" +
                                                         "Hello " + Name +
                                                        "</p>" +
                                                        "<p style='font-size: 16px;max-width: 325px;margin-top: 0.6rem;line-height: 1.8;'>" +
                                                            "Thank you for registering for " +
                                                            "<strong style='color: #10c1ff; text-decoration: underline;'>" +
                                                            "Inomi Career Counselling Test </strong>" +
                                                        "</p>" +
                                                        "</td>" +
                                                    "</tr>" +
                                                    "</table>" +
                                                "</td>" +
                                                "</tr>" +
                                                "<tr>" +
                                                "<td style='padding: 0 1.8rem; '>" +
                                                     "<table  style='border: 1px solid #e5e5e5;background-color: #fff;width: 100%;order-radius: 5px;margin-bottom: 0.5rem;'>" +
                                                    "<tr>" +
                                                        "<td style='color: #1b2d4e;font-size: 18px;font-weight: 700;padding: 1.5rem 0 0.4rem 0.5rem;'> " +
                                                        "User Information" +
                                                        "</td> " +
                                                    "</tr> " +
                                                    "<tr> " +
                                                        "<td style='padding-bottom: 1.3rem;'>" +
                                                        "<table style='width: 100%;font-size: 16px;border-collapse: separate;border-spacing: 10px 5px;'>" +
                                                            "<tr>" +
                                                            "<td style='width: 50%;background-color: rgba(16, 193, 255, 0.25);padding: 10px 16px;border-radius: 5px;font-weight: 700;'>" +
                                                                "URL :" +
                                                            "</td>" +
                                                            "<td style='width: 50%;border: 1px solid #efefef;border-radius: 5px;padding: 10px 16px;'>http://115.112.157.139:58000/LoginQuestionnaire" +
                                                            "</td>" +
                                                            "</tr>" +
                                                            "<tr>" +
                                                            "<td style='width: 50%;background-color: rgba(16, 193, 255, 0.25);padding: 10px 16px;border-radius: 5px;font-weight: 700;'>" +
                                                                "Email :" +
                                                            "</td>" +
                                                            "<td style='width: 50%;border: 1px solid #efefef;border-radius: 5px;padding: 10px 16px;'>" +
                                                                UserName +
                                                            "</td>" +
                                                            "</tr>" +
                                                            "<tr>" +
                                                            "<td style='width: 50%;background-color: rgba(16, 193, 255, 0.25);padding: 10px 16px;border-radius: 5px;font-weight: 700;'>" +
                                                                "Password :" +
                                                            "</td>" +
                                                            "<td style='width: 50%;border: 1px solid #efefef;border-radius: 5px;padding: 10px 16px;'>" +
                                                                Password +
                                                            "</td>" +
                                                            "</tr>" +
                                                        "</table>" +
                                                        "</td>" +
                                                    "</tr> " +
                                                    "</table> " +
                                                "</td>" +
                                                "</tr>" +
                                                "<tr>" +
                                                "<td>" +
                                                    "<table border='0' cellspacing='0' style='background-color: #262625;width: 100%;border-collapse: collapse;border-top-left-radius: 5px;border-top-right-radius: 5px;'>" +
                                                    "<tr>" +
                                                        "<td style='padding: 1rem 0 1rem; '>" +
                                                         "<p style='font-size: 14px;color: #fff;text-align: center;margin-bottom: 0;'>" +
                                                            "10, Arjun Marg, DLF Phase I, Gurgaon 122002 | +919811923388" +
                                                        "</p> " +
                                                        "</td> " +
                                                    "</tr> " +
                                                    "</table> " +
                                                "</td> " +
                                                "</tr> " +
                                                "<tr style='background-color: #262625;'> " +
                                                "<td style='padding-bottom: 1rem;'> " +
                                                    "<table border='0' cellspacing='0' style=' width: 100%;max-width: 120px;margin: 0 auto;border-collapse: collapse;'>" +
                                                    "<tr>" +
                                                        "<td>" +
                                                        "<a href='#' target='_blank'>" +
                                                            "<img src=cid:faicon alt='fa icon' style='margin: 0 auto; display: block;'/> " +
                                                        "</a>" +
                                                        "</td>" +
                                                        "<td>" +
                                                        "<a href='#' target='_blank'>" +
                                                            "<img src=cid:liicon alt='team logo' style='margin: 0 auto; display: block;' />" +
                                                        "</a>" +
                                                        "</td>" +
                                                        "<td>" +
                                                        "<a href='#'>" +
                                                            "<img src=cid:twicon alt='team logo' style='margin: 0 auto; display: block;' /> " +
                                                        "</a>" +
                                                        "</td>" +
                                                    "</tr>" +
                                                    "</table>" +
                                                "</td>" +
                                                "</tr>" +
                                                "<tr>" +
                                                "<td>" +
                                                    "<table border='0' cellspacing='0' style='background-color: #262625;width: 100%; border-collapse: collapse;'> " +
                                                    "<tr>" +
                                                        "<td style='padding: 0 0 1rem; '>" +
                                                         "<p  style='font-size: 12px;color: #73819a;text-align: center;margin-top: 0;'> " +
                                                            "© 2020 All rights reserved." +
                                                        "</p>" +
                                                        "</td>" +
                                                    "</tr>" +
                                                    "</table>" +
                                                "</td>" +
                                                "</tr>" +
                                            "</table>" +
                                            "</body>" +
                                        "</html>";


                AlternateView avHtml = AlternateView.CreateAlternateViewFromString
                                (emailBody, null, "text/html");

                LinkedResource inline = new LinkedResource(HostingEnvironment.MapPath(@"/images/inomi-logo.png"), MediaTypeNames.Image.Jpeg);
                inline.ContentId = "logopath";
                avHtml.LinkedResources.Add(inline);

                LinkedResource fa = new LinkedResource(HostingEnvironment.MapPath(@"/images/fa-icon.png"), MediaTypeNames.Image.Jpeg);
                fa.ContentId = "faicon";
                avHtml.LinkedResources.Add(fa);

                LinkedResource li = new LinkedResource(HostingEnvironment.MapPath(@"/images/li-icon.png"), MediaTypeNames.Image.Jpeg);
                li.ContentId = "liicon";
                avHtml.LinkedResources.Add(li);

                LinkedResource tw = new LinkedResource(HostingEnvironment.MapPath(@"/images/tw-icon.png"), MediaTypeNames.Image.Jpeg);
                tw.ContentId = "twicon";
                avHtml.LinkedResources.Add(tw);

                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new System.Net.Mail.MailAddress(senderEmail);
                mailMessage.To.Add(new System.Net.Mail.MailAddress(toMail));
                mailMessage.Subject = "Inomi Carrer Counselling Test Registeration";

                mailMessage.AlternateViews.Add(avHtml);

                mailMessage.Body = "Inomi - 'You At your True Best'";

                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;

                client.Send(mailMessage);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}