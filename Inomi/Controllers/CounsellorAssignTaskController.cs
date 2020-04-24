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
    public class CounsellorAssignTaskController : Controller
    {
        // GET: ConsellorAssignTask
        [SessionExpireAttribute]
        public ActionResult CounsellorAssignTask(string Str, string StrMain)
        {
            string UsertypeId = Session["UserTypeId"].ToString();
            if (StrMain == null || StrMain == "")
            {
                TempData["Message"] = "";
            }
            else
            {
                TempData["Message"] = StrMain;
            }

            DataTable dtUnversity = new DataTable();
            dtUnversity = StudentCon.UniversityList();
            ViewBag.UniversityName = ToSelectList(dtUnversity, "Id", "Name");

            DataTable dtStudentList = new DataTable();
            dtStudentList = StudentCon.StudentList(UsertypeId);
            ViewBag.StudentList = ToSelectList(dtStudentList, "Id", "Name");

            DataTable dtTaskDetails = new DataTable();
            dtTaskDetails = StudentCon.AssignTaskDetails(Str,UsertypeId);
            ViewBag.TaskDetails = dtTaskDetails;

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

        public ActionResult InsertAssignDetails(string json, string Id)
        {
            string UsertypeId = Session["UserTypeId"].ToString();
            string UserRole = Session["UserRole"].ToString();
            XmlDocument XmlDoc;
            XmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + json + "}", "Task");

            TempData["Message"] = "Record has been save successfully";

            var Model = new List<StudentDetails>();
            DataTable dt = new DataTable();
            StudentCon.InsertAssignDetails(XmlDoc.InnerXml, Id, UsertypeId, UserRole);
            if (dt.Rows.Count > 0)
            {
                StudentDetails studentDetails = new StudentDetails();
                studentDetails.InsertStatus = "";
                Model.Add(studentDetails);
            }

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);
        }



        public ActionResult DeleteAssignTaskDetails(string id)
        {
            StudentCon.DeleteAssignTaskDetails(id);
            TempData["Message"] = "Record has been delete successfully";
            return RedirectToAction("CounsellorAssignTask", "CounsellorAssignTask");
        }

        public ActionResult EditConAssignTaskDetails(string id)
        {
            var Model = new List<AssignTaskDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.EditConAssignTaskDetails(id);

            if (dt.Rows.Count > 0)
            {

                AssignTaskDetails assignTaskDetails = new AssignTaskDetails();
                string Rid = dt.Rows[0]["Id"].ToString();
                int Noi = int.Parse(Rid);

                assignTaskDetails.Id = Noi;
                assignTaskDetails.Student = dt.Rows[0]["Student"].ToString();
                assignTaskDetails.DeadlineDate = dt.Rows[0]["DeadlineDate"].ToString();
                assignTaskDetails.TaskDescription = dt.Rows[0]["TaskDescription"].ToString();
                assignTaskDetails.TaskType = dt.Rows[0]["TaskType"].ToString();
                assignTaskDetails.UniversityId = dt.Rows[0]["UniversityId"].ToString();
                assignTaskDetails.EssayId = dt.Rows[0]["EssayId"].ToString();
                assignTaskDetails.EssayCount = dt.Rows[0]["EssayCount"].ToString();

                Model.Add(assignTaskDetails);

            }


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);

        }
    }
}