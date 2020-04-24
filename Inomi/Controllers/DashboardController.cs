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
    public class DashboardController : Controller
    {
        // GET: Dashboard
        [SessionExpireAttribute]
        public ActionResult Dashboard(string str, string StrMain)
        {
            if (StrMain == null || StrMain == "")
            {
                TempData["Message"] = "";
            }
            else
            {
                TempData["Message"] = StrMain;
            }


            string UsertypeId = Session["UserTypeId"].ToString();

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
            objStudent = DashboardCon.GetDashboardStudentReport(str, UsertypeId);
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
        public ActionResult GetProductChart(string ProductId, string TabId)
        {
            var Model = new List<ProductChart>();
            DataTable dt = new DataTable();
            dt = StudentCon.ProductChart(ProductId, TabId);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; dt.Rows.Count > i; i++)
                    {
                        ProductChart productChart = new ProductChart();
                        string Pid = dt.Rows[i]["Product"].ToString();
                        int Noi = int.Parse(Pid);

                        productChart.ProductCount = Noi;
                        productChart.Month = dt.Rows[i]["Month"].ToString();
                        Model.Add(productChart);
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
        public ActionResult GetCounsellerPendingTask()
        {
            var Model = new List<CounsellerPendingTask>();
            DataTable dt = new DataTable();
            dt = StudentCon.CounsellerPendingTask();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; dt.Rows.Count > i; i++)
                    {
                        CounsellerPendingTask counsellerPendingTask = new CounsellerPendingTask();
                        //string Pid = dt.Rows[i]["Product"].ToString();
                        //int Noi = int.Parse(Pid);

                        //productChart.ProductCount = Noi;
                        counsellerPendingTask.TotalCount = dt.Rows[i]["TotalCount"].ToString();
                        counsellerPendingTask.TotalTask = dt.Rows[i]["TotalTask"].ToString();
                        counsellerPendingTask.TotalEssay = dt.Rows[i]["TotalEssay"].ToString();
                        counsellerPendingTask.TotalMessage = dt.Rows[i]["TotalMessage"].ToString();
                        Model.Add(counsellerPendingTask);
                    }

                }
            }
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTotalAppliction()
        {
            var Model = new List<CounsellerPendingTask>();
            DataTable dt = new DataTable();
            dt = StudentCon.TotalAppliction();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; dt.Rows.Count > i; i++)
                    {
                        CounsellerPendingTask counsellerPendingTask = new CounsellerPendingTask();
                        //string Pid = dt.Rows[i]["Product"].ToString();
                        //int Noi = int.Parse(Pid);

                        //productChart.ProductCount = Noi;
                        counsellerPendingTask.TotalAppliction = dt.Rows[i]["Action"].ToString();
                        counsellerPendingTask.Accepted = dt.Rows[i]["Approved"].ToString();
                        counsellerPendingTask.Rejected = dt.Rows[i]["Rejected"].ToString();

                        Model.Add(counsellerPendingTask);
                    }

                }
            }
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);
        }
    }
}