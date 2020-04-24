using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inomi.Models.Student;

using System.Globalization;
using System.IO;
using System.Configuration;
using DataLayer;

using BusinessLayer;

using Newtonsoft.Json;
using System.Xml;
using System.Data;
using Inomi.Models.Questionnaire;

namespace Inomi.Controllers.Student
{
    [SessionExpireAttribute]
    public class StudentController : Controller

    {


        InomiEntities db = new InomiEntities();
        Dashboard Dashboard = new Dashboard();
        double Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        int number = 0;
        // GET: Student
        public ActionResult Index()
        {
            number = int.Parse(Session["UserTypeId"].ToString());

            string Backdate = DateTime.Now.AddDays(-7).ToString("MM-dd-yyyy");
            string currentdate = DateTime.Now.ToString("MM-dd-yyyy");


            //var UpcomingDeadlineCount = db.tblColleges.ToList().Where(a => a.IsActive == true && DateTime.ParseExact(a.ApplicationDeadline.Replace("/", "-"), "MM-dd-yyyy", CultureInfo.InvariantCulture).AddDays(-5) >= DateTime.ParseExact(currentdate.Replace("/", "-"), "MM-dd-yyyy", CultureInfo.InvariantCulture) && DateTime.ParseExact(a.ApplicationDeadline.Replace("/", "-"), "MM-dd-yyyy", CultureInfo.InvariantCulture) >= DateTime.ParseExact(currentdate.Replace("/", "-"), "MM-dd-yyyy", CultureInfo.InvariantCulture)).Count();

            var UpcomingDeadlineCount = db.tblColleges.ToList().Where(a => a.IsActive == true && DateTime.ParseExact(currentdate.Replace("/", "-"), "MM-dd-yyyy", CultureInfo.InvariantCulture) >= DateTime.ParseExact(a.ApplicationDeadline.Replace("/", "-"), "MM-dd-yyyy", CultureInfo.InvariantCulture).AddDays(-5) && DateTime.ParseExact(currentdate.Replace("/", "-"), "MM-dd-yyyy", CultureInfo.InvariantCulture) <= DateTime.ParseExact(a.ApplicationDeadline.Replace("/", "-"), "MM-dd-yyyy", CultureInfo.InvariantCulture)).Count();


            var DeadlineEssayCount = db.tblAssingTasks.ToList().Where(a => a.Student == "" + number + "" && a.IsActive == "1" && a.DeadlineDate <= DateTime.Now.Date && a.TaskType == "Essay").Count();


            var MessageCount = db.tblAssingTasks.ToList().Where(a => a.Student == "" + number + "" && a.IsActive == "1" && a.TaskType == "Message").Count();


            Dashboard.Counts.UpcomingDeadlineCount = UpcomingDeadlineCount.ToString("00");
            Dashboard.Counts.DeadlineEssayCount = DeadlineEssayCount.ToString("00");
            Dashboard.Counts.MessageCount = MessageCount.ToString("00");

            var AcademicsData = (from ep in db.tblAcadenmice


                                 where ep.StudentID == number && ep.IsActive=="1"
                                 select new
                                 {
                                     StudentID = ep.StudentID,
                                     Class = ep.Class,
                                     Year = ep.Year,
                                     Board = ep.Board,
                                     School = ep.School,
                                     Transcript = ep.Transcript,
                                     OverAllGPAMark = ep.OverAllGPAMark,
                                     Subject = ep.Subject,
                                     id = ep.Id

                                 }).ToList();
            if (AcademicsData.Count > 0)
            {
                foreach (var item in AcademicsData)
                {
                    Academics Academics = new Academics();
                    Academics.StudentID = item.StudentID;
                    Academics.Class = item.Class;
                    Academics.Year = item.Year;
                    Academics.Board = item.Board;
                    Academics.School = item.School;
                    Academics.Transcript = item.Transcript;
                    Academics.OverAllGPAMark = item.OverAllGPAMark;
                    Academics.Subject = item.Subject;
                    Academics.Id = item.id.ToString();


                    Dashboard.Academics.Add(Academics);
                }
            }




            var ProjectData = (from ep in db.tblProjects


                               where ep.StudentID == number && ep.IsActive == "1"
                               select new
                               {
                                   ProjectID = ep.ProjectID,
                                   Name = ep.Name,
                                   Description = ep.Description


                               }).OrderByDescending(m => m.ProjectID).ToList();
            if (ProjectData.Count > 0)
            {
                foreach (var item in ProjectData)
                {
                    Project Project = new Project();
                    Project.Projectid = item.ProjectID;
                    Project.Name = item.Name;
                    Project.Description = item.Description;


                    var MilestoneData = (from ep in db.tblProjectMilestones


                                         where ep.ProjectID == Project.Projectid
                                         select new
                                         {
                                             StudentID = ep.StudentID,
                                             Milestone = ep.Milestone,
                                             Deadline = ep.Deadline,
                                             status = ep.status,
                                             MId = ep.MId,
                                             Define = ep.Define,

                                         }).ToList();
                    if (MilestoneData.Count > 0)
                    {
                        foreach (var item1 in MilestoneData)
                        {
                            Milestone Milestone = new Milestone();
                            Milestone.ID = item1.MId;
                            Milestone.MilestoneName = item1.Milestone;
                            Milestone.Milestonedate = item1.Deadline;
                            Milestone.Status = item1.status;
                            Milestone.Description = item1.Define;



                            Project.Milestone.Add(Milestone);
                        }
                    }




                    Dashboard.Project.Add(Project);
                }
            }





            var TestReportData = (from ep in db.tblTestReports


                                  where ep.StudentID == number && ep.IsActive=="1"
                                  select new
                                  {
                                      StudentID = ep.StudentID,
                                      TestName = ep.TestName,
                                      Score = ep.Score,
                                      id = ep.Id


                                  }).ToList();
            if (TestReportData.Count > 0)
            {
                foreach (var item in TestReportData)
                {
                    TestReport TestReport = new TestReport();
                    TestReport.TestName = item.TestName;
                    TestReport.Score = item.Score;
                    TestReport.id = item.id.ToString();



                    Dashboard.TestReport.Add(TestReport);
                }
            }
            var ExtraCurricularActivitesData = (from ep in db.tblExtraCurricularActivites


                                                where ep.StudentID == number && ep.IsActive=="1"
                                                select new
                                                {
                                                    Activities = ep.Activities,
                                                    Achievements = ep.Achievements,
                                                    Details = ep.Details,
                                                    id = ep.Id


                                                }).ToList();
            if (ExtraCurricularActivitesData.Count > 0)
            {
                foreach (var item in ExtraCurricularActivitesData)
                {
                    ExtraCurricularActivites ExtraCurricularActivites = new ExtraCurricularActivites();
                    ExtraCurricularActivites.Activities = item.Activities;
                    ExtraCurricularActivites.Achievements = item.Achievements;
                    ExtraCurricularActivites.Details = item.Details;
                    ExtraCurricularActivites.id = item.id.ToString() ;



                    Dashboard.ExtraCurricularActivites.Add(ExtraCurricularActivites);
                }
            }

            var LeadershipPositionsData = (from ep in db.tblLeadershipPositions


                                           where ep.StudentID == number && ep.IsActive=="1"
                                           select new
                                           {
                                               Position = ep.Position,
                                               id = ep.Id



                                           }).ToList();
            if (LeadershipPositionsData.Count > 0)
            {
                foreach (var item in LeadershipPositionsData)
                {
                    LeadershipPositions LeadershipPositions = new LeadershipPositions();
                    LeadershipPositions.Position = item.Position;
                    LeadershipPositions.id = item.id;




                    Dashboard.LeadershipPositions.Add(LeadershipPositions);
                }
            }

            var CounsellorData = (from pd in db.tblStudents
                                  join od in db.tblCounsellors on pd.Counsellor equals od.Id.ToString()
                                  where pd.ID == number
                                  select new
                                  {
                                      od.Name,
                                      od.Attribute1,
                                      od.Education,
                                      od.Experience,
                                  }).SingleOrDefault();

            Dashboard.Counsellor.Education = CounsellorData.Education;
            Dashboard.Counsellor.Name = CounsellorData.Name;
            Dashboard.Counsellor.Experience = CounsellorData.Experience;
            Dashboard.Counsellor.Picture = CounsellorData.Attribute1;

            DataTable dtPathfinder = new DataTable();
            dtPathfinder = StudentCon.Pathfinder(number);
            ViewBag.Pathfinder = dtPathfinder.Rows[0].Field<string>(0);


            return View("Index", Dashboard);
        }


        public JsonResult GetColumnChartData()
        {
            string UserName = Session["EmailId"].ToString();

            tblQuestionnaireSubStrengthResult temp = db.tblQuestionnaireSubStrengthResults.Where(x => x.UserName == UserName).FirstOrDefault();

            List<SubStrengthValueChart> list = new List<SubStrengthValueChart>();

            if (temp != null)
            {
                SubStrengthValueChart Artistic = new SubStrengthValueChart();
                Artistic.Name = "Artistic";
                Artistic.Value = temp.Artistic;
                list.Add(Artistic);

                SubStrengthValueChart Aesthetic = new SubStrengthValueChart();
                Aesthetic.Name = "Aesthetic";
                Aesthetic.Value = temp.Aesthetic;
                list.Add(Aesthetic);

                SubStrengthValueChart Idea_Generator = new SubStrengthValueChart();
                Idea_Generator.Name = "Idea Generator";
                Idea_Generator.Value = temp.Idea_Generator;
                list.Add(Idea_Generator);

                SubStrengthValueChart Team_Player = new SubStrengthValueChart();
                Team_Player.Name = "Team Player";
                Team_Player.Value = temp.Team_Player;
                list.Add(Team_Player);

                SubStrengthValueChart Independent = new SubStrengthValueChart();
                Independent.Name = "Independent";
                Independent.Value = temp.Independent;
                list.Add(Independent);

                SubStrengthValueChart Mentor = new SubStrengthValueChart();
                Mentor.Name = "Mentor";
                Mentor.Value = temp.Mentor;
                list.Add(Mentor);

                SubStrengthValueChart Analytics = new SubStrengthValueChart();
                Analytics.Name = "Analytics";
                Analytics.Value = temp.Analytics;
                list.Add(Analytics);

                SubStrengthValueChart Social_Philosophical = new SubStrengthValueChart();
                Social_Philosophical.Name = "Social Philosophical";
                Social_Philosophical.Value = temp.Social_Philosophical;
                list.Add(Social_Philosophical);

                SubStrengthValueChart Practical = new SubStrengthValueChart();
                Practical.Name = "Practical";
                Practical.Value = temp.Practical;
                list.Add(Practical);
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult AcadamicData(Academicdetails Academicdetails)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            if (ModelState.IsValid)
            {

                if (Academicdetails.Fichier1.ContentLength > 0)
                {
                    string Transcript = SingleSaveToPhysicalLocation(Academicdetails.Fichier1, "" + number + "_", "Academic", "Transcript" + Timestamp + "");

                    tblAcadenmouse tblAcadenmouse = new tblAcadenmouse();

                    tblAcadenmouse.StudentID = number;
                    tblAcadenmouse.Class = Academicdetails.Class;
                    tblAcadenmouse.Year = Academicdetails.Year;
                    tblAcadenmouse.Board = Academicdetails.Board;
                    tblAcadenmouse.School = Academicdetails.School;
                    tblAcadenmouse.Subject = Academicdetails.Subject;
                    tblAcadenmouse.Transcript = Transcript;
                    tblAcadenmouse.OverAllGPAMark = Academicdetails.OverAllGPAMark;
                    tblAcadenmouse.Createdby = number.ToString();
                    tblAcadenmouse.CreatedDateTime = DateTime.Now;
                    tblAcadenmouse.UpdatedBy = null;
                    tblAcadenmouse.UpdatedDateTime = null;
                    tblAcadenmouse.IsActive = "1";


                    db.tblAcadenmice.Add(tblAcadenmouse);

                    db.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }



        [HttpPost]
        public ActionResult UpdateExtraactivites(EditExtraCurricularActivites EditTestReport)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            using (InomiEntities entities = new InomiEntities())
            {
                tblExtraCurricularActivite tblAcadenmouse = (from c in entities.tblExtraCurricularActivites
                                                where c.Id == EditTestReport.id
                                                select c).FirstOrDefault();


                tblAcadenmouse.Activities = EditTestReport.Activities;
                tblAcadenmouse.Achievements = EditTestReport.Achievements;
                tblAcadenmouse.Details = EditTestReport.Details;

                tblAcadenmouse.UpdatedBy = number.ToString();
                tblAcadenmouse.UpdatedDateTime = DateTime.Now;



                int insertedRecords = entities.SaveChanges();
            }
            return RedirectToAction("Index");
        }



        [HttpPost]
        public ActionResult UpdateLeadership(EditLeadershipPositions EditTestReport)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            using (InomiEntities entities = new InomiEntities())
            {
                tblLeadershipPosition tblAcadenmouse = (from c in entities.tblLeadershipPositions
                                                where c.Id == EditTestReport.id
                                                select c).FirstOrDefault();


                tblAcadenmouse.Position = EditTestReport.Position;
                

                tblAcadenmouse.UpdatedBy = number.ToString();
                tblAcadenmouse.UpdatedDateTime = DateTime.Now;



                int insertedRecords = entities.SaveChanges();
            }
            return RedirectToAction("Index");
        }








        [HttpPost]
        public ActionResult UpdateTestReport(EditTestReport EditTestReport)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            using (InomiEntities entities = new InomiEntities())
            {
                tblTestReport tblAcadenmouse = (from c in entities.tblTestReports
                                                 where c.Id == EditTestReport.id
                                                 select c).FirstOrDefault();


                tblAcadenmouse.TestName = EditTestReport.TestName;
                tblAcadenmouse.Score = EditTestReport.Score;
           
                tblAcadenmouse.UpdatedBy = number.ToString();
                tblAcadenmouse.UpdatedDateTime = DateTime.Now;
              


                int insertedRecords = entities.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateAcademics(EditAcademicdetails Academicdetails)
        {
            number = int.Parse(Session["UserTypeId"].ToString());


            if (Academicdetails != null)
            {
                if (Academicdetails.Fichier1 != null)
                {
                    string Transcript = SingleSaveToPhysicalLocation(Academicdetails.Fichier1, "" + number + "_", "Academic", "Transcript" + Timestamp + "");
                    using (InomiEntities entities = new InomiEntities())
                    {
                        tblAcadenmouse tblAcadenmouse = (from c in entities.tblAcadenmice
                                                         where c.Id == Academicdetails.Id
                                                         select c).FirstOrDefault();


                        tblAcadenmouse.StudentID = number;
                        tblAcadenmouse.Class = Academicdetails.Class;
                        tblAcadenmouse.Year = Academicdetails.Year;
                        tblAcadenmouse.Board = Academicdetails.Board;
                        tblAcadenmouse.School = Academicdetails.School;
                        tblAcadenmouse.Subject = Academicdetails.Subject;
                        tblAcadenmouse.Transcript = Transcript;
                        tblAcadenmouse.OverAllGPAMark = Academicdetails.OverAllGPAMark;
                        tblAcadenmouse.UpdatedBy = number.ToString();
                        tblAcadenmouse.UpdatedDateTime = DateTime.Now;
                        


                        int insertedRecords = entities.SaveChanges();
                    }






                }
                else
                {
                    using (InomiEntities entities = new InomiEntities())
                    {
                        tblAcadenmouse tblAcadenmouse = (from c in entities.tblAcadenmice
                                                         where c.Id == Academicdetails.Id
                                                         select c).FirstOrDefault();


                        tblAcadenmouse.StudentID = number;
                        tblAcadenmouse.Class = Academicdetails.Class;
                        tblAcadenmouse.Year = Academicdetails.Year;
                        tblAcadenmouse.Board = Academicdetails.Board;
                        tblAcadenmouse.School = Academicdetails.School;
                        tblAcadenmouse.Subject = Academicdetails.Subject;

                        tblAcadenmouse.OverAllGPAMark = Academicdetails.OverAllGPAMark;
                        tblAcadenmouse.Createdby = number.ToString();
                        tblAcadenmouse.CreatedDateTime = DateTime.Now;
                        tblAcadenmouse.UpdatedBy = null;
                        tblAcadenmouse.UpdatedDateTime = null;


                        int insertedRecords = entities.SaveChanges();
                    }
                }



            }

            return RedirectToAction("Index");






        }
        [HttpGet]
        public ActionResult deleteacademics(int id)
        {
            using (InomiEntities entities = new InomiEntities())
            {
                tblAcadenmouse tblAcadenmouse = (from c in entities.tblAcadenmice
                                                 where c.Id == id
                                                 select c).FirstOrDefault();


                tblAcadenmouse.IsActive = "0";
                int insertedRecords = entities.SaveChanges();
            }

                return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult deletetestReports(int id)
        {
            using (InomiEntities entities = new InomiEntities())
            {
                tblTestReport tblAcadenmouse = (from c in entities.tblTestReports
                                                 where c.Id == id
                                                 select c).FirstOrDefault();


                tblAcadenmouse.IsActive = "0";
                int insertedRecords = entities.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult deleteExtraCurricularActivites(int id)
        {
            using (InomiEntities entities = new InomiEntities())
            {
                tblExtraCurricularActivite tblAcadenmouse = (from c in entities.tblExtraCurricularActivites
                                                where c.Id == id
                                                select c).FirstOrDefault();


                tblAcadenmouse.IsActive = "0";
                int insertedRecords = entities.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult deleteLeadershipPositions(int id)
        {
            using (InomiEntities entities = new InomiEntities())
            {
                tblLeadershipPosition tblAcadenmouse = (from c in entities.tblLeadershipPositions
                                                             where c.Id == id
                                                             select c).FirstOrDefault();


                tblAcadenmouse.IsActive = "0";
                int insertedRecords = entities.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        







        [HttpPost]
        public ActionResult TestReportData(TestReportDetails TestReportDetails)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            if (ModelState.IsValid)
            {

                tblTestReport tblTestReport = new tblTestReport();

                tblTestReport.StudentID = number;
                tblTestReport.TestName = TestReportDetails.TestName;
                tblTestReport.Score = TestReportDetails.Score;

                tblTestReport.Createdby = "" + number + "";
                tblTestReport.CreatedDateTime = DateTime.Now;
                tblTestReport.UpdatedBy = null;
                tblTestReport.UpdatedDateTime = null;
                tblTestReport.IsActive = "1";


                db.tblTestReports.Add(tblTestReport);

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ExtraCurricularActivitesData(ExtraCurricularActivitesDetails ExtraCurricularActivitesDetails)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            if (ModelState.IsValid)
            {

                tblExtraCurricularActivite tblExtraCurricularActivite = new tblExtraCurricularActivite();

                tblExtraCurricularActivite.StudentID = number;
                tblExtraCurricularActivite.Achievements = ExtraCurricularActivitesDetails.Achievements;
                tblExtraCurricularActivite.Activities = ExtraCurricularActivitesDetails.Activities;
                tblExtraCurricularActivite.Details = ExtraCurricularActivitesDetails.Details;
                tblExtraCurricularActivite.Createdby = "" + number + "";
                tblExtraCurricularActivite.CreatedDateTime = DateTime.Now;
                tblExtraCurricularActivite.UpdatedBy = null;
                tblExtraCurricularActivite.UpdatedDateTime = null;
                tblExtraCurricularActivite.IsActive = "1";


                db.tblExtraCurricularActivites.Add(tblExtraCurricularActivite);

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult LeadershipPositionsData(LeadershipPositionsDetails LeadershipPositionsDetails)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            if (ModelState.IsValid)
            {



                tblLeadershipPosition tblLeadershipPosition = new tblLeadershipPosition();

                tblLeadershipPosition.StudentID = number;
                tblLeadershipPosition.Position = LeadershipPositionsDetails.Position;
                tblLeadershipPosition.Createdby = "" + number + "";
                tblLeadershipPosition.CreatedDateTime = DateTime.Now;
                tblLeadershipPosition.UpdatedBy = null;
                tblLeadershipPosition.UpdatedDateTime = null;
                tblLeadershipPosition.IsActive = "1";


                db.tblLeadershipPositions.Add(tblLeadershipPosition);

                db.SaveChanges();



            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult MilestoneuploadData(milestoneupload pd)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            if (ModelState.IsValid)
            {

                pd.Upload = MultipleSaveToPhysicalLocation(pd.salaryslip, "" + number + "_", "MilestoneDocument", "Document", "Document" + Timestamp + "");

                studentmilestoneupload tblLeadershipPosition = new studentmilestoneupload();

                tblLeadershipPosition.STD = number;
                tblLeadershipPosition.MID = pd.MID;
                tblLeadershipPosition.Upload = pd.Upload;



                db.studentmilestoneuploads.Add(tblLeadershipPosition);

                db.SaveChanges();



            }
            return RedirectToAction("Projects");
        }

        [HttpPost]
        public ActionResult MilestoneuploadData1(milestoneupload pd)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            if (ModelState.IsValid)
            {

                pd.Upload = MultipleSaveToPhysicalLocation(pd.salaryslip, "" + number + "_", "MilestoneDocument", "Document", "Document" + Timestamp + "");

                studentmilestoneupload tblLeadershipPosition = new studentmilestoneupload();

                tblLeadershipPosition.STD = number;
                tblLeadershipPosition.MID = pd.MID;
                tblLeadershipPosition.Upload = pd.Upload;



                db.studentmilestoneuploads.Add(tblLeadershipPosition);

                db.SaveChanges();



            }
            return RedirectToAction("Index");
        }

        private string SingleSaveToPhysicalLocation(HttpPostedFileBase file, string Mainfolder, string Subfolder, string FileName)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            if (file != null && file.ContentLength > 0)
                try
                {
                    Random rnd = new Random();
                    int myRandomNo = rnd.Next(10000000, 99999999);

                    string FileExtension = Path.GetExtension(file.FileName);
                    string k = Path.Combine(Server.MapPath("~/"));
                    string directory = Path.Combine(Server.MapPath("~/UserImages/" + Mainfolder + "/" + Subfolder + ""));
                    //       string directory = @"D:/TeamComputer/REGISTERATIONFORMTEAMCOMPUTER/DATA/" + Mainfolder + "/" + Subfolder + "";
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    string path = Path.Combine(Server.MapPath("~/UserImages/" + Mainfolder + "/" + Subfolder + ""),
                                               Path.GetFileName(FileName + FileExtension));
                    file.SaveAs(path);
                    return "/" + path.Replace(k, "").Replace("\\", "/");
                    // ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    return string.Empty;

                }
            else
            {
                return string.Empty;

            }

        }

        public ActionResult UpcomingDeadline()
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            string currentdate = DateTime.Now.ToString("MM-dd-yyyy");

            var UpcomingDeadline = db.tblColleges.ToList().Where(a => a.IsActive == true && DateTime.ParseExact(a.ApplicationDeadline.Replace("/", "-"), "MM-dd-yyyy", CultureInfo.InvariantCulture).Year <= DateTime.ParseExact(currentdate.Replace("/", "-"), "MM-dd-yyyy", CultureInfo.InvariantCulture).Year && DateTime.ParseExact(a.ApplicationDeadline.Replace("/", "-"), "MM-dd-yyyy", CultureInfo.InvariantCulture).Year >= DateTime.ParseExact(currentdate.Replace("/", "-"), "MM-dd-yyyy", CultureInfo.InvariantCulture).Year && DateTime.ParseExact(a.ApplicationDeadline.Replace("/", "-"), "MM-dd-yyyy", CultureInfo.InvariantCulture).Month <= DateTime.ParseExact(currentdate.Replace("/", "-"), "MM-dd-yyyy", CultureInfo.InvariantCulture).Month && DateTime.ParseExact(a.ApplicationDeadline.Replace("/", "-"), "MM-dd-yyyy", CultureInfo.InvariantCulture).Month >= DateTime.ParseExact(currentdate.Replace("/", "-"), "MM-dd-yyyy", CultureInfo.InvariantCulture).Month).ToList();
            if (UpcomingDeadline.Count > 0)
            {
                foreach (var item in UpcomingDeadline)
                {
                    UpcomingDeadLineReport UpcomingDeadLineReport = new UpcomingDeadLineReport();
                    UpcomingDeadLineReport.EarlyDeadLine = item.EarlyDate;
                    UpcomingDeadLineReport.LateDeadLine = item.LateDate;
                    UpcomingDeadLineReport.FinalDeadLine = item.ApplicationDeadline;
                    UpcomingDeadLineReport.Campus = item.CampusName;
                    UpcomingDeadLineReport.University = item.NameOfCollege;



                    Dashboard.UpcomingDeadLineReport.Add(UpcomingDeadLineReport);
                }
            }

            return View("UpcomingDeadline", Dashboard);
        }

        public ActionResult DeadlineEssay()
        {
            number = int.Parse(Session["UserTypeId"].ToString());

            string currentdate = DateTime.Now.ToString("MM-dd-yyyy");

            var DeadlineEssay = (from pd in db.tblAssingTasks
                                 join oj in db.tblCollegeDetails on pd.EssayId.ToString() equals oj.Id.ToString()
                                 join od in db.tblColleges on oj.CID equals od.Id.ToString()
                                 where pd.IsActive == "1" && pd.Student == "" + number + "" && pd.TaskType == "Essay"
                                 //  && pd.DeadlineDate <= DateTime.Now.Date
                                 select new
                                 {
                                     pd.Id,
                                     od.NameOfCollege,
                                     oj.EssayName,
                                     oj.WordCount,
                                     oj.deadline,
                                     pd.Status,
                                 }).ToList();



            if (DeadlineEssay.Count > 0)
            {
                foreach (var item in DeadlineEssay)
                {
                    DeadLineEssay DeadLineEssay = new DeadLineEssay();
                    DeadLineEssay.ID = item.Id;

                    DeadLineEssay.NameOfCollege = item.NameOfCollege;
                    DeadLineEssay.EssayName = item.EssayName;
                    DeadLineEssay.WordCount = item.WordCount;
                    DeadLineEssay.deadline = item.deadline;
                    DeadLineEssay.Status = item.Status;

                    DeadLineEssay.LatestFile = db.UploadEssayFiles.Where(a => a.AssignTaskID == item.Id).OrderByDescending(z => z.CreatedDateTime).Take(1).Select(b => b.FilePath).DefaultIfEmpty("-").SingleOrDefault().ToString();



                    Dashboard.DeadLineEssay.Add(DeadLineEssay);
                }
            }

            return View("DeadLineEssay", Dashboard);
        }




        public ActionResult ViewUploadEssay(int Id)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            var k = (from pd in db.UploadEssayFiles

                     where pd.AssignTaskID == Id
                     select new
                     {
                         pd.AssignTaskID,
                         pd.FilePath,
                         pd.CreatedDateTime,
                     }).ToList();
            if (k.Count > 0)
            {
                foreach (var item in k)
                {
                    DeadLineEssayview DeadLineEssayview = new DeadLineEssayview();
                    DeadLineEssayview.LatestFile = item.FilePath;
                    DeadLineEssayview.CreatedDateTime = item.CreatedDateTime;
                    DeadLineEssayview.ID = item.AssignTaskID;

                    Dashboard.DeadLineEssayview.Add(DeadLineEssayview);
                }
            }

            return PartialView("_Details", Dashboard);
        }
        public ActionResult EditAcademics(int Id)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            var MilestoneData = db.EditAcedemicsDetails(Id.ToString()).SingleOrDefault();
            if (MilestoneData != null)
            {


                Dashboard.EditAcademics.Id = MilestoneData.Id.ToString();
                Dashboard.EditAcademics.StudentID = MilestoneData.StudentID;
                Dashboard.EditAcademics.Class = MilestoneData.Class;
                Dashboard.EditAcademics.Year = MilestoneData.Year;
                Dashboard.EditAcademics.Board = MilestoneData.Board;
                Dashboard.EditAcademics.School = MilestoneData.School;
                Dashboard.EditAcademics.Transcript = MilestoneData.Transcript;
                Dashboard.EditAcademics.OverAllGPAMark = MilestoneData.OverAllGPAMark;
                Dashboard.EditAcademics.Subject = MilestoneData.Subject;

            }

            return PartialView("_EditAcademics", Dashboard);
        }


        public ActionResult EditExtraCurricularActivites(int Id)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            var MilestoneData = db.EditActivitiesDetails(Id.ToString()).SingleOrDefault();
            if (MilestoneData != null)
            {


                Dashboard.EditExtraCurricularActivites.Activities = MilestoneData.Activities.ToString();
                Dashboard.EditExtraCurricularActivites.Achievements = MilestoneData.Achievements;
                Dashboard.EditExtraCurricularActivites.Details = MilestoneData.Details;
                Dashboard.EditExtraCurricularActivites.id = MilestoneData.Id;
                

            }

            return PartialView("_EditExtraactivities", Dashboard);
        }

        public ActionResult EditLeadershipPositions(int Id)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            var MilestoneData = db.EditLeadershipDetails(Id.ToString()).SingleOrDefault();
            if (MilestoneData != null)
            {


                Dashboard.EditLeadershipPositions.id = MilestoneData.Id;
                Dashboard.EditLeadershipPositions.Position = MilestoneData.Position;
            

            }

            return PartialView("_EditLeadershipDetails", Dashboard);
        }

        public ActionResult Edittestreport(int Id)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            var MilestoneData = db.EditTestScoreDetails(Id.ToString()).SingleOrDefault();
            if (MilestoneData != null)
            {


                Dashboard.EditTestReport.TestName = MilestoneData.TestName.ToString();
                Dashboard.EditTestReport.Score = MilestoneData.Score;
                Dashboard.EditTestReport.id = MilestoneData.Id;
                

            }

            return PartialView("_EditTestReport", Dashboard);
        }
        public ActionResult ViewMilestone(int Id)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            var MilestoneData = (from ep in db.tblProjectMilestones


                                 where ep.MId == Id
                                 select new
                                 {
                                     StudentID = ep.StudentID,
                                     Milestone = ep.Milestone,
                                     Deadline = ep.Deadline,
                                     status = ep.status,
                                     MId = ep.MId,
                                     Define = ep.Define,

                                 }).SingleOrDefault();
            if (MilestoneData != null)
            {


                Dashboard.Milestone.ID = MilestoneData.MId;
                Dashboard.Milestone.MilestoneName = MilestoneData.Milestone;
                Dashboard.Milestone.Milestonedate = MilestoneData.Deadline;
                Dashboard.Milestone.Status = MilestoneData.status;
                Dashboard.Milestone.Description = MilestoneData.Define;





            }

            return PartialView("_DetailsMilestone", Dashboard);
        }
        public ActionResult UploadEssay(UploadEssay UploadEssay)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            if (ModelState.IsValid)
            {
                if (UploadEssay.Fichier1.ContentLength > 0)
                {
                    string FilePath = SingleSaveToPhysicalLocation(UploadEssay.Fichier1, "" + number + "_", "UploadEssay", "UploadEssay_" + Timestamp + "");

                    UploadEssayFile UploadEssayFile = new UploadEssayFile();

                    UploadEssayFile.AssignTaskID = Convert.ToInt32(UploadEssay.UploadId);
                    UploadEssayFile.FilePath = FilePath;
                    UploadEssayFile.CreatedDateTime = DateTime.Now;
                    UploadEssayFile.Createdby = "Student";



                    db.UploadEssayFiles.Add(UploadEssayFile);

                    db.SaveChanges();
                }

            }
            return RedirectToAction("DeadLineEssay");
        }

        public FileResult Download(string CurrentFileName)
        {
            number = int.Parse(Session["UserTypeId"].ToString());

            string contentType = string.Empty;

            if (CurrentFileName.Contains(".pdf"))
            {
                contentType = "application/pdf";
                return File(CurrentFileName, contentType, CurrentFileName);
            }

            else if (CurrentFileName.Contains(".docx"))
            {
                contentType = "application/docx";
                return File(CurrentFileName, contentType, CurrentFileName);
            }
            else if (CurrentFileName.Contains(".jpeg") || CurrentFileName.Contains(".png") || CurrentFileName.Contains(".jpg"))
            {
                contentType = "Images/png";
                return File(CurrentFileName, contentType, CurrentFileName);
            }
            else if (CurrentFileName.Contains(".xlsx"))
            {
                contentType = "application/vnd.ms-excel";
                return File(CurrentFileName, contentType, CurrentFileName);
            }
            else if (CurrentFileName.Contains(".png"))
            {
                contentType = "application/vnd.ms-excel";
                return File(CurrentFileName, contentType, CurrentFileName);
            }


            return File(CurrentFileName, contentType, CurrentFileName);
        }




        public ActionResult message()
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            string currentdate = DateTime.Now.ToString("MM-dd-yyyy");

            var MessageDetails = db.tblAssingTasks.ToList().Where(a => a.IsActive == "1" && a.TaskType == "Message" && a.Student == "" + number + "").ToList();
            if (MessageDetails.Count > 0)
            {
                foreach (var item in MessageDetails)
                {
                    Studentmessage Studentmessage = new Studentmessage();
                    Studentmessage.Message = item.TaskDescription;




                    Dashboard.Studentmessage.Add(Studentmessage);
                }
            }

            return View("message", Dashboard);
        }


        public ActionResult projects()
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            var ProjectData = (from ep in db.tblProjects


                               where ep.StudentID == number && ep.IsActive == "1"
                               select new
                               {
                                   ProjectID = ep.ProjectID,
                                   Name = ep.Name,
                                   Description = ep.Description


                               }).OrderByDescending(m => m.ProjectID).ToList();
            if (ProjectData.Count > 0)
            {
                foreach (var item in ProjectData)
                {
                    Project Project = new Project();
                    Project.Projectid = item.ProjectID;
                    Project.Name = item.Name;
                    Project.Description = item.Description;


                    var MilestoneData = (from ep in db.tblProjectMilestones


                                         where ep.ProjectID == Project.Projectid
                                         select new
                                         {
                                             StudentID = ep.StudentID,
                                             Milestone = ep.Milestone,
                                             Deadline = ep.Deadline,
                                             status = ep.status,
                                             MId = ep.MId,
                                             Define = ep.Define,

                                         }).ToList();
                    if (MilestoneData.Count > 0)
                    {
                        foreach (var item1 in MilestoneData)
                        {
                            Milestone Milestone = new Milestone();
                            Milestone.ID = item1.MId;
                            Milestone.MilestoneName = item1.Milestone;
                            Milestone.Milestonedate = item1.Deadline;
                            Milestone.Status = item1.status;
                            Milestone.Description = item1.Define;



                            Project.Milestone.Add(Milestone);
                        }
                    }




                    Dashboard.Project.Add(Project);
                }
            }

            return View("projects", Dashboard);
        }
        [HttpGet]
        public ActionResult interestinglinks()
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            var interestinglinksData = (from ep in db.interestinglinks


                                        where ep.STD == number
                                        select new
                                        {
                                            Title = ep.Title,
                                            description = ep.description,
                                            CreatedDateTime = ep.CreatedDateTime


                                        }).OrderByDescending(m => m.CreatedDateTime).ToList();
            if (interestinglinksData.Count > 0)
            {
                foreach (var item in interestinglinksData)
                {
                    Interestinglinksview interestinglink = new Interestinglinksview();
                    interestinglink.Title = item.Title;
                    interestinglink.Createddate = item.CreatedDateTime;
                    interestinglink.description = item.description;
                    Dashboard.Interestinglinksview.Add(interestinglink);
                }
            }

            return View("interestinglinks", Dashboard);
        }
        [HttpPost]
        public ActionResult interestinglinks(Interestinglinksdetails Interestinglinksdetails)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            if (ModelState.IsValid)
            {



                interestinglink interestinglink = new interestinglink();

                interestinglink.STD = number;
                interestinglink.Title = Interestinglinksdetails.Title;
                interestinglink.description = Interestinglinksdetails.ckeExample;
                interestinglink.CreatedDateTime = DateTime.Now;



                db.interestinglinks.Add(interestinglink);

                db.SaveChanges();



            }
            return RedirectToAction("interestinglinks", Dashboard);


        }

        [HttpGet]
        public ActionResult callnote()
        {

            number = int.Parse(Session["UserTypeId"].ToString());
            var callnoteData = (from ep in db.tblCallNotes


                                where ep.STD == number
                                select new
                                {
                                    CallNoteDate = ep.CallNoteDate,
                                    Academics = ep.Academics,
                                    Testing = ep.Testing,
                                    College = ep.College,
                                    Project1 = ep.Project1,
                                    Project2 = ep.Project2,
                                    ExtraCurricular = ep.ExtraCurricular,

                                    CreatedDateTime = ep.createdDate


                                }).OrderByDescending(m => m.CreatedDateTime).ToList();
            if (callnoteData.Count > 0)
            {
                foreach (var item in callnoteData)
                {
                    CallNote CallNote = new CallNote();
                    CallNote.Date = item.CallNoteDate;
                    CallNote.Academics = item.Academics;
                    CallNote.Testing = item.Testing;
                    CallNote.College = item.College;
                    CallNote.Project1 = item.Project1;
                    CallNote.Project2 = item.Project2;
                    CallNote.ExtraCurricular = item.ExtraCurricular;


                    Dashboard.CallNote.Add(CallNote);
                }
            }

            return View("callnote", Dashboard);
        }

        public JsonResult InsertCallNote(List<tblCallNote> customers)
        {

            number = int.Parse(Session["UserTypeId"].ToString());

            if (customers.Count > 0)
            {


                using (InomiEntities entities = new InomiEntities())
                {
                    //Truncate Table to delete all old records.
                    //  entities.Database.ExecuteSqlCommand("TRUNCATE TABLE [Customers]");

                    //Check for NULL.
                    if (customers == null)
                    {
                        customers = new List<tblCallNote>();
                    }
                    tblCallNote tblCallNote = new tblCallNote();
                    //Loop and insert records.
                    foreach (tblCallNote customer in customers)
                    {
                        customer.STD = number;
                        customer.createdDate = DateTime.Now;
                        entities.tblCallNotes.Add(customer);
                    }
                    int insertedRecords = entities.SaveChanges();
                    return Json(insertedRecords);
                }
            }
            return Json(0);
        }


        public JsonResult StatusRecord(selectedcollege customers)
        {

            number = int.Parse(Session["UserTypeId"].ToString());

            if (customers != null)
            {


                using (InomiEntities entities = new InomiEntities())
                {
                    //Truncate Table to delete all old records.
                    //  entities.Database.ExecuteSqlCommand("TRUNCATE TABLE [Customers]");

                    //Check for NULL.
                    if (customers == null)
                    {
                        customers = new selectedcollege();
                    }
                    selectedcollege tblCallNote = new selectedcollege();
                    //Loop and insert records.

                    tblCallNote.CID = customers.id;
                    tblCallNote.Status = customers.Status;
                    tblCallNote.StuId = number;
                    entities.selectedcolleges.Add(tblCallNote);

                    int insertedRecords = entities.SaveChanges();
                    return Json(insertedRecords);
                }
            }
            return Json(0);
        }

        public ActionResult applicationview2(string college, string country, string city, string course, string ranklist, string shortlist, string application)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            var data = db.FilterData(college, country, city, course, ranklist, shortlist, application).ToList();


            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    Collegeshortlist CallNote = new Collegeshortlist();
                    CallNote.Id = item.Id;
                    CallNote.NameOfCollege = item.NameOfCollege;
                    CallNote.Description = item.Description;
                    CallNote.Status = item.Status;



                    Dashboard.Collegeshortlist.Add(CallNote);
                }
            }

            return PartialView("FilterData", Dashboard);
        }

        [HttpGet]

        public ActionResult applicationview()
        {
            ViewBag.CountryList = ToSelectList1((from A in db.tblCountryNames select new DropDownModel { Id = A.CID.ToString(), Value = A.Name }).ToList());
            ViewBag.CollegeList = ToSelectList1((from A in db.tblColleges select new DropDownModel { Id = A.Id.ToString(), Value = A.NameOfCollege }).ToList());
            ViewBag.ApplicationThrough = ToSelectList1((from A in db.tblApplicationModes select new DropDownModel { Id = A.Id.ToString(), Value = A.Name }).ToList());
            ViewBag.courselist = ToSelectList1((from A in db.tblCourses select new DropDownModel { Id = A.Id.ToString(), Value = A.NameOfCourse }).ToList());
            var data = db.sp_collegeshortlist().ToList();
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    Collegeshortlist CallNote = new Collegeshortlist();
                    CallNote.Id = item.Id;
                    CallNote.NameOfCollege = item.NameOfCollege;
                    CallNote.Description = item.Description;
                    CallNote.Status = item.Status;



                    Dashboard.Collegeshortlist.Add(CallNote);
                }
            }
            //var result = (from ep in db.tblColleges


            //                    where ep.IsActive == true
            //                    select new
            //                    {
            //                        Data = ep.SATScore





            //                    }).Distinct().ToList();

            //if (result.Count > 0)
            //{

            //        foreach (var item in result)
            //        {
            //            ranklist CallNote = new ranklist();
            //            CallNote.Data = item.Data;




            //            Dashboard.ranklist.Add(CallNote);
            //        }
            //    }



            return View("applicationview", Dashboard);
        }
        //public ActionResult applicationview()
        //{
        //    number = int.Parse(Session["UserTypeId"].ToString());
        //    ViewBag.CountryList = ToSelectList1((from A in db.tblCountries select new DropDownModel { Id = A.Id.ToString(), Value = A.NameOfCountry }).ToList());
        //    ViewBag.CollegeList = ToSelectList1((from A in db.tblColleges select new DropDownModel { Id = A.Id.ToString(), Value = A.NameOfCollege }).ToList());

        //    var data = db.sp_collegeshortlist().ToList();
        //    if (data.Count > 0)
        //    {
        //        foreach (var item in data)
        //        {
        //            Collegeshortlist CallNote = new Collegeshortlist();
        //            CallNote.Id = item.Id;
        //            CallNote.NameOfCollege = item.NameOfCollege;
        //            CallNote.Description = item.Description;
        //            CallNote.Status = item.Status;



        //            Dashboard.Collegeshortlist.Add(CallNote);
        //        }
        //    }
        //    //var result = (from ep in db.tblColleges


        //    //                    where ep.IsActive == true
        //    //                    select new
        //    //                    {
        //    //                        Data = ep.SATScore





        //    //                    }).Distinct().ToList();

        //    //if (result.Count > 0)
        //    //{

        //    //        foreach (var item in result)
        //    //        {
        //    //            ranklist CallNote = new ranklist();
        //    //            CallNote.Data = item.Data;




        //    //            Dashboard.ranklist.Add(CallNote);
        //    //        }
        //    //    }



        //    return View("applicationview", Dashboard);
        //}



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
        [HttpGet]
        public ActionResult LoR(string FooBarDropDown)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            if (FooBarDropDown == "LoR")
            {


                var LoRData = (from ep in db.tblFormats


                               where ep.FileType == "LoR" && ep.IsActive == true
                               select new
                               {
                                   FilePath = ep.FilePath,
                                   FileSize = ep.FileSize,
                                   FileName = ep.Format


                               }).ToList();
                if (LoRData.Count > 0)
                {
                    foreach (var item in LoRData)
                    {
                        LORDetail CallNote = new LORDetail();
                        CallNote.LoRspath = item.FilePath;
                        CallNote.LoRsSize = ConvertBytesToMegabytes(Convert.ToInt32(item.FileSize)).ToString("00.00");
                        CallNote.LoRs = item.FileName;


                        Dashboard.LORDetail.Add(CallNote);
                    }
                }
            }
            else if (FooBarDropDown == "Bragsheet")

            {
                var LoRData = (from ep in db.tblFormats


                               where ep.FileType == "Bragsheet" && ep.IsActive == true
                               select new
                               {
                                   FilePath = ep.FilePath,
                                   FileSize = ep.FileSize,
                                   FileName = ep.Format



                               }).ToList();
                if (LoRData.Count > 0)
                {
                    foreach (var item in LoRData)
                    {
                        LORDetail CallNote = new LORDetail();
                        CallNote.LoRspath = item.FilePath;
                        CallNote.LoRsSize = ConvertBytesToMegabytes(Convert.ToInt32(item.FileSize)).ToString("00.00");
                        CallNote.LoRs = item.FileName;


                        Dashboard.LORDetail.Add(CallNote);
                    }
                }
            }
            else
            {
                var LoRData = (from ep in db.tblFormats


                               where ep.IsActive == true
                               select new
                               {
                                   FilePath = ep.FilePath,
                                   FileSize = ep.FileSize,
                                   FileName = ep.Format



                               }).ToList();
                if (LoRData.Count > 0)
                {
                    foreach (var item in LoRData)
                    {
                        LORDetail CallNote = new LORDetail();
                        CallNote.LoRspath = item.FilePath;
                        CallNote.LoRsSize = ConvertBytesToMegabytes(Convert.ToInt32(item.FileSize)).ToString("00.00");

                        CallNote.LoRs = item.FileName;

                        Dashboard.LORDetail.Add(CallNote);
                    }
                }
            }


            return View("LoR", Dashboard);
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
        static double ConvertBytesToMegabytes(int bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        private string MultipleSaveToPhysicalLocation(HttpPostedFileBase[] files, string Mainfolder, string sub, string Subfolder, string filename)
        {
            number = int.Parse(Session["UserTypeId"].ToString());
            string SalarySlip = "";
            string K;
            int i = 0;

            try
            {
                K = Path.Combine(Server.MapPath("~"));
                foreach (HttpPostedFileBase file in files)
                {

                    //Checking file is available to save.  
                    if (file != null)
                    {
                        Random rnd = new Random();
                        int myRandomNo = rnd.Next(10000000, 99999999);
                        string FileExtension = Path.GetExtension(file.FileName);


                        string path = Path.Combine(Server.MapPath("~/UserImages/" + Mainfolder + "/" + Subfolder + ""));

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        i++;
                        string ServerSavePath = Path.Combine(Server.MapPath("~/UserImages/" + Mainfolder + "/" + Subfolder + ""),
                        Path.GetFileName(filename + FileExtension));
                        file.SaveAs(ServerSavePath);

                        SalarySlip = SalarySlip + ServerSavePath.ToString() + ";";



                    }

                }
                SalarySlip = SalarySlip.Replace(K, "").Replace("\\", "/");
                return SalarySlip.Substring(0, SalarySlip.Length - 1);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }


        }


        public ActionResult StudentCalendar(string StrMain)
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
        public JsonResult GetCalendarEvents()
        {
            var events = new List<Event>();
            DataTable dt = new DataTable();
            string UsertypeId = Session["UserTypeId"].ToString();
            string UserType = Session["UserType"].ToString();
            dt = CalendarCon.GetCalendarEvnetDetails(UsertypeId, UserType);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; dt.Rows.Count > i; i++)
                {

                    Event eventDetails = new Event();
                    string Rid = dt.Rows[i]["EventId"].ToString();
                    int EventId = int.Parse(Rid);

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
        }
        public void InsertCalendarEventDetails(string json)
        {
            string UsertypeId = Session["UserTypeId"].ToString();
            XmlDocument XmlDoc;

            XmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + json + "}", "Event");

            CalendarCon.InsertCalendarEventDetails(XmlDoc.InnerXml, UsertypeId);
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

        public ActionResult ToDoListCalendar(string StrMain)
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
        public JsonResult ToDoListGetEvents()
        {
            var events = new List<Event>();
            DataTable dt = new DataTable();
            string UsertypeId = Session["UserTypeId"].ToString();
            string UserType = Session["UserType"].ToString();
            dt = CalendarCon.GetToDoListEvnetDetails(UsertypeId, UserType);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; dt.Rows.Count > i; i++)
                {

                    Event eventDetails = new Event();
                    string Rid = dt.Rows[i]["EventId"].ToString();
                    int EventId = int.Parse(Rid);

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
        }
        public void InsertToDoListEventDetails(string json)
        {
            string UsertypeId = Session["UserTypeId"].ToString();
            XmlDocument XmlDoc;

            XmlDoc = (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Details\":" + json + "}", "Event");

            CalendarCon.InsertDoToListEventDetails(XmlDoc.InnerXml, UsertypeId);
        }

        public JsonResult ToDashboardGetEvents()
        {
            var events = new List<Event>();
            DataTable dt = new DataTable();
            string UsertypeId = Session["UserTypeId"].ToString();
            string UserType = Session["UserType"].ToString();
            dt = CalendarCon.GetDashboardEvnetDetails(UsertypeId, UserType);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; dt.Rows.Count > i; i++)
                {

                    Event eventDetails = new Event();
                    string Rid = dt.Rows[i]["EventId"].ToString();
                    int EventId = int.Parse(Rid);

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
        }
    }
}