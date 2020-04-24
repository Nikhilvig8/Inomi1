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
    public class ActivityReportController : Controller
    {
        // GET: ActivityReport
        [SessionExpireAttribute]
        public ActionResult ActivityReport(string Str)
        {
            string UsertypeId = Session["UserTypeId"].ToString();

            DataTable dtStudentList = new DataTable();
            dtStudentList = StudentCon.StudentList(UsertypeId);
            ViewBag.StudentList = ToSelectList(dtStudentList, "Name");

            DataTable dtSchoolList = new DataTable();
            dtSchoolList = StudentCon.SchoolList();
            ViewBag.SchoolList = ToSelectList(dtSchoolList, "Name");

            DataTable dtYearList = new DataTable();
            dtYearList = StudentCon.ApplicationYearList();
            ViewBag.YearList = ToSelectList(dtYearList, "Name");

            DataTable dt = new DataTable();
            dt = ActivityReportCon.GetActivityReport(Str);

            return View(dt);

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
    }


}