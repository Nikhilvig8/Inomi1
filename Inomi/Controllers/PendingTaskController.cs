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
    public class PendingTaskController : Controller
    {
        // GET: PendingTask
        [SessionExpireAttribute]
        public ActionResult PendingTask(string Str, string StrMain)
        {
            TempData["Str"] = Str;

            if (Str == "Task")
            {
                StrMain = StrMain + "And TaskType='Task'";
            }
            if (Str == "Essays")
            {
                StrMain = StrMain + "And TaskType='Essay'";
            }
            if (Str == "Messages")
            {
                StrMain = StrMain + "And TaskType='Message'";
            }



            string UsertypeId = Session["UserTypeId"].ToString();
            DataTable dtCounsellor = new DataTable();
            dtCounsellor = StudentCon.CounsellorList();
            ViewBag.CounsellorList = ToSelectList(dtCounsellor, "Id", "Name");

            DataTable dtStudentList = new DataTable();
            dtStudentList = StudentCon.StudentList(UsertypeId);
            ViewBag.StudentList = ToSelectList(dtStudentList, "Id", "Name");
            if (StrMain == null)
            {
                StrMain = "And 1=1";
            }
            DataTable dt = new DataTable();
            dt = AssignTaskCon.AssignTaskDetails(StrMain, UsertypeId);

            return View(dt);
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
        public ActionResult InsertAssignTask(string json, string Id)
        {
            XmlDocument XmlDoc;
            XmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + json + "}", "Student");
            AssignTaskCon.InsertAssignTask(XmlDoc.InnerXml, Id);
            TempData["Message"] = "Record has been update successfully";
            return RedirectToAction("Student", "AddNew");
        }
        public ActionResult EditAssignTaskDetails(string id)
        {
            var Model = new List<AssignTaskDetails>();
            DataTable dt = new DataTable();
            dt = AssignTaskCon.EditAssignTaskDetails(id);

            if (dt.Rows.Count > 0)
            {

                AssignTaskDetails assignTaskDetails = new AssignTaskDetails();
                string Rid = dt.Rows[0]["Id"].ToString();
                int Noi = int.Parse(Rid);

                assignTaskDetails.Id = Noi;
                assignTaskDetails.Counsellor = dt.Rows[0]["Counsellor"].ToString();
                assignTaskDetails.Student = dt.Rows[0]["Student"].ToString();
                assignTaskDetails.DeadlineDate = dt.Rows[0]["DeadlineDate"].ToString();
                assignTaskDetails.TaskDescription = dt.Rows[0]["TaskDescription"].ToString();



                Model.Add(assignTaskDetails);

            }


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);

        }
        public ActionResult DeleteAssignTaskDetails(string id)
        {
            AssignTaskCon.DeleteAssignTaskDetails(id);
            TempData["Message"] = "Record has been delete successfully";
            return RedirectToAction("AssignTask", "AssignTask");
        }
    }
}