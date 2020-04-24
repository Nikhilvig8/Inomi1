using DataLayer;
using Inomi.Models.Questionnaire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Inomi.Controllers.Questionnaire
{
    public class HomeQuestionnaireController : Controller
    {
       
        private InomiEntities db = new InomiEntities();
        public ActionResult TermsAndConditions()
        {
            if (Request.IsAuthenticated)
            {
                string UserName = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                tblQuestionnaireSubStrengthResult temp = db.tblQuestionnaireSubStrengthResults.Where(x => x.UserName == UserName).FirstOrDefault();

                tblQuestionnaireLogin login = db.tblQuestionnaireLogins.Where(x => x.UserName == UserName).FirstOrDefault();
                if (login.IsAdmin == "1")
                {
                    return RedirectToAction("Career", "AdminQuestionnaire");
                }


                if (temp == null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("DashBoard", "DashBoardQuestionnaire");
                }
            }
            else
            {
                return RedirectToAction("Login", "LoginQuestionnaire");
            }
        }

        public ActionResult QuestionSelect()
        {
            if (Request.IsAuthenticated)
            {
                string UserName = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

                tblQuestionnaireSubStrengthResult temp = db.tblQuestionnaireSubStrengthResults.Where(x => x.UserName == UserName).FirstOrDefault();

                tblQuestionnaireLogin login = db.tblQuestionnaireLogins.Where(x => x.UserName == UserName).FirstOrDefault();
                if (login.IsAdmin == "1")
                {
                    return RedirectToAction("Career", "AdminQuestionnaire");
                }

                if (temp == null)
                {
                    List<Sp_QuestionList_Result> list = db.Sp_QuestionList().ToList();
                    return View(list);
                }
                else
                {
                    return RedirectToAction("DashBoard", "DashBoardQuestionnaire");
                }
            }
            else
            {
                return RedirectToAction("Login", "LoginQuestionnaire");
            }
        }

        [HttpPost]
        public JsonResult QuestionSelect(string QuestionSelect)
        {
            if (QuestionSelect != null && QuestionSelect != "")
            {
                Session["QuestionSelect"] = QuestionSelect;
                return Json(new { Status = true });
            }
            else
            {
                return Json(new { Status = false });
            }
        }


        public ActionResult Questions()
        {
            if (Request.IsAuthenticated)
            {
                string UserName = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

                tblQuestionnaireSubStrengthResult temp = db.tblQuestionnaireSubStrengthResults.Where(x => x.UserName == UserName).FirstOrDefault();

                tblQuestionnaireLogin login = db.tblQuestionnaireLogins.Where(x => x.UserName == UserName).FirstOrDefault();
                if (login.IsAdmin == "1")
                {
                    return RedirectToAction("Career", "AdminQuestionnaire");
                }

                if (temp == null)
                {
                    string QuestionSelect = Session["QuestionSelect"].ToString();

                    if (QuestionSelect == "" && QuestionSelect == null)
                    {
                        return RedirectToAction("QuestionSelect", "HomeQuestionnaire");
                    }
                    else
                    {
                        List<Sp_QuestionChecked_Result> list = db.Sp_QuestionChecked(QuestionSelect).ToList();
                        return View(list);
                    }
                }
                else
                {
                    return RedirectToAction("DashBoard", "DashBoardQuestionnaire");
                }

            }
            else
            {
                return RedirectToAction("Login", "LoginQuestionnaire");
            }
        }

        [HttpPost]
        public JsonResult Questions(int[] marks)
        {
            if (marks != null)
            {
                string UserName = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                for (int i = 0; i < marks.Length; i++)
                {
                    int count = db.tblQuestionnaireSubStrengthQuestions.Where(x => x.SubStrengthId == i).Count();
                    int limit = 15 * count;
                    if (marks[i] > limit)
                    {
                        return Json(new { Status = false });
                    }
                }

                tblQuestionnaireSubStrengthResult temp = db.tblQuestionnaireSubStrengthResults.Where(x => x.UserName == UserName).FirstOrDefault();

                if (temp == null)
                {
                    tblQuestionnaireSubStrengthResult result = new tblQuestionnaireSubStrengthResult();
                    result.UserName = UserName;
                    result.Artistic = marks[1];
                    result.Aesthetic = marks[2];
                    result.Idea_Generator = marks[3];
                    result.Team_Player = marks[4];
                    result.Independent = marks[5];
                    result.Mentor = marks[6];
                    result.Analytics = marks[7];
                    result.Social_Philosophical = marks[8];
                    result.Practical = marks[9];
                    result.Average = Convert.ToDecimal(marks.Sum()) / 9;

                    db.tblQuestionnaireSubStrengthResults.Add(result);
                    db.SaveChanges();

                    return Json(new { Status = true });
                }
                else
                {
                    return Json(new { Status = false });
                }


            }
            else
            {
                return Json(new { Status = false });
            }
        }
    }
}