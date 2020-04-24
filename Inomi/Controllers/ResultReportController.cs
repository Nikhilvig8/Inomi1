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
    public class ResultReportController : Controller
    {
        // GET: ResultReport
        [SessionExpireAttribute]
        public ActionResult ResultReport(string Str)
        {
            string UsertypeId = Session["UserTypeId"].ToString();

            DataTable dtYearList = new DataTable();
            DataTable dtStudentList = new DataTable();
            DataTable dtSchoolList = new DataTable();
            DataTable dtProductList = new DataTable();

            dtYearList = StudentCon.ApplicationYearList();
            ViewBag.YearList = ToSelectList(dtYearList, "Name");

            dtStudentList = StudentCon.StudentList(UsertypeId);
            ViewBag.StudentList = ToSelectList(dtStudentList, "Name");

            dtSchoolList = StudentCon.SchoolList();
            ViewBag.SchoolList = ToSelectList(dtSchoolList, "Name");

            dtProductList = StudentCon.ProductList();
            ViewBag.ProductList = ToSelectListWithValues(dtProductList, "Id", "Name");

            if (Str == null)
            {
                Str = "And 1=1";
            }
            DataTable dt = new DataTable();
            dt = StudentReport.GetStudentReport(Str, UsertypeId);

            return View(dt);
        }

        //public ActionResult ResultReportCall(string Str)
        //{
        //    DataTable dtYearList = new DataTable();
        //    DataTable dtStudentList = new DataTable();
        //    DataTable dtSchoolList = new DataTable();
        //    DataTable dtProductList = new DataTable();

        //    dtYearList = StudentCon.ApplicationYearList();
        //    ViewBag.YearList = ToSelectList(dtYearList, "Name");

        //    dtStudentList = StudentCon.StudentList();
        //    ViewBag.StudentList = ToSelectList(dtStudentList, "Name");

        //    dtSchoolList = StudentCon.SchoolList();
        //    ViewBag.SchoolList = ToSelectList(dtSchoolList, "Name");

        //    dtProductList = StudentCon.ProductList();
        //    ViewBag.ProductList = ToSelectList(dtProductList, "Name");
        //    DataTable dt = new DataTable();
        //    dt = StudentReport.GetStudentReport(Str);

        //    return View(dt);
        //}

        //public ActionResult BindStudentReport(string Str)
        //{
        //    var Model = new List<StudentReportDetails>();
        //    DataTable dt = new DataTable();
        //    dt = StudentReport.GetStudentReport(Str);

        //    if (dt.Rows.Count > 0)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; dt.Rows.Count > i; i++)
        //            {
        //                StudentReportDetails studentReportDetails = new StudentReportDetails();
        //                studentReportDetails.ApplicationYear = dt.Rows[i]["ApplicationYear"].ToString();
        //                studentReportDetails.FirstName = dt.Rows[i]["FirstName"].ToString();
        //                studentReportDetails.School = dt.Rows[i]["School"].ToString();
        //                studentReportDetails.Product = dt.Rows[i]["Product"].ToString();
        //                studentReportDetails.CollegeApply = dt.Rows[i]["CollegeApply"].ToString();
        //                studentReportDetails.CareerApplying = dt.Rows[i]["CareerApplying"].ToString();
        //                studentReportDetails.Result = dt.Rows[i]["Result"].ToString();

        //                Model.Add(studentReportDetails);
        //            }

        //        }
        //    }


        //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //    return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);

        //}

        public List<SelectListItem> ToSelectList(DataTable table, string valueField)
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

        public List<SelectListItem> ToSelectListWithValues(DataTable table, string valueField, string textField)
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
    }
}