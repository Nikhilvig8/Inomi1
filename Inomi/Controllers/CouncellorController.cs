using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using Inomi.Models.Student;

namespace Inomi.Controllers.Student
{
    [SessionExpireAttribute]
    public class CouncellorController : Controller
    {
        // GET: Councellor

        int number = 0;
        InomiEntities db = new InomiEntities();
        Dashboard Dashboard = new Dashboard();


        public JsonResult CounsellorStatusRecord(selectedcollege customers)
        {
            number = int.Parse(Session["UserTypeId"].ToString());


            if (customers != null)
            {

                using (InomiEntities entities = new InomiEntities())
                {
                    selectedcollege updatedCustomer = (from c in entities.selectedcolleges
                                                       where c.id == customers.id
                                                       select c).FirstOrDefault();
                    updatedCustomer.counsellorid = number;
                    updatedCustomer.counsellorstatus = customers.Status;
                    //entities.SaveChanges();


                    int insertedRecords = entities.SaveChanges();
                    return Json(insertedRecords);
                }
            }
            return Json(0);
        }
        public JsonResult AdminStatusRecord(selectedcollege customers)
        {
            number = int.Parse(Session["UserTypeId"].ToString());


            if (customers != null)
            {

                using (InomiEntities entities = new InomiEntities())
                {
                    selectedcollege updatedCustomer = (from c in entities.selectedcolleges
                                                       where c.id == customers.id
                                                       select c).FirstOrDefault();
                    updatedCustomer.adminid = number;
                    updatedCustomer.adminstatus = customers.Status;
                    //entities.SaveChanges();


                    int insertedRecords = entities.SaveChanges();
                    return Json(insertedRecords);
                }
            }
            return Json(0);
        }

        public ActionResult applicationview2(string studid, string counid, string college, string country, string city, string course, string ranklist, string shortlist, string application)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            //var data = db.FilterDataCouncellor(studid, counid, college, country, city, course, ranklist, shortlist, application).ToList();
            var data = db.FilterDataCouncellor(studid, number.ToString(), "", "", "", "", "", shortlist, "").ToList();


            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    Collegeshortlist CallNote = new Collegeshortlist();
                    CallNote.Id = item.Id;
                    CallNote.NameOfCollege = item.NameOfCollege;
                    CallNote.Description = item.Description;
                    CallNote.Status = item.Status;
                    CallNote.sequenceid = item.sequenceid;
                    CallNote.counsellorid = item.counsellorid;
                    CallNote.counsellorstatus = item.counsellorstatus;
                    CallNote.adminstatus = item.adminstatus;
                    CallNote.adminid = item.adminid;



                    Dashboard.Collegeshortlist.Add(CallNote);
                }
            }

            return PartialView("FilterData", Dashboard);
        }

        public ActionResult applicationview3(string studid, string counid, string college, string country, string city, string course, string ranklist, string shortlist, string application)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            //var data = db.FilterDataCouncellor(studid, counid, college, country, city, course, ranklist, shortlist, application).ToList();
            var data = db.FilterDataAdmin(studid, counid, "", "", "", "", "", shortlist, "").ToList();


            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    Collegeshortlist CallNote = new Collegeshortlist();
                    CallNote.Id = item.Id;
                    CallNote.NameOfCollege = item.NameOfCollege;
                    CallNote.Description = item.Description;
                    CallNote.Status = item.Status;
                    CallNote.sequenceid = item.sequenceid;
                    CallNote.counsellorid = item.counsellorid;
                    CallNote.counsellorstatus = item.counsellorstatus;
                    CallNote.adminstatus = item.adminstatus;
                    CallNote.adminid = item.adminid;



                    Dashboard.Collegeshortlist.Add(CallNote);
                }
            }

            return PartialView("FilterDataAdmin", Dashboard);
        }

        [HttpGet]
        public ActionResult Index()
        {
            number = int.Parse(Session["UserTypeId"].ToString());

            ViewBag.CountryList = ToSelectList1((from A in db.tblCountryNames select new DropDownModel { Id = A.CID.ToString(), Value = A.Name }).ToList());
            ViewBag.StudentList = ToSelectList1((from A in db.tblStudents where A.Counsellor == "" + number + "" select new DropDownModel { Id = A.ID.ToString(), Value = A.FirstName + " " + A.LastName }).ToList());
            ViewBag.CollegeList = ToSelectList1((from A in db.tblColleges select new DropDownModel { Id = A.Id.ToString(), Value = A.NameOfCollege }).ToList());
            ViewBag.ApplicationThrough = ToSelectList1((from A in db.tblApplicationModes select new DropDownModel { Id = A.Id.ToString(), Value = A.Name }).ToList());
            ViewBag.courselist = ToSelectList1((from A in db.tblCourses select new DropDownModel { Id = A.Id.ToString(), Value = A.NameOfCourse }).ToList());
            var data = db.sp_collegeshortlist2(number.ToString()).ToList();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    Collegeshortlist CallNote = new Collegeshortlist();
                    CallNote.Id = item.Id;
                    CallNote.NameOfCollege = item.NameOfCollege;
                    CallNote.Description = item.Description;
                    CallNote.Status = item.Status;
                    CallNote.sequenceid = item.sequenceid;
                    CallNote.counsellorid = item.counsellorid;
                    CallNote.counsellorstatus = item.counsellorstatus;
                    CallNote.adminstatus = item.adminstatus;
                    CallNote.adminid = item.adminid;



                    Dashboard.Collegeshortlist.Add(CallNote);
                }
            }




            return View("index", Dashboard);
        }
        public JsonResult GetStates(int Id)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            ViewBag.CityList = ToSelectList1((from A in db.tblCityNames.Where(a => a.CID == Id) select new DropDownModel { Id = A.CTID.ToString(), Value = A.Name }).ToList());

            return Json(new SelectList(ViewBag.CityList, "Value", "Text"));
        }
        public JsonResult GetCourse(int Id)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            ViewBag.CourseList = ToSelectList1((from A in db.tblCourses.Where(a => a.College == Id.ToString()) select new DropDownModel { Id = A.Id.ToString(), Value = A.NameOfCourse }).ToList());

            return Json(new SelectList(ViewBag.CourseList, "Value", "Text"));
        }

        public List<SelectListItem> ToSelectList1(List<DropDownModel> lis)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            List<string> dublicate = new List<string>();
            foreach (var row in lis)
            {
                list.Add(new SelectListItem()
                {
                    Value = row.Id,
                    Text = row.Value
                });
            }
            return list;
        }

        public JsonResult GetStudent(int Id)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            ViewBag.StudentList = ToSelectList1((from A in db.tblStudents where A.Counsellor == "" + Id + "" select new DropDownModel { Id = A.ID.ToString(), Value = A.FirstName + " " + A.LastName }).ToList());
                
            return Json(new SelectList(ViewBag.StudentList, "Value", "Text"));
        }

        [HttpGet]
        public ActionResult AdmintApplicationView()
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            ViewBag.CountryList = ToSelectList1((from A in db.tblCountryNames select new DropDownModel { Id = A.CID.ToString(), Value = A.Name }).ToList());
            ViewBag.CouncellorList = ToSelectList1((from A in db.tblCounsellors select new DropDownModel { Id = A.Id.ToString(), Value = A.Name }).ToList());
            ViewBag.StudentList = ToSelectList1((from A in db.tblStudents select new DropDownModel { Id = A.ID.ToString(), Value = A.FirstName + " " + A.LastName }).ToList());
            ViewBag.CollegeList = ToSelectList1((from A in db.tblColleges select new DropDownModel { Id = A.Id.ToString(), Value = A.NameOfCollege }).ToList());
            ViewBag.ApplicationThrough = ToSelectList1((from A in db.tblApplicationModes select new DropDownModel { Id = A.Id.ToString(), Value = A.Name }).ToList());
            ViewBag.courselist = ToSelectList1((from A in db.tblCourses select new DropDownModel { Id = A.Id.ToString(), Value = A.NameOfCourse }).ToList());
            var data = db.sp_collegeshortlist2("").ToList();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    Collegeshortlist CallNote = new Collegeshortlist();
                    CallNote.Id = item.Id;
                    CallNote.NameOfCollege = item.NameOfCollege;
                    CallNote.Description = item.Description;
                    CallNote.Status = item.Status;
                    CallNote.sequenceid = item.sequenceid;
                    CallNote.counsellorid = item.counsellorid;
                    CallNote.counsellorstatus = item.counsellorstatus;
                    CallNote.adminstatus = item.adminstatus;
                    CallNote.adminid = item.adminid;



                    Dashboard.Collegeshortlist.Add(CallNote);
                }
            }




            return View("AdmintApplicationView", Dashboard);
        }
    }
}