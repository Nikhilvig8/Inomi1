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
    public class AddNewController : Controller
    {
        private InomiEntities db = new InomiEntities();

        // GET: AddNew
        [SessionExpireAttribute]
        public ActionResult Student(string StrMain, string tab)
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

            DataTable dtCounsellor = new DataTable();
            DataTable dtCourse = new DataTable();
            DataTable dtApplicationMode = new DataTable();
            DataTable dtTestingRequirement = new DataTable();
            DataTable dtCollege = new DataTable();
            DataTable dtProductList = new DataTable();
            DataTable dtApplicationYear = new DataTable();
            DataTable dtCountryList = new DataTable();

            dtCounsellor = StudentCon.CounsellorList();
            ViewBag.CounsellorList = ToSelectList(dtCounsellor, "Id", "Name");

            dtCourse = StudentCon.CourseList();
            ViewBag.CourseList = ToSelectList(dtCourse, "Id", "Name");

            dtApplicationMode = StudentCon.ApplicationModeList();
            ViewBag.ApplicationModeList = ToSelectList(dtApplicationMode, "Id", "Name");

            dtTestingRequirement = StudentCon.TestingRequirementList();
            ViewBag.TestingRequirementList = ToSelectList(dtTestingRequirement, "Id", "Name");

            dtCollege = StudentCon.CollegeList();
            ViewBag.CollegeList = ToSelectList(dtCollege, "Id", "Name");

            dtProductList = StudentCon.ProductList();
            ViewBag.ProductList = ToSelectList(dtProductList, "Id", "Name");

            dtApplicationYear = StudentCon.ApplicationYearList();
            ViewBag.ApplicationYear = ToSelectListOnlyValue(dtApplicationYear, "Name");

            dtCountryList = StudentCon.CountryList();
            ViewBag.CountryList = ToSelectList(dtCountryList, "Id", "Name");

            DataTable dtStudentList = new DataTable();
            dtStudentList = StudentCon.StudentList(UsertypeId);
            ViewBag.StudentList = ToSelectList(dtStudentList, "Id", "Name");

            DataTable dtScoreType = new DataTable();
            dtScoreType = StudentCon.ScoreType(UsertypeId);
            ViewBag.ScoreType = ToSelectListOnlyValue(dtScoreType, "Name");


            DataTable dtStudentDetails = new DataTable();
            dtStudentDetails = StudentCon.StudentDetails();
            ViewBag.StudentDetails = dtStudentDetails;

            DataTable dtCounsellorDetails = new DataTable();
            dtCounsellorDetails = StudentCon.CounsellorDetails();
            ViewBag.CounsellorDetails = dtCounsellorDetails;

            DataTable dtCollegeDetails = new DataTable();
            dtCollegeDetails = StudentCon.CollegeDetails();
            ViewBag.CollegeDetails = dtCollegeDetails;

            DataTable dtCountryDetails = new DataTable();
            dtCountryDetails = StudentCon.CountryDetails();
            ViewBag.CountryDetails = dtCountryDetails;

            DataTable dtProductDetails = new DataTable();
            dtProductDetails = StudentCon.ProductDetails();
            ViewBag.ProductDetails = dtProductDetails;

            DataTable dtCourseDetails = new DataTable();
            dtCourseDetails = StudentCon.CourseDetails();
            ViewBag.CourseDetails = dtCourseDetails;

            DataTable dtTaskDetails = new DataTable();
            dtTaskDetails = StudentCon.TaskDetails();
            ViewBag.TaskDetails = dtTaskDetails;

            DataTable dtTestDetails = new DataTable();
            dtTestDetails = StudentCon.TestDetails();
            ViewBag.TestDetails = dtTestDetails;

            DataTable dtFormatDetails = new DataTable();
            dtFormatDetails = StudentCon.FormatDetails();
            ViewBag.FormatDetails = dtFormatDetails;

            return View();
        }
        public ActionResult InsertStudentStatus(string json, string Id, string jsonIns)
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



            XmlDocument XmlDoc;
            XmlDocument XmlInsDate;
            XmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + json + "}", "Student");
            XmlInsDate = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + jsonIns + "}", "InsDate");

            var Model = new List<StudentDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.InsertStudentDetails(XmlDoc.InnerXml, Id, filePath, XmlInsDate.InnerXml); 
            if (dt.Rows.Count > 0)
            {
                StudentDetails studentDetails = new StudentDetails();
                studentDetails.InsertStatus = dt.Rows[0]["InsertStatus"].ToString();
                Model.Add(studentDetails);
            }

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);

        }
        public ActionResult InsertCounsoellorStatus(string json, string Id)
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
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }

            XmlDocument XmlDoc;
            XmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + json + "}", "Counsellor");
            StudentCon.InsertCounsoellorDetails(XmlDoc.InnerXml, Id, filePath);
            //if (Id == null)
            //{
            //    TempData["Message"] = "Record has been save successfully";
            //}
            //else
            //{
            //    TempData["Message"] = "Record has been update successfully";
            //}
            return RedirectToAction("Student", "AddNew");
        }
        public void InsertCollegeStatus(string json, string Id, string jsonDetails, string jsonScore)
        {
            XmlDocument XmlDoc;
            XmlDocument XmlDocDetails;
            XmlDocument XmlDocScore;

            XmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + json + "}", "College");
            XmlDocDetails = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + jsonDetails + "}", "College");
            XmlDocScore = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + jsonScore + "}", "Score");

            StudentCon.InsertCollegeDetails(XmlDoc.InnerXml, Id, XmlDocDetails.InnerXml, XmlDocScore.InnerXml);
            TempData["Message"] = "Record has been update successfully";
        }
        public ActionResult InsertCountryStatus(string json, string Id)
        {
            XmlDocument XmlDoc;
            XmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + json + "}", "Country");
            StudentCon.InsertCountryDetails(XmlDoc.InnerXml, Id);
            TempData["Message"] = "Record has been update successfully";
            return RedirectToAction("Student", "AddNew");
        }
        public ActionResult InsertProductStatus(string json, string Id)
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

                        filePath = "/Uploads/" + fname + "";
                        fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);

                        file.SaveAs(fname);
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }



            XmlDocument XmlDoc;
            XmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + json + "}", "Product");
            StudentCon.InsertProductDetails(XmlDoc.InnerXml, Id, filePath);
            TempData["Message"] = "Record has been update successfully";
            return RedirectToAction("Student", "AddNew");
        }
        public ActionResult InsertCourseStatus(string json, string Id)
        {
            XmlDocument XmlDoc;
            XmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + json + "}", "Course");
            StudentCon.InsertCourseDetails(XmlDoc.InnerXml, Id);
            TempData["Message"] = "Record has been update successfully";
            return RedirectToAction("Student", "AddNew");
        }
        public ActionResult InsertTaskStatus(string json, string Id)
        {
            string UsertypeId = Session["UserTypeId"].ToString();
            XmlDocument XmlDoc;
            XmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + json + "}", "Task");
            StudentCon.InsertTaskDetails(XmlDoc.InnerXml, Id, UsertypeId);
            TempData["Message"] = "Record has been update successfully";
            return RedirectToAction("Student", "AddNew");
        }
        public ActionResult InsertTestStatus(string json, string Id)
        {
            XmlDocument XmlDoc;
            XmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + json + "}", "Test");
            StudentCon.InsertTestDetails(XmlDoc.InnerXml, Id);
            TempData["Message"] = "Record has been update successfully";
            return RedirectToAction("Student", "AddNew");
        }
        public ActionResult InsertFormatStatus(string json, string Id, string EmailText, string jsonC, string jsonS, string Emailsubject)
        {
            string filePath = string.Empty;
            string fname = "";
            int Filesize = 0;

            string Emailbody = DecodeString(EmailText);

            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        Filesize = file.ContentLength;

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
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }

            XmlDocument XmlDoc;

            XmlDocument XmlDocC;
            XmlDocument XmlDocS;

            XmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + json + "}", "Format");

            XmlDocC = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + jsonC + "}", "Counsellor");
            XmlDocS = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + jsonS + "}", "Student");


            StudentCon.InsertFormatDetails(XmlDoc.InnerXml, Id, filePath, Emailbody, XmlDocC.InnerXml, XmlDocS.InnerXml, Emailsubject, Filesize);
            TempData["Message"] = "Record has been update successfully";
            SendEmail(filePath, fname, Emailbody, XmlDocC.InnerXml, XmlDocS.InnerXml, Emailsubject);
            return RedirectToAction("Student", "AddNew");
        }
        public ActionResult DeleteStudentDetails(string id)
        {
            StudentCon.DeleteStudentDetails(id);
            TempData["Message"] = "Record has been delete successfully";
            return RedirectToAction("Student", "AddNew");
        }
        public ActionResult EditStudentDetails(string id)
        {
            var Model = new List<StudentDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.EditStudentDetails(id);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    StudentDetails studentDetails = new StudentDetails();
                    string Rid = dt.Rows[i]["ID"].ToString();
                    int Noi = int.Parse(Rid);

                    studentDetails.ID = Noi;
                    studentDetails.FirstName = dt.Rows[i]["FirstName"].ToString();
                    studentDetails.Grade = dt.Rows[i]["Grade"].ToString();
                    studentDetails.School = dt.Rows[i]["School"].ToString();
                    studentDetails.Phone = dt.Rows[i]["Phone"].ToString();
                    studentDetails.Email = dt.Rows[i]["Email"].ToString();
                    studentDetails.Gender = dt.Rows[i]["Gender"].ToString();
                    studentDetails.Product = dt.Rows[i]["Product"].ToString();
                    studentDetails.InstallmentCount = dt.Rows[i]["InstallmentCount"].ToString();

                    studentDetails.InsAmt = dt.Rows[i]["InstallmentAmount"].ToString();
                    studentDetails.InsDate = dt.Rows[i]["InsDate"].ToString();

                    studentDetails.Picture = dt.Rows[i]["Picture"].ToString();
                    studentDetails.Parent1Name = dt.Rows[i]["Parent1Name"].ToString();
                    studentDetails.Parent1Occupation = dt.Rows[i]["Parent1Occupation"].ToString();
                    studentDetails.Parent1Phone = dt.Rows[i]["Parent1Phone"].ToString();
                    studentDetails.Parent1Email = dt.Rows[i]["Parent1Email"].ToString();
                    studentDetails.Parent2Name = dt.Rows[i]["Parent2Name"].ToString();
                    studentDetails.Parent2Occupation = dt.Rows[i]["Parent2Occupation"].ToString();
                    studentDetails.Parent2Phone = dt.Rows[i]["Parent2Phone"].ToString();
                    studentDetails.Parent2Email = dt.Rows[i]["Parent2Email"].ToString();
                    studentDetails.CareerApplying = dt.Rows[i]["CareerApplying"].ToString();
                    studentDetails.CountryApplying = dt.Rows[i]["CountryApplying"].ToString();
                    studentDetails.ApplicationYear = dt.Rows[i]["ApplicationYear"].ToString();
                    studentDetails.FinacialAidNeeded = dt.Rows[i]["FinacialAidNeeded"].ToString();
                    studentDetails.Counsellor = dt.Rows[i]["Counsellor"].ToString();

                    studentDetails.IsConcession = dt.Rows[i]["IsConcession"].ToString();
                    studentDetails.ConcessionAmount = dt.Rows[i]["ConcessionAmount"].ToString();


                    Model.Add(studentDetails);

                }
            }


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);

        }
        public ActionResult DeleteCounsellorDetails(string id)
        {
            StudentCon.DeleteCounsellorDetails(id);
            TempData["Message"] = "Record has been delete successfully";
            return RedirectToAction("Student", "AddNew");
        }
        public ActionResult EditCounsellorDetails(string id)
        {
            var Model = new List<CounsellorDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.EditCounsellorDetails(id);

            if (dt.Rows.Count > 0)
            {

                CounsellorDetails counsellorDetails = new CounsellorDetails();
                string Rid = dt.Rows[0]["Id"].ToString();
                int Noi = int.Parse(Rid);

                counsellorDetails.Id = Noi;
                counsellorDetails.Name = dt.Rows[0]["Name"].ToString();
                counsellorDetails.Education = dt.Rows[0]["Education"].ToString();
                counsellorDetails.Link = dt.Rows[0]["Link"].ToString();
                counsellorDetails.Country = dt.Rows[0]["Country"].ToString();
                counsellorDetails.City = dt.Rows[0]["City"].ToString();
                counsellorDetails.Phone = dt.Rows[0]["Phone"].ToString();
                counsellorDetails.Email = dt.Rows[0]["Email"].ToString();
                counsellorDetails.Experience = dt.Rows[0]["Experience"].ToString();
                counsellorDetails.Picture = dt.Rows[0]["Picture"].ToString();
                counsellorDetails.Achievements = dt.Rows[0]["Achievements"].ToString();

                Model.Add(counsellorDetails);

            }


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);

        }
        public ActionResult DeleteCollegeDetails(string id)
        {
            StudentCon.DeleteCollegeDetails(id);
            TempData["Message"] = "Record has been delete successfully";
            return RedirectToAction("Student", "AddNew");
        }
        public ActionResult EditCollegeDetails(string id)
        {
            var Model = new List<CollegeDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.EditCollegeDetails(id);

            if (dt.Rows.Count > 0)
            {

                CollegeDetails collegeDetails = new CollegeDetails();
                string Rid = dt.Rows[0]["Id"].ToString();
                int Noi = int.Parse(Rid);

                collegeDetails.Id = Noi;
                collegeDetails.NameOfCollege = dt.Rows[0]["NameOfCollege"].ToString();
                collegeDetails.Country = dt.Rows[0]["Country"].ToString();
                collegeDetails.City = dt.Rows[0]["City"].ToString();
                collegeDetails.Courses = dt.Rows[0]["Courses"].ToString();
                collegeDetails.ApplicationMode = dt.Rows[0]["ApplicationMode"].ToString();
                collegeDetails.EssayName = dt.Rows[0]["EssayName"].ToString();
                collegeDetails.Topic = dt.Rows[0]["Topic"].ToString();
                collegeDetails.WordCount = dt.Rows[0]["WordCount"].ToString();
                collegeDetails.deadline = dt.Rows[0]["deadline"].ToString();

                collegeDetails.TestingRequirement = dt.Rows[0]["TestingRequirement"].ToString();
                collegeDetails.Link = dt.Rows[0]["Link"].ToString();
                collegeDetails.ApplicationDeadline = dt.Rows[0]["ApplicationDeadline"].ToString();

                collegeDetails.InternationalRank = dt.Rows[0]["IRank"].ToString();
                collegeDetails.IndianRank = dt.Rows[0]["IndRank"].ToString();

                collegeDetails.USNewsRanking = dt.Rows[0]["USNewsRanking"].ToString();
                collegeDetails.QSWorldRanking = dt.Rows[0]["QSWorldRanking"].ToString();
                collegeDetails.QSBySubject = dt.Rows[0]["QSBySubject"].ToString();

                collegeDetails.GradeProfile = dt.Rows[0]["GrandeProfile"].ToString();

                collegeDetails.CampusName = dt.Rows[0]["CampusName"].ToString();
                collegeDetails.EarlyDate = dt.Rows[0]["EarlyDate"].ToString();
                collegeDetails.LateDate = dt.Rows[0]["LateDate"].ToString();
                collegeDetails.AboutCollege = dt.Rows[0]["AboutCollege"].ToString();

                Model.Add(collegeDetails);

            }


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);

        }
        public ActionResult DeleteCountryDetails(string id)
        {
            StudentCon.DeleteCountryDetails(id);
            TempData["Message"] = "Record has been delete successfully";
            return RedirectToAction("Student", "AddNew");
        }
        public ActionResult EditCountryDetails(string id)
        {
            var Model = new List<CountryDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.EditCountryDetails(id);

            if (dt.Rows.Count > 0)
            {

                CountryDetails countryDetails = new CountryDetails();
                string Rid = dt.Rows[0]["Id"].ToString();
                int Noi = int.Parse(Rid);

                countryDetails.Id = Noi;
                countryDetails.NameOfCountry = dt.Rows[0]["NameOfCountry"].ToString();
                countryDetails.JobProspect = dt.Rows[0]["JobProspect"].ToString();
                countryDetails.VisaProcess = dt.Rows[0]["VisaProcess"].ToString();
                countryDetails.Link = dt.Rows[0]["Link"].ToString();

                Model.Add(countryDetails);

            }


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);

        }
        public ActionResult DeleteProductDetails(string id)
        {
            StudentCon.DeleteProductDetails(id);
            TempData["Message"] = "Record has been delete successfully";
            return RedirectToAction("Student", "AddNew");
        }
        public ActionResult EditProductDetails(string id)
        {
            var Model = new List<ProductDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.EditProductDetails(id);

            if (dt.Rows.Count > 0)
            {

                ProductDetails productDetails = new ProductDetails();
                string Rid = dt.Rows[0]["Id"].ToString();
                int Noi = int.Parse(Rid);

                productDetails.Id = Noi;
                productDetails.Name = dt.Rows[0]["Name"].ToString();
                productDetails.Fee = dt.Rows[0]["Fee"].ToString();
                productDetails.Link = dt.Rows[0]["Link"].ToString();

                productDetails.Attribute1 = dt.Rows[0]["Attribute1"].ToString();    //FilePath
                productDetails.Attribute2 = dt.Rows[0]["Attribute2"].ToString();    //FileName
                productDetails.ProductDtls = dt.Rows[0]["ProductDetails"].ToString();

                Model.Add(productDetails);

            }

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);

        }
        public ActionResult DeleteCourseDetails(string id)
        {
            StudentCon.DeleteCourseDetails(id);
            TempData["Message"] = "Record has been delete successfully";
            return RedirectToAction("Student", "AddNew");
        }
        public ActionResult EditCourseDetails(string id)
        {
            var Model = new List<CourseDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.EditCourseDetails(id);

            if (dt.Rows.Count > 0)
            {

                CourseDetails courseDetails = new CourseDetails();
                string Rid = dt.Rows[0]["Id"].ToString();
                int Noi = int.Parse(Rid);

                courseDetails.Id = Noi;
                courseDetails.NameOfCourse = dt.Rows[0]["NameOfCourse"].ToString();
                courseDetails.College = dt.Rows[0]["College"].ToString();
                courseDetails.Career = dt.Rows[0]["Career"].ToString();
                courseDetails.Link = dt.Rows[0]["Link"].ToString();

                Model.Add(courseDetails);

            }

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);

        }
        public ActionResult DeleteTaskDetails(string id)
        {
            StudentCon.DeleteTaskDetails(id);
            TempData["Message"] = "Record has been delete successfully";
            return RedirectToAction("Student", "AddNew");
        }
        public ActionResult EditTaskDetails(string id)
        {
            var Model = new List<TaskDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.EditTaskDetails(id);

            if (dt.Rows.Count > 0)
            {

                TaskDetails taskDetails = new TaskDetails();
                string Rid = dt.Rows[0]["Id"].ToString();
                int Noi = int.Parse(Rid);

                taskDetails.Id = Noi;
                taskDetails.Counsellor = dt.Rows[0]["Counsellor"].ToString();
                taskDetails.Task = dt.Rows[0]["Task"].ToString();
                taskDetails.Deadline = dt.Rows[0]["Deadline"].ToString();

                Model.Add(taskDetails);

            }

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);

        }
        public ActionResult DeleteTestDetails(string id)
        {
            StudentCon.DeleteTestDetails(id);
            TempData["Message"] = "Record has been delete successfully";
            return RedirectToAction("Student", "AddNew");
        }
        public ActionResult EditTestDetails(string id)
        {
            var Model = new List<TestDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.EditTestDetails(id);

            if (dt.Rows.Count > 0)
            {

                TestDetails testDetails = new TestDetails();
                string Rid = dt.Rows[0]["Id"].ToString();
                int Noi = int.Parse(Rid);

                testDetails.Id = Noi;
                testDetails.NameOfTest = dt.Rows[0]["NameOfTest"].ToString();

                Model.Add(testDetails);

            }

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);

        }
        public ActionResult DeleteFormatDetails(string id)
        {
            StudentCon.DeleteFormatDetails(id);
            TempData["Message"] = "Record has been delete successfully";
            return RedirectToAction("Student", "AddNew");
        }
        public ActionResult EditFormatDetails(string id)
        {
            var Model = new List<FormatDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.EditFormatDetails(id);

            if (dt.Rows.Count > 0)
            {

                FormatDetails formatDetails = new FormatDetails();
                string Rid = dt.Rows[0]["Id"].ToString();
                int Noi = int.Parse(Rid);

                formatDetails.Id = Noi;
                formatDetails.Format = dt.Rows[0]["Format"].ToString();

                Model.Add(formatDetails);

            }

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);

        }
        public ActionResult BindStudentDetails()
        {
            var Model = new List<StudentDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.StudentDetails();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; dt.Rows.Count > i; i++)
                    {
                        StudentDetails studentDetails = new StudentDetails();
                        string id = dt.Rows[i]["ID"].ToString();
                        int Noi = int.Parse(id);

                        studentDetails.ID = Noi;
                        studentDetails.FirstName = dt.Rows[i]["FirstName"].ToString();
                        studentDetails.Grade = dt.Rows[i]["Grade"].ToString();
                        studentDetails.School = dt.Rows[i]["School"].ToString();
                        studentDetails.Phone = dt.Rows[i]["Phone"].ToString();
                        studentDetails.Email = dt.Rows[i]["Email"].ToString();
                        studentDetails.Gender = dt.Rows[i]["Gender"].ToString();
                        studentDetails.Product = dt.Rows[i]["Product"].ToString();

                        Model.Add(studentDetails);
                    }

                }
            }


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);
        }
        public ActionResult BindCounsoellorDetails()
        {
            var Model = new List<CounsellorDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.CounsellorDetails();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; dt.Rows.Count > i; i++)
                    {
                        CounsellorDetails counsellorDetails = new CounsellorDetails();
                        string id = dt.Rows[i]["ID"].ToString();
                        int Noi = int.Parse(id);

                        counsellorDetails.Id = Noi;
                        counsellorDetails.Name = dt.Rows[i]["Name"].ToString();
                        counsellorDetails.Education = dt.Rows[i]["Education"].ToString();
                        counsellorDetails.Link = dt.Rows[i]["Link"].ToString();
                        counsellorDetails.Country = dt.Rows[i]["Country"].ToString();
                        counsellorDetails.City = dt.Rows[i]["City"].ToString();
                        counsellorDetails.Phone = dt.Rows[i]["Phone"].ToString();
                        counsellorDetails.Experience = dt.Rows[i]["Experience"].ToString();

                        Model.Add(counsellorDetails);
                    }

                }
            }


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);
        }
        public ActionResult BindCollegeDetails()
        {
            var Model = new List<CollegeDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.CollegeDetails();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; dt.Rows.Count > i; i++)
                    {
                        CollegeDetails collegeDetails = new CollegeDetails();
                        string id = dt.Rows[i]["ID"].ToString();
                        int Noi = int.Parse(id);

                        collegeDetails.Id = Noi;
                        collegeDetails.NameOfCollege = dt.Rows[i]["NameOfCollege"].ToString();
                        collegeDetails.Country = dt.Rows[i]["Country"].ToString();
                        collegeDetails.City = dt.Rows[i]["City"].ToString();
                        collegeDetails.Courses = dt.Rows[i]["Courses"].ToString();
                        collegeDetails.ApplicationMode = dt.Rows[i]["ApplicationMode"].ToString();
                        collegeDetails.TestingRequirement = dt.Rows[i]["TestingRequirement"].ToString();
                        collegeDetails.Link = dt.Rows[i]["Link"].ToString();
                        collegeDetails.ApplicationDeadline = dt.Rows[i]["ApplicationDeadline"].ToString();


                        Model.Add(collegeDetails);
                    }

                }
            }


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);
        }
        public ActionResult BindCountryDetails()
        {
            var Model = new List<CountryDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.CountryDetails();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; dt.Rows.Count > i; i++)
                    {
                        CountryDetails countryDetails = new CountryDetails();
                        string id = dt.Rows[i]["ID"].ToString();
                        int Noi = int.Parse(id);

                        countryDetails.Id = Noi;
                        countryDetails.NameOfCountry = dt.Rows[i]["NameOfCountry"].ToString();
                        countryDetails.JobProspect = dt.Rows[i]["JobProspect"].ToString();
                        countryDetails.VisaProcess = dt.Rows[i]["VisaProcess"].ToString();
                        countryDetails.Link = dt.Rows[i]["Link"].ToString();
                        Model.Add(countryDetails);
                    }

                }
            }


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);
        }
        public ActionResult BindProductDetails()
        {
            var Model = new List<ProductDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.ProductDetails();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; dt.Rows.Count > i; i++)
                    {
                        ProductDetails productDetails = new ProductDetails();
                        string id = dt.Rows[i]["ID"].ToString();
                        int Noi = int.Parse(id);

                        productDetails.Id = Noi;
                        productDetails.Name = dt.Rows[i]["Name"].ToString();
                        productDetails.Fee = dt.Rows[i]["Fee"].ToString();
                        productDetails.Installment1 = dt.Rows[i]["Installment1"].ToString();
                        productDetails.DueDate1 = dt.Rows[i]["DueDate1"].ToString();

                        productDetails.Installment2 = dt.Rows[i]["Installment2"].ToString();
                        productDetails.DueDate2 = dt.Rows[i]["DueDate2"].ToString();

                        productDetails.Installment3 = dt.Rows[i]["Installment3"].ToString();
                        productDetails.DueDate2 = dt.Rows[i]["DueDate3"].ToString();

                        productDetails.Installment4 = dt.Rows[i]["Installment4"].ToString();
                        productDetails.DueDate4 = dt.Rows[i]["DueDate4"].ToString();

                        productDetails.Link = dt.Rows[i]["Link"].ToString();


                        Model.Add(productDetails);
                    }

                }
            }



            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);
        }
        public ActionResult BindCourseDetails()
        {
            var Model = new List<CourseDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.CourseDetails();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; dt.Rows.Count > i; i++)
                    {
                        CourseDetails courseDetails = new CourseDetails();
                        string id = dt.Rows[i]["ID"].ToString();
                        int Noi = int.Parse(id);

                        courseDetails.Id = Noi;
                        courseDetails.NameOfCourse = dt.Rows[i]["NameOfCourse"].ToString();
                        courseDetails.College = dt.Rows[i]["College"].ToString();
                        courseDetails.Career = dt.Rows[i]["Career"].ToString();
                        courseDetails.Link = dt.Rows[i]["Link"].ToString();
                        Model.Add(courseDetails);
                    }

                }
            }



            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);
        }
        public ActionResult BindTaskDetails()
        {
            var Model = new List<TaskDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.TaskDetails();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; dt.Rows.Count > i; i++)
                    {
                        TaskDetails taskDetails = new TaskDetails();
                        string id = dt.Rows[i]["ID"].ToString();
                        int Noi = int.Parse(id);

                        taskDetails.Id = Noi;
                        taskDetails.Counsellor = dt.Rows[i]["Counsellor"].ToString();
                        taskDetails.Task = dt.Rows[i]["Task"].ToString();
                        taskDetails.Deadline = dt.Rows[i]["Deadline"].ToString();
                        Model.Add(taskDetails);
                    }

                }
            }



            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);
        }
        public ActionResult BindTestDetails()
        {
            var Model = new List<TestDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.TestDetails();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; dt.Rows.Count > i; i++)
                    {
                        TestDetails testDetails = new TestDetails();
                        string id = dt.Rows[i]["ID"].ToString();
                        int Noi = int.Parse(id);

                        testDetails.Id = Noi;
                        testDetails.NameOfTest = dt.Rows[i]["NameOfTest"].ToString();
                        Model.Add(testDetails);
                    }

                }
            }



            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);
        }
        public ActionResult BindFormatDetails()
        {
            var Model = new List<FormatDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.FormatDetails();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; dt.Rows.Count > i; i++)
                    {
                        FormatDetails formatDetails = new FormatDetails();
                        string id = dt.Rows[i]["ID"].ToString();
                        int Noi = int.Parse(id);

                        formatDetails.Id = Noi;
                        formatDetails.Format = dt.Rows[i]["Format"].ToString();
                        Model.Add(formatDetails);
                    }

                }
            }
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCityList(string Cid)
        {
            var Model = new List<CityDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.CityList(Cid);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; dt.Rows.Count > i; i++)
                    {
                        CityDetails cityDetails = new CityDetails();
                        string id = dt.Rows[i]["Id"].ToString();
                        int Noi = int.Parse(id);

                        cityDetails.CTID = Noi;
                        cityDetails.Name = dt.Rows[i]["Name"].ToString();
                        Model.Add(cityDetails);
                    }

                }
            }
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);
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
        public List<SelectListItem> ToSelectListOnlyValue(DataTable table, string valueField)
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
                        Text = row[valueField].ToString()
                    });
                }
            }
            return list;
        }
        public ActionResult StudentStatus(string Id, string Status)
        {
            StudentCon.StudentStatus(Id, Status);
            StudnetConfirmationEmail(Id, Status);
            TempData["Message"] = "Record has been update successfully";
            return RedirectToAction("Student", "AddNew");
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
        public void SendEmail(string file, string FilePath, string Emailbody, string CounsellorId, string StudentId, string Emailsubject)
        {
            Session["file"] = file;
            try
            {
                DataTable dt = new DataTable();
                dt = StudentCon.GetEmails(CounsellorId, StudentId);

                String date = DateTime.Now.ToString("dd.MM.yyy");

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["Host"].ToString());
                mail.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"].ToString());
                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    mail.To.Add(dt.Rows[i]["Email"].ToString());
                }

                List<string> li = new List<string>();

               // li.Add("manojkumar@teamcomputers.com"); //Add CC

                li.Add("richa@inomi.in");

                string bodyHtml = "<!DOCTYPE html> <html lang='en'> <head> <!-- Required meta tags --> <meta charset='utf-8' /> <meta name='viewport' content='width=device-width, initial-scale=1' /> <title>A Message to our Customers</title> <link href='https://fonts.googleapis.com/css?family=Roboto:400,500,700&display=swap' rel='stylesheet' /> </head> <body style=' background-color: #fff; margin: 0; padding: 0; font-family: ' roboto', sans-serif; padding: 0; font-weight: 400; '> <table border='0' cellpadding='0' cellspacing='0' style=' background: #fefefe url(https://www.teamcomputers.com/repositry/edm/html-edm/inomi/images//bg.png) no-repeat; font-size: 1rem; line-height: 1.4; max-width: 650px; width: 100%; margin: 0 auto; border-collapse: collapse; border: 1px solid #e5e5e5; '> <tr border-spacing='0' cellspacing='0'> <td border-spacing='0' cellspacing='0'> <table border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse;'> <tr> <td style='padding: 1rem 1.8rem;'> <a href='https://www.teamcomputers.com/' target='_blank'> <img src='https://ci5.googleusercontent.com/proxy/CCympt22TuSCyHxRvAjyd2iqQXmLc_PuUPq_R0Y6x-ljkYVHNga8IpwNPt18PTpOhLrLWbpRoo6RAuvL05GgIu752ira3sX4aUtyZzkHrI_ye9qua933euKj5Wyc62h1uUok=s0-d-e1-ft#https://www.teamcomputers.com/repositry/edm/html-edm/inomi/images/inomi-logo.png' alt='inomi logo' /> </a> </td> </tr> <tr> <td valign='center' style='position: relative;'></td> </tr> <tr> <td style='padding: 2rem 1.8rem 3.5rem; letter-spacing: 0.2px;'> <p style=' font-size: 18px; font-weight: 700; color: #1b2d4e; margin-bottom: 0; '></p> <p style=' font-size: 16px; max-width: 325px; margin-top: 0.6rem; line-height: 1.8; '>" + Emailbody + " </p> </td> </tr> </table> </td> </tr> <tr> <td style='padding: 0 1.8rem;'>  <tr> <td> <table border='0' cellspacing='0' style=' background-color: #262625; width: 100%; border-collapse: collapse; border-top-left-radius: 5px; border-top-right-radius: 5px; '> <tr> <td style='padding: 1rem 0 1rem;'> <p style=' font-size: 14px; color: #fff; text-align: center; margin-bottom: 0; '> 10, Arjun Marg, DLF Phase I, Gurgaon 122002 | +919811923388 </p> </td> </tr> </table> </td> </tr> <tr style='background-color: #262625;'> <td style='padding-bottom: 1rem;'> <table border='0' cellspacing='0' style=' width: 100%; max-width: 120px; margin: 0 auto; border-collapse: collapse; '> <tr> <td> <a href='#' target='_blank'> <img src='https://www.teamcomputers.com/repositry/edm/html-edm/inomi/images//fa-icon.png' alt='fa icon' style='margin: 0 auto; display: block;' /> </a> </td> <td> <a href='#' target='_blank'> <img src='https://www.teamcomputers.com/repositry/edm/html-edm/inomi/images//li-icon.png' alt='team logo' style='margin: 0 auto; display: block;' /> </a> </td> <td> <a href='#'> <img src='https://www.teamcomputers.com/repositry/edm/html-edm/inomi/images//tw-icon.png' alt='team logo' style='margin: 0 auto; display: block;' /> </a> </td> </tr> </table> </td> </tr> <tr> <td> <table border='0' cellspacing='0' style=' background-color: #262625; width: 100%; border-collapse: collapse; '> <tr> <td style='padding: 0 0 1rem;'> <p style=' font-size: 12px; color: #73819a; text-align: center; margin-top: 0; '> © 2020 All rights reserved. </p> </td> </tr> </table> </td> </tr> </table> </body> </html>";


                mail.CC.Add(string.Join<string>(",", li)); // Sending CC  
                mail.Bcc.Add(string.Join<string>(",", li)); // Sending Bcc   
                mail.Subject = Emailsubject; // Mail Subject  

                mail.IsBodyHtml = true;
                mail.Body = bodyHtml;
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(FilePath); //Attaching File to Mail  
                mail.Attachments.Add(attachment);
                SmtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]); //PORT  
                SmtpServer.EnableSsl = true;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                //SmtpServer.Credentials = new NetworkCredential("manojkumar@teamcomputers.com", "Daksh@9045");
                SmtpServer.Credentials = new NetworkCredential("apps.helpdesk@teamcomputers.com", "Password!1");
                SmtpServer.Send(mail);
                StudentCon.UpdateStatus(2, "Sucessful send", file);

            }
            catch (Exception ex)
            {
                //string FileName = Session["file"].ToString();
                StudentCon.UpdateStatus(2, ex.Message, "test");
            }
        }

        public void StudnetConfirmationEmail(string Id, string Status)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = StudentCon.GetStudnetEmails(Id);

                String date = DateTime.Now.ToString("dd.MM.yyy");

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["Host"].ToString());
                mail.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"].ToString());
                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    mail.To.Add(dt.Rows[i]["Email"].ToString());
                }

                List<string> li = new List<string>();

                //li.Add("manojkumar@teamcomputers.com"); //Add CC
                li.Add("richa@inomi.in");

                mail.CC.Add(string.Join<string>(",", li)); // Sending CC  
                mail.Bcc.Add(string.Join<string>(",", li)); // Sending Bcc   
                mail.Subject = "Onboard at Inomi"; // Mail Subject 
                string bodyHtml = "";
                if (Status == "Approved")
                {
                    bodyHtml = "<!DOCTYPE html> <html lang='en'> <head> <!-- Required meta tags --> <meta charset='utf-8' /> <meta name='viewport' content='width=device-width, initial-scale=1' /> <title>A Message to our Customers</title> <link href='https://fonts.googleapis.com/css?family=Roboto:400,500,700&display=swap' rel='stylesheet' /> </head> <body style=' background-color: #fff; margin: 0; padding: 0; font-family: ' roboto', sans-serif; padding: 0; font-weight: 400; '> <table border='0' cellpadding='0' cellspacing='0' style=' background: #fefefe url(https://www.teamcomputers.com/repositry/edm/html-edm/inomi/images//bg.png) no-repeat; font-size: 1rem; line-height: 1.4; max-width: 650px; width: 100%; margin: 0 auto; border-collapse: collapse; border: 1px solid #e5e5e5; '> <tr border-spacing='0' cellspacing='0'> <td border-spacing='0' cellspacing='0'> <table border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse;'> <tr> <td style='padding: 1rem 1.8rem;'> <a href='https://www.teamcomputers.com/' target='_blank'> <img src='https://ci5.googleusercontent.com/proxy/CCympt22TuSCyHxRvAjyd2iqQXmLc_PuUPq_R0Y6x-ljkYVHNga8IpwNPt18PTpOhLrLWbpRoo6RAuvL05GgIu752ira3sX4aUtyZzkHrI_ye9qua933euKj5Wyc62h1uUok=s0-d-e1-ft#https://www.teamcomputers.com/repositry/edm/html-edm/inomi/images/inomi-logo.png' alt='inomi logo' /> </a> </td> </tr> <tr> <td valign='center' style='position: relative;'></td> </tr> <tr> <td style='padding: 2rem 1.8rem 3.5rem; letter-spacing: 0.2px;'> <p style=' font-size: 18px; font-weight: 700; color: #1b2d4e; margin-bottom: 0; '> Hello " + dt.Rows[0]["Name"].ToString() + ", </p> <p style=' font-size: 16px; max-width: 325px; margin-top: 0.6rem; line-height: 1.8; '> Congratulations... you are Onboard in <strong style='color: #10c1ff; text-decoration: underline;'>Inomi.</strong></p> </td> </tr> </table> </td> </tr> <tr> <td style='padding: 0 1.8rem;'> <table style=' border: 1px solid #e5e5e5; background-color: #fff; width: 100%; border-radius: 5px; margin-bottom: 0.5rem; '> <tr> <td style=' color: #1b2d4e; font-size: 18px; font-weight: 700; padding: 1.5rem 0 0.4rem 0.5rem; '> User Information </td> </tr> <tr> <td style='padding-bottom: 1.3rem;'> <table style=' width: 100%; font-size: 16px; border-collapse: separate; border-spacing: 10px 5px; '> <tr> <td style=' width: 50%; background-color: rgba(16, 193, 255, 0.25); padding: 10px 16px; border-radius: 5px; font-weight: 700; '> Name : </td> <td style=' width: 50%; border: 1px solid #efefef; border-radius: 5px; padding: 10px 16px; '> " + dt.Rows[0]["Name"].ToString() + " </td> </tr> <tr> <td style=' width: 50%; background-color: rgba(16, 193, 255, 0.25); padding: 10px 16px; border-radius: 5px; font-weight: 700; '> UserName : </td> <td style=' width: 50%; border: 1px solid #efefef; border-radius: 5px; padding: 10px 16px; '> " + dt.Rows[0]["Email"].ToString() + " </td> </tr> <tr> <td style=' width: 50%; background-color: rgba(16, 193, 255, 0.25); padding: 10px 16px; border-radius: 5px; font-weight: 700; '> Password : </td> <td style=' width: 50%; border: 1px solid #efefef; border-radius: 5px; padding: 10px 16px; '> Password </td> </tr> <tr> <td style=' width: 50%; background-color: rgba(16, 193, 255, 0.25); padding: 10px 16px; border-radius: 5px; font-weight: 700; '> Name of the Organisation : </td> <td style=' width: 50%; border: 1px solid #efefef; border-radius: 5px; padding: 10px 16px; '> Inomi</td> </tr></table> </td> </tr> </table> </td> </tr> <tr> <td> <table border='0' cellspacing='0' style=' background-color: #262625; width: 100%; border-collapse: collapse; border-top-left-radius: 5px; border-top-right-radius: 5px; '> <tr> <td style='padding: 1rem 0 1rem;'> <p style=' font-size: 14px; color: #fff; text-align: center; margin-bottom: 0; '> 10, Arjun Marg, DLF Phase I, Gurgaon 122002 | +919811923388 </p> </td> </tr> </table> </td> </tr> <tr style='background-color: #262625;'> <td style='padding-bottom: 1rem;'> <table border='0' cellspacing='0' style=' width: 100%; max-width: 120px; margin: 0 auto; border-collapse: collapse; '> <tr> <td> <a href='#' target='_blank'> <img src='https://www.teamcomputers.com/repositry/edm/html-edm/inomi/images//fa-icon.png' alt='fa icon' style='margin: 0 auto; display: block;' /> </a> </td> <td> <a href='#' target='_blank'> <img src='https://www.teamcomputers.com/repositry/edm/html-edm/inomi/images//li-icon.png' alt='team logo' style='margin: 0 auto; display: block;' /> </a> </td> <td> <a href='#'> <img src='https://www.teamcomputers.com/repositry/edm/html-edm/inomi/images//tw-icon.png' alt='team logo' style='margin: 0 auto; display: block;' /> </a> </td> </tr> </table> </td> </tr> <tr> <td> <table border='0' cellspacing='0' style=' background-color: #262625; width: 100%; border-collapse: collapse; '> <tr> <td style='padding: 0 0 1rem;'> <p style=' font-size: 12px; color: #73819a; text-align: center; margin-top: 0; '> © 2020 All rights reserved. </p> </td> </tr> </table> </td> </tr> </table> </body> </html>";
                }
                if (Status == "Rejected")
                {
                    bodyHtml = "<!DOCTYPE html> <html lang='en'> <head> <!-- Required meta tags --> <meta charset='utf-8' /> <meta name='viewport' content='width=device-width, initial-scale=1' /> <title>A Message to our Customers</title> <link href='https://fonts.googleapis.com/css?family=Roboto:400,500,700&display=swap' rel='stylesheet' /> </head> <body style=' background-color: #fff; margin: 0; padding: 0; font-family: ' roboto', sans-serif; padding: 0; font-weight: 400; '> <table border='0' cellpadding='0' cellspacing='0' style=' background: #fefefe url(https://www.teamcomputers.com/repositry/edm/html-edm/inomi/images//bg.png) no-repeat; font-size: 1rem; line-height: 1.4; max-width: 650px; width: 100%; margin: 0 auto; border-collapse: collapse; border: 1px solid #e5e5e5; '> <tr border-spacing='0' cellspacing='0'> <td border-spacing='0' cellspacing='0'> <table border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse;'> <tr> <td style='padding: 1rem 1.8rem;'> <a href='https://www.teamcomputers.com/' target='_blank'> <img src='https://ci5.googleusercontent.com/proxy/CCympt22TuSCyHxRvAjyd2iqQXmLc_PuUPq_R0Y6x-ljkYVHNga8IpwNPt18PTpOhLrLWbpRoo6RAuvL05GgIu752ira3sX4aUtyZzkHrI_ye9qua933euKj5Wyc62h1uUok=s0-d-e1-ft#https://www.teamcomputers.com/repositry/edm/html-edm/inomi/images/inomi-logo.png' alt='inomi logo' /> </a> </td> </tr> <tr> <td valign='center' style='position: relative;'></td> </tr> <tr> <td style='padding: 2rem 1.8rem 3.5rem; letter-spacing: 0.2px;'> <p style=' font-size: 18px; font-weight: 700; color: #1b2d4e; margin-bottom: 0; '>Hello " + dt.Rows[0]["Name"].ToString() + ",</p> <p style=' font-size: 16px; max-width: 325px; margin-top: 0.6rem; line-height: 1.8; '>Your application has been rejected. </p> </td> </tr> </table> </td> </tr> <tr> <td style='padding: 0 1.8rem;'>  <tr> <td> <table border='0' cellspacing='0' style=' background-color: #262625; width: 100%; border-collapse: collapse; border-top-left-radius: 5px; border-top-right-radius: 5px; '> <tr> <td style='padding: 1rem 0 1rem;'> <p style=' font-size: 14px; color: #fff; text-align: center; margin-bottom: 0; '> 10, Arjun Marg, DLF Phase I, Gurgaon 122002 | +919811923388 </p> </td> </tr> </table> </td> </tr> <tr style='background-color: #262625;'> <td style='padding-bottom: 1rem;'> <table border='0' cellspacing='0' style=' width: 100%; max-width: 120px; margin: 0 auto; border-collapse: collapse; '> <tr> <td> <a href='#' target='_blank'> <img src='https://www.teamcomputers.com/repositry/edm/html-edm/inomi/images//fa-icon.png' alt='fa icon' style='margin: 0 auto; display: block;' /> </a> </td> <td> <a href='#' target='_blank'> <img src='https://www.teamcomputers.com/repositry/edm/html-edm/inomi/images//li-icon.png' alt='team logo' style='margin: 0 auto; display: block;' /> </a> </td> <td> <a href='#'> <img src='https://www.teamcomputers.com/repositry/edm/html-edm/inomi/images//tw-icon.png' alt='team logo' style='margin: 0 auto; display: block;' /> </a> </td> </tr> </table> </td> </tr> <tr> <td> <table border='0' cellspacing='0' style=' background-color: #262625; width: 100%; border-collapse: collapse; '> <tr> <td style='padding: 0 0 1rem;'> <p style=' font-size: 12px; color: #73819a; text-align: center; margin-top: 0; '> © 2020 All rights reserved. </p> </td> </tr> </table> </td> </tr> </table> </body> </html>";

                }
                mail.IsBodyHtml = true;
                mail.Body = bodyHtml;
                //System.Net.Mail.Attachment attachment;
                //attachment = new System.Net.Mail.Attachment(FilePath); //Attaching File to Mail  
                //mail.Attachments.Add(attachment);
                SmtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]); //PORT  
                SmtpServer.EnableSsl = true;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                //SmtpServer.Credentials = new NetworkCredential("manojkumar@teamcomputers.com", "Daksh@9045");
                SmtpServer.Credentials = new NetworkCredential("apps.helpdesk@teamcomputers.com", "Password!1");
                SmtpServer.Send(mail);
                // StudentCon.UpdateStatus(2, "Sucessful send", file);

            }
            catch (Exception ex)
            {
                //string FileName = Session["file"].ToString();
                // StudentCon.UpdateStatus(2, ex.Message, "test");
            }
        }
        public string DecodeString(string strMain)
        {
            string strDecode = strMain.Replace("~amp;", "&")
                       .Replace("~lt;", "<")
                       .Replace("~gt;", ">")
                       .Replace("~quot;", "")
                       .Replace("~039;", "'")
                       .Replace("~040;", "#");
            return strDecode;
        }

    }
}