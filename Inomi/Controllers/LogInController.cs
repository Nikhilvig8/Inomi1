using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using DataLayer;
using Models;
using Newtonsoft.Json;
using System.Xml;
using System.Data;


namespace Inomi.Controllers
{
    public class LogInController : Controller
    {
        // GET: LogIn
        public ActionResult LogIn()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            else
            {
                ViewBag.Message = "";
            }

            return View();
        }

        [HttpPost]
        public ActionResult LoginCheck(string username, string password)
        {
            Users user = new Users();
            user.EmailId = username.ToString();
            user.Password = password.ToString();
            TempData["Message"] = "";
            if (user.EmailId == null || user.EmailId == "")
            {
                TempData["Message"] = "Please enter user name.";
                return RedirectToAction("Login", "Login");
            }
            if (user.Password == null || user.Password == "")
            {
                TempData["Message"] = "Please enter password.";
                return RedirectToAction("Login", "Login");
            }

            try
            {
                if (user.EmailId != null && user.Password != null)
                {
                    DataTable dtUser = new DataTable();
                    dtUser = LoginCheckCon.LoninDetails(user.EmailId, user.Password);
                    string login = dtUser.Rows[0]["login"].ToString();
                    if (login == "1")
                    {
                        Session["UserId"] = dtUser.Rows[0]["UserId"].ToString();
                        Session["UserCode"] = dtUser.Rows[0]["UserCode"].ToString();
                        Session["UserName"] = dtUser.Rows[0]["UserName"].ToString();
                        Session["EmailId"] = dtUser.Rows[0]["EmailId"].ToString();
                        Session["UserType"] = dtUser.Rows[0]["UserType"].ToString();
                        Session["UserRole"] = dtUser.Rows[0]["UserRole"].ToString();
                        Session["Picture"] = dtUser.Rows[0]["Picture"].ToString();
                        Session["UserTypeId"] = dtUser.Rows[0]["UserTypeId"].ToString();
                        if (Session["UserRole"].ToString() == "Admin")
                        {
                            return RedirectToAction("Dashboard", "Dashboard");
                        }
                        if (Session["UserRole"].ToString() == "Counsellor")
                        {
                            return RedirectToAction("CounsellorDashboard", "CounsellorDashboard");
                        }
                        if (Session["UserRole"].ToString() == "Student")
                        {
                            return RedirectToAction("Index", "Student");
                        }
                        if (Session["UserRole"].ToString() == "Father")
                        {
                            return RedirectToAction("Index", "Student");
                        }
                        if (Session["UserRole"].ToString() == "Mother")
                        {
                            return RedirectToAction("Index", "Student");
                        }

                    }
                    else
                    {
                        TempData["Message"] = "Please check username and password.";
                        return RedirectToAction("Login", "Login");
                    }

                }


            }
            catch
            {

            }
            return View();


        }

        public ActionResult LogOut()
        {
            Session["UserId"] = null;
            Session["UserCode"] = null;
            Session["UserName"] = null;
            Session["EmailId"] = null;
            Session["UserType"] = null;
            Session["UserRole"] = null;
            Session["Picture"] = null;
            Session["UserTypeId"] = null;
            return RedirectToAction("Login", "Login");
        }
    }
}