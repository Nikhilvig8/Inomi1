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
    [SessionExpireAttribute]
    public class HomeController : Controller
    {
        [SessionExpireAttribute]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult CounsellorCalendar(string StrMain)
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

            ViewBag.Message = "Your contact page.";
            DataTable dtStudentList = new DataTable();
            dtStudentList = StudentCon.StudentList(UsertypeId);
            ViewBag.StudentList = ToSelectList(dtStudentList, "Id", "Name");

            return View();
        }

        public JsonResult GetEvents()
        {
            var events = new List<Event>();
            DataTable dt = new DataTable();
            string UsertypeId = Session["UserTypeId"].ToString();
            string UserType = Session["UserType"].ToString();
            dt = CalendarCon.GetEvnetDetails(UsertypeId, UserType);

            if (dt.Rows.Count > 0)
            {
                for (int i=0; dt.Rows.Count > i;i++)
                {

                    Event eventDetails = new Event();
                    string Rid = dt.Rows[i]["EventId"].ToString();
                    int EventId = int.Parse(Rid);

                    //events.Id = Noi;


                    eventDetails.EvenID = EventId;
                    eventDetails.Subject = dt.Rows[i]["Subject"].ToString();
                    eventDetails.Description = dt.Rows[i]["Description"].ToString();
                    eventDetails.Start = DateTime.Parse(dt.Rows[i]["Start"].ToString());
                    eventDetails.End = DateTime.Parse(dt.Rows[i]["End"].ToString());
                    eventDetails.ThemeColor = dt.Rows[i]["ThemeColor"].ToString();
                    eventDetails.IsFullDay = bool.Parse(dt.Rows[i]["IsFullDay"].ToString());

                    events.Add(eventDetails);
                }
            }

            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            //using (InomiEntities dc = new InomiEntities())
            //{
            //    var events = dc.Events.ToList();
            //    return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //}
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

        public ActionResult InsertEventDetails(string json)
        {
            string UsertypeId = Session["UserTypeId"].ToString();
            XmlDocument XmlDoc;

            XmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + json + "}", "Event");

            CalendarCon.InsertEventDetails(XmlDoc.InnerXml, UsertypeId);
            TempData["Message"] = "Record has been save successfully";
            return RedirectToAction("CounsellorCalendar", "Home");
        }
    }
}