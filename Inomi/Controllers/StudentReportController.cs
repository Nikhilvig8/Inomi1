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
    public class StudentReportController : Controller
    {
        // GET: StudentReport
        public ActionResult StudentReport(string str, string StrMain)
        {
            string UsertypeId = Session["UserTypeId"].ToString();

            //if (StrMain == null || StrMain == "")
            //{
            //    TempData["Message"] = "";
            //}
            //else
            //{
            //    TempData["Message"] = StrMain;
            //}
            TempData["PageName"] = str;

            if (str == "Accepted")
            {
                StrMain = StrMain + "And Status='Approved'";
            }
            if (str == "Rejected")
            {
                StrMain = StrMain + "And Status='Rejected'";
            }

            DataTable dtStudentList = new DataTable();
            dtStudentList = StudentCon.StudentList(UsertypeId);
            ViewBag.StudentList = ToSelectList(dtStudentList, "Id", "Name");

            DataTable dtCounsellor = new DataTable();
            dtCounsellor = StudentCon.CounsellorList();
            ViewBag.CounsellorList = ToSelectList(dtCounsellor, "Id", "Name");

            DataTable dtProductList = new DataTable();
            dtProductList = StudentCon.ProductList();
            ViewBag.ProductList = ToSelectList(dtProductList, "Id", "Name");

            DataTable dtApplicationYear = new DataTable();
            dtApplicationYear = StudentCon.ApplicationYearList();
            ViewBag.ApplicationYear = ToSelectListOnlyValue(dtApplicationYear, "Name");

            DataTable dtCountryList = new DataTable();
            dtCountryList = StudentCon.CountryList();
            ViewBag.CountryList = ToSelectList(dtCountryList, "Id", "Name");

            DataTable dtSchoolList = new DataTable();
            dtSchoolList = StudentCon.SchoolList();
            ViewBag.SchoolList = ToSelectList(dtSchoolList, "Name");

            DataTable objStudent = new DataTable();
            objStudent = DashboardCon.GetDashboardStudentReport(StrMain, UsertypeId);
            return View(objStudent);
        }

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
    }
}