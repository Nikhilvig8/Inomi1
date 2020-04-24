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
using System.Text;
using Microsoft.TeamFoundation.SourceControl.WebApi.Legacy;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace Inomi.Controllers
{
    public class StudentDetailsController : Controller
    {
        // GET: StudentDetails
        [SessionExpireAttribute]
        public ActionResult StudentDetails(string StrMain, string tab)
        {
            if (StrMain == null || StrMain == "")
            {
                TempData["Message"] = "";
            }
            else
            {
                TempData["Message"] = StrMain;
            }

            if (tab == null || tab == "")
            {
                TempData["tab"] = "";
            }
            else
            {
                TempData["tab"] = tab;
            }
            string UsertypeId = Session["UserTypeId"].ToString();

            DataTable dtStudentList = new DataTable();
            dtStudentList = StudentCon.StudentList(UsertypeId);
            ViewBag.StudentList = ToSelectList(dtStudentList, "Id", "Name");

            DataTable dtAcedemics = new DataTable();
            dtAcedemics = StudentCon.AcedemicsDetails();
            ViewBag.Acedemics = dtAcedemics;

            DataTable dtTestScore = new DataTable();
            dtTestScore = StudentCon.TestScoreDetails();
            ViewBag.TestScore = dtTestScore;

            DataTable dtActivitics = new DataTable();
            dtActivitics = StudentCon.TestActiviticsDetails();
            ViewBag.Activitics = dtActivitics;

            DataTable dtLeadership = new DataTable();
            dtLeadership = StudentCon.TestLeadershipDetails();
            ViewBag.Leadership = dtLeadership;

            DataTable dtPathfinder = new DataTable();
            dtPathfinder = StudentCon.PathfinderpDetails();
            ViewBag.Pathfinder = dtPathfinder;

            return View();
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
        public void InsertAcedemiscDetails(string json, string Id)
        {
            string filePath = string.Empty;
            if (Request.Files.Count > 0)
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

            XmlDocument XmlDoc;
            XmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + json + "}", "Acedemics");
            StudentCon.InsertAcedemicsDetails(XmlDoc.InnerXml, Id, filePath);
            TempData["Message"] = "Record has been update successfully";
        }
        public void InsertTestScoreDetails(string json, string Id)
        {

            XmlDocument XmlDoc;
            XmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + json + "}", "TestScore");
            StudentCon.InsertTestScoreDetails(XmlDoc.InnerXml, Id);
            TempData["Message"] = "Record has been save successfully";

        }
        public void InsertActivitiesDetails(string json, string Id)
        {

            XmlDocument XmlDoc;
            XmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + json + "}", "Activities");
            StudentCon.InsertActivitiesDetails(XmlDoc.InnerXml, Id);
            TempData["Message"] = "Record has been save successfully";

        }
        public void InsertLeadershipDetails(string json, string Id)
        {

            XmlDocument XmlDoc;
            XmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + json + "}", "Leadership");
            StudentCon.InsertLeadershipDetails(XmlDoc.InnerXml, Id);
            TempData["Message"] = "Record has been save successfully";

        }
        public void DeleteAcedemiscDetails(string id)
        {
            StudentCon.DeleteAcedemicsDetails(id);
            TempData["Message"] = "Record has been delete successfully";
        }
        public ActionResult EditAcedemiscDetails(string id)
        {
            var Model = new List<AcedemicsDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.EditAcedemicsDetails(id);

            if (dt.Rows.Count > 0)
            {

                AcedemicsDetails acedemicsDetails = new AcedemicsDetails();
                string Rid = dt.Rows[0]["Id"].ToString();
                int Noi = int.Parse(Rid);

                acedemicsDetails.Id = Noi;
                acedemicsDetails.StudentID = dt.Rows[0]["StudentID"].ToString();
                acedemicsDetails.Class = dt.Rows[0]["Class"].ToString();
                acedemicsDetails.Year = dt.Rows[0]["Year"].ToString();
                acedemicsDetails.Board = dt.Rows[0]["Board"].ToString();
                acedemicsDetails.School = dt.Rows[0]["School"].ToString();
                acedemicsDetails.Transcript = dt.Rows[0]["Transcript"].ToString();
                acedemicsDetails.OverAllGPAMark = dt.Rows[0]["OverAllGPAMark"].ToString();

                Model.Add(acedemicsDetails);

            }

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);

        }
        public void DeleteTestScoreDetails(string id)
        {
            StudentCon.DeleteTestScoreDetails(id);
            TempData["Message"] = "Record has been delete successfully";
        }
        public ActionResult EditTestScoreDetails(string id)
        {
            var Model = new List<TestReportDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.EditTestScoreDetails(id);

            if (dt.Rows.Count > 0)
            {

                TestReportDetails testReportDetails = new TestReportDetails();
                string Rid = dt.Rows[0]["Id"].ToString();
                int Noi = int.Parse(Rid);

                testReportDetails.Id = Noi;
                testReportDetails.StudentID = dt.Rows[0]["StudentID"].ToString();
                testReportDetails.TestName = dt.Rows[0]["TestName"].ToString();
                testReportDetails.Score = dt.Rows[0]["Score"].ToString();

                Model.Add(testReportDetails);

            }

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);

        }
        public void DeleteActivitiesDetails(string id)
        {
            StudentCon.DeleteActivitiesDetails(id);
            TempData["Message"] = "Record has been delete successfully";
        }
        public ActionResult EditActivitiesDetails(string id)
        {
            var Model = new List<ActivitiesDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.EditActivitiesDetails(id);

            if (dt.Rows.Count > 0)
            {

                ActivitiesDetails activitiesDetails = new ActivitiesDetails();
                string Rid = dt.Rows[0]["Id"].ToString();
                int Noi = int.Parse(Rid);

                activitiesDetails.Id = Noi;
                activitiesDetails.StudentID = dt.Rows[0]["StudentID"].ToString();
                activitiesDetails.Activities = dt.Rows[0]["Activities"].ToString();
                activitiesDetails.Achievements = dt.Rows[0]["Achievements"].ToString();
                activitiesDetails.Details = dt.Rows[0]["Details"].ToString();


                Model.Add(activitiesDetails);

            }

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);

        }
        public void DeleteLeadershipDetails(string id)
        {
            StudentCon.DeleteLeadershipDetails(id);
            TempData["Message"] = "Record has been delete successfully";
        }
        public ActionResult EditLeadershipDetails(string id)
        {
            var Model = new List<LeadershipDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.EditLeadershipDetails(id);

            if (dt.Rows.Count > 0)
            {

                LeadershipDetails leadershipDetails = new LeadershipDetails();
                string Rid = dt.Rows[0]["Id"].ToString();
                int Noi = int.Parse(Rid);

                leadershipDetails.Id = Noi;
                leadershipDetails.StudentID = dt.Rows[0]["StudentID"].ToString();
                leadershipDetails.Position = dt.Rows[0]["Position"].ToString();

                Model.Add(leadershipDetails);

            }

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);

        }

        public void InsertPathfinderDetails(string json, string Id)
        {
            string filePath = string.Empty;
            if (Request.Files.Count > 0)
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

            XmlDocument XmlDoc;
            XmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + json + "}", "Pathfinder");
            StudentCon.InsertPathfinderDetails(Id, filePath, XmlDoc.InnerXml);

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