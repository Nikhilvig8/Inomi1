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
using System.IO;

namespace Inomi.Controllers
{
    public class MyProfileController : Controller
    {
        [SessionExpireAttribute]
        // GET: MyProfile
        public ActionResult MyProfile(string StrMain)
        {
            string UsertypeId = Session["UserTypeId"].ToString();

            if (StrMain == null || StrMain == "")
            {
                TempData["Message"] = "";
            }
            else
            {
                TempData["Message"] = StrMain;
            }
            DataTable dtCountryList = new DataTable();

            dtCountryList = StudentCon.CountryList();
            ViewBag.CountryList = ToSelectList(dtCountryList, "Id", "Name");

            DataTable objStudent = new DataTable();
            objStudent = MyProfileCon.GetMyProfile(UsertypeId);
            return View(objStudent);
        }

        public void InsertMyProfileAbount(string Id, string Phone, string country, string city, string Picture)
        {
            string filePath = string.Empty;
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;

                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        fname = Generate() + fname;
                        filePath = "/Uploads/" + fname + "";
                        fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);

                        file.SaveAs(fname);
                    }
                }
                catch (Exception ex)
                {

                }
            }


            MyProfileCon.InsertMyProfileAbount(Id, Phone, country, city, filePath, Picture);
            TempData["Message"] = "Record has been update successfully";
            TempData["tab"] = "About";
        }

        public void InsertMyProfileExperience(string Id, string Experience)
        {
            MyProfileCon.InsertMyProfilExperience(Id, Experience);
            TempData["Message"] = "Record has been update successfully";
            TempData["tab"] = "Experience";
        }

        public void InsertMyProfileAchievement(string Id, string Achievement)
        {
            MyProfileCon.InsertMyProfileAchievement(Id, Achievement);
            TempData["Message"] = "Record has been update successfully";
            TempData["tab"] = "Achievement";
        }

        public void InsertMyProfilelinksUp(string Id, string linksUp)
        {
            MyProfileCon.InsertMyProfilelinksUp(Id, linksUp);
            TempData["Message"] = "Record has been update successfully";
            TempData["tab"] = "linksUp";
        }

        public List<SelectListItem> ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            List<string> dublicate = new List<string>();
            foreach (DataRow row in table.Rows)
            {
                if (!dublicate.Contains(row[valueField].ToString()))
                {
                    dublicate.Add(row[valueField].ToString());
                    list.Add(new SelectListItem()
                    {
                        Value = row[valueField].ToString(),
                        Text = row[textField].ToString()
                    });
                }
            }
            return list;
        }

        protected string Generate()
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;

            characters += alphabets + small_alphabets + numbers;

            int length = int.Parse("10");
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }
    }
}