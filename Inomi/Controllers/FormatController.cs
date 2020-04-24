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
    public class FormatController : Controller
    {
        // GET: Format
        [SessionExpireAttribute]
        public ActionResult Format(string StrMain)
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
            DataTable dtCounsellor = new DataTable();
            dtCounsellor = StudentCon.CounsellorList();
            ViewBag.CounsellorList = ToSelectList(dtCounsellor, "Id", "Name");


            DataTable dtStudentList = new DataTable();
            dtStudentList = StudentCon.StudentList(UsertypeId);
            ViewBag.StudentList = ToSelectList(dtStudentList, "Id", "Name");

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
    }
}