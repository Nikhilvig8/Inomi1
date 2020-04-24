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


namespace Inomi.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Payment()
        {
            string UsertypeId = Session["UserTypeId"].ToString();

            DataTable dtProductList = new DataTable();
            dtProductList = StudentCon.ProductList();
            ViewBag.ProductList = ToSelectList(dtProductList, "Id", "Name");

            DataTable dtCounsellor = new DataTable();
            dtCounsellor = StudentCon.CounsellorList();
            ViewBag.CounsellorList = ToSelectList(dtCounsellor, "Id", "Name");

            DataTable dtStudentList = new DataTable();
            dtStudentList = StudentCon.StudentList(UsertypeId);
            ViewBag.StudentList = ToSelectList(dtStudentList,"Id", "Name");

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

        public ActionResult GetPaymentDetails(string StudentID)
        {
            var Model = new List<StudentDetails>();
            DataTable dt = new DataTable();
            dt = StudentCon.EditStudentDetails(StudentID);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    StudentDetails studentDetails = new StudentDetails();
                    string Rid = dt.Rows[i]["ID"].ToString();
                    int Noi = int.Parse(Rid);

                    studentDetails.ID = Noi;
                    studentDetails.FirstName = dt.Rows[i]["FirstName"].ToString();
                    studentDetails.Grade = dt.Rows[i]["Grade"].ToString();
                    studentDetails.School = dt.Rows[i]["School"].ToString();
                    studentDetails.Phone = dt.Rows[i]["Phone"].ToString();
                    studentDetails.Email = dt.Rows[i]["Email"].ToString();
                    studentDetails.Gender = dt.Rows[i]["Gender"].ToString();
                    studentDetails.Product = dt.Rows[i]["Product"].ToString();
                    studentDetails.InstallmentCount = dt.Rows[i]["InstallmentCount"].ToString();

                    studentDetails.InsAmt = dt.Rows[i]["InstallmentAmount"].ToString();
                    studentDetails.InsDate = dt.Rows[i]["InsDate"].ToString();

                    studentDetails.Picture = dt.Rows[i]["Picture"].ToString();
                    studentDetails.Parent1Name = dt.Rows[i]["Parent1Name"].ToString();
                    studentDetails.Parent1Occupation = dt.Rows[i]["Parent1Occupation"].ToString();
                    studentDetails.Parent1Phone = dt.Rows[i]["Parent1Phone"].ToString();
                    studentDetails.Parent1Email = dt.Rows[i]["Parent1Email"].ToString();
                    studentDetails.Parent2Name = dt.Rows[i]["Parent2Name"].ToString();
                    studentDetails.Parent2Occupation = dt.Rows[i]["Parent2Occupation"].ToString();
                    studentDetails.Parent2Phone = dt.Rows[i]["Parent2Phone"].ToString();
                    studentDetails.Parent2Email = dt.Rows[i]["Parent2Email"].ToString();
                    studentDetails.CareerApplying = dt.Rows[i]["CareerApplying"].ToString();
                    studentDetails.CountryApplying = dt.Rows[i]["CountryApplying"].ToString();
                    studentDetails.ApplicationYear = dt.Rows[i]["ApplicationYear"].ToString();
                    studentDetails.FinacialAidNeeded = dt.Rows[i]["FinacialAidNeeded"].ToString();
                    studentDetails.Counsellor = dt.Rows[i]["Counsellor"].ToString();


                    Model.Add(studentDetails);

                }
            }


            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return Json(serializer.Serialize(Model), JsonRequestBehavior.AllowGet);

        }
    }
}