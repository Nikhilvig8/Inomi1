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

namespace Inomi.Controllers
{
    public class FAQController : Controller
    {
        // GET: FAQ
        public ActionResult FAQ()
        {
            DataTable dt = new DataTable();
            dt = FAQCon.GetFAQDetails();

            return View(dt);
        }

        public ActionResult FAQMaster(string StrMain)
        {
            if (StrMain == null || StrMain == "")
            {
                TempData["Message"] = "";
            }
            else
            {
                TempData["Message"] = StrMain;
            }

            DataTable dt = new DataTable();
            dt = FAQCon.GetFAQDetails();

            return View(dt);
        }

        public ActionResult FAQInsertData(string json, string Id)
        {
            XmlDocument XmlDoc;
            XmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + json + "}", "FAQ");
            FAQCon.InsertFAQData(XmlDoc.InnerXml, Id);
            TempData["Message"] = "Record has been update successfully";
            return RedirectToAction("FAQMaster", "FAQ");
        }

        public ActionResult EditFAQDetails(string Id)
        {
            var Model = new List<FAQDetails>();
            DataTable dt = new DataTable();
            dt = FAQCon.EditFAQDetails(Id);

            if (dt.Rows.Count > 0)
            {

                FAQDetails fAQDetails = new FAQDetails();
                string Rid = dt.Rows[0]["Id"].ToString();
                int Noi = int.Parse(Rid);

                fAQDetails.Id = Noi;
                fAQDetails.Question = dt.Rows[0]["Question"].ToString();
                fAQDetails.Answer = dt.Rows[0]["Answer"].ToString();

                Model.Add(fAQDetails);

            }

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteFAQDetails(string Id)
        {
            FAQCon.DeleteFAQDetails(Id);
            TempData["Message"] = "Record has been delete successfully";
            return RedirectToAction("FAQMaster", "FAQ");
        }
    }
}