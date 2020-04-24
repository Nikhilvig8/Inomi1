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
using System.Web.UI;

using BusinessLayer;
using System.Data;


namespace Inomi.Controllers
{
    public class CounsellorEssayController : Controller
    {
        InomiEntities db = new InomiEntities();
        Dashboard Dashboard = new Dashboard();
        double Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        int number = 0;
        [SessionExpireAttribute]
        public ActionResult EssayApprove()
        {
            number = int.Parse(Session["UserTypeId"].ToString());

            string currentdate = DateTime.Now.ToString("MM-dd-yyyy");

            var DeadlineEssay = (from pd in db.tblAssingTasks
                                 join oj in db.tblCollegeDetails on pd.EssayId.ToString() equals oj.Id.ToString()
                                 join od in db.tblColleges on oj.CID equals od.Id.ToString()
                                 where pd.IsActive == "1" && pd.Counsellor == "" + number + "" && pd.TaskType == "Essay"
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

            return View("EssayApprove", Dashboard);
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
            return RedirectToAction("EssayApprove");
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
            else if (CurrentFileName.Contains(".jpeg") || CurrentFileName.Contains(".png"))
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

        public void ApproveStatusEssay(string id)
        {
            StudentCon.ApproveStatusEssay(id);
        }

        public void ApproveBackEssay(string id)
        {
            StudentCon.ApproveBackStatus(id);
        }
    }
}