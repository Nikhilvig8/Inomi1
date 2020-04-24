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
    public class PaymentReportController : Controller
    {
        // GET: PaymentReport
        [SessionExpireAttribute]
        public ActionResult PaymentReport(string Str)
        {
            string UsertypeId = Session["UserTypeId"].ToString();

            DataTable dtStudentList = new DataTable();
            dtStudentList = StudentCon.StudentList(UsertypeId);
            ViewBag.StudentList = ToSelectList(dtStudentList, "Name");
            if (Str == null)
            {
                Str = "And 1=1";
            }

            DataTable dt = new DataTable();
            dt = PaymentReportCon.GetPaymentPendingReport(Str);
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

        public void ApprovedPayment(string Id)
        {
            PaymentReportCon.ApprovedPaymentStatus(Id);
            TempData["Message"] = "Record has been delete successfully";
            //return RedirectToAction("PaymentReport", "PaymentReport");
        }
    }
}