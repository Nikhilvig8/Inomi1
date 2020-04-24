using DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Inomi.Controllers.Questionnaire
{
    public class AdminQuestionnaireController : Controller
    {
        private InomiEntities db = new InomiEntities();
        // GET: Admin
        
        public ActionResult Career()
        {
            if (Request.IsAuthenticated)
            {
                string UserName = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                tblQuestionnaireLogin temp = db.tblQuestionnaireLogins.Where(x => x.UserName == UserName).FirstOrDefault();
                if (temp.IsAdmin != "1")
                {
                    FormsAuthentication.SignOut();
                    Session.Abandon();
                    return RedirectToAction("Login", "LoginQuestionnaire");
                }
                List<tblQuestionnaireFieldLogic> list = db.tblQuestionnaireFieldLogics.ToList();

                return View(list);
            }
            else
            {
                return RedirectToAction("Login", "LoginQuestionnaire");
            }
        }

        public ActionResult CareerData(int id)
        {
            if (Request.IsAuthenticated)
            {
                string UserName = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                tblQuestionnaireLogin temp = db.tblQuestionnaireLogins.Where(x => x.UserName == UserName).FirstOrDefault();
                if (temp.IsAdmin != "1")
                {
                    FormsAuthentication.SignOut();
                    Session.Abandon();
                    return RedirectToAction("Login", "LoginQuestionnaire");
                }
                ViewBag.CareerFields = db.tblQuestionnaireFieldLogics.Where(x => x.Id == id).FirstOrDefault();
                ViewBag.Description = db.tblQuestionnaireCareerDescriptions.Where(x => x.Id == id).FirstOrDefault();
                ViewBag.CourseWork = db.tblQuestionnaireCourseWorks.Where(x => x.Id == id).FirstOrDefault();
                ViewBag.Videos = db.tblQuestionnaireVideos.Where(x => x.CareerId == id).ToList();
                ViewBag.Reading = db.tblQuestionnaireReadings.Where(x => x.CareerId == id).ToList();
                ViewBag.OnlineCourses = db.tblQuestionnaireOnlineCourses.Where(x => x.CareerId == id).ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "LoginQuestionnaire");
            }
        }


        public JsonResult UpdateCareer(string careerId, string maindescription, string course, string subdescription, string reading, string videos, string online)
        {
            int career = Convert.ToInt32(careerId);
            int result;
            ObjectParameter returnId = new ObjectParameter("ERROR", typeof(int));
            using (InomiEntities db = new InomiEntities())
            {

                var resultvalue = db.Sp_UpdateCareer(career, maindescription, subdescription, course, reading, videos, online, returnId).FirstOrDefault();
                result = (int)resultvalue;
            };


            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}