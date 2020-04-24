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
    public class DashBoardQuestionnaireController : Controller
    {
        private InomiEntities db = new InomiEntities();


        // GET: DashBoard
        public ActionResult DashBoard()
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
                    return RedirectToAction("TermsAndConditions", "HomeQuestionnaire");
                }
                else
                {
                    tblQuestionnaireFieldLogicResult tblQuestionnaireFieldLogicResult = db.tblQuestionnaireFieldLogicResults.Where(x => x.UserName == UserName).FirstOrDefault();

                    if (tblQuestionnaireFieldLogicResult == null)
                    {
                        List<SubStrengthValue> list = new List<SubStrengthValue>();

                        SubStrengthValue Artistic = new SubStrengthValue();
                        Artistic.Name = 1;
                        Artistic.Value = temp.Artistic;
                        list.Add(Artistic);

                        SubStrengthValue Aesthetic = new SubStrengthValue();
                        Aesthetic.Name = 2;
                        Aesthetic.Value = temp.Aesthetic;
                        list.Add(Aesthetic);

                        SubStrengthValue Idea_Generator = new SubStrengthValue();
                        Idea_Generator.Name = 3;
                        Idea_Generator.Value = temp.Idea_Generator;
                        list.Add(Idea_Generator);

                        SubStrengthValue Team_Player = new SubStrengthValue();
                        Team_Player.Name = 4;
                        Team_Player.Value = temp.Team_Player;
                        list.Add(Team_Player);

                        SubStrengthValue Independent = new SubStrengthValue();
                        Independent.Name = 5;
                        Independent.Value = temp.Independent;
                        list.Add(Independent);

                        SubStrengthValue Mentor = new SubStrengthValue();
                        Mentor.Name = 6;
                        Mentor.Value = temp.Mentor;
                        list.Add(Mentor);

                        SubStrengthValue Analytics = new SubStrengthValue();
                        Analytics.Name = 7;
                        Analytics.Value = temp.Analytics;
                        list.Add(Analytics);

                        SubStrengthValue Social_Philosophical = new SubStrengthValue();
                        Social_Philosophical.Name = 8;
                        Social_Philosophical.Value = temp.Social_Philosophical;
                        list.Add(Social_Philosophical);

                        SubStrengthValue Practical = new SubStrengthValue();
                        Practical.Name = 9;
                        Practical.Value = temp.Practical;
                        list.Add(Practical);

                        list = list.Where(x => x.Value > temp.Average).OrderByDescending(x => x.Value).ToList();

                        List<tblQuestionnaireFieldLogic> fieldLogic = db.tblQuestionnaireFieldLogics.ToList();

                        foreach (var item in fieldLogic)
                        {
                            tblQuestionnaireFieldLogicResult result = new tblQuestionnaireFieldLogicResult();
                            result.CareerId = item.Id;
                            result.UserName = UserName;
                            result.MainField = item.MainField;
                            result.SubField = item.SubField;
                            result.Critical1 = list.Where(x => x.Name == item.Critical1).Select(x => x.Value).FirstOrDefault();
                            result.Critical2 = list.Where(x => x.Name == item.Critical2).Select(x => x.Value).FirstOrDefault();
                            result.Critical3 = list.Where(x => x.Name == item.Critical3).Select(x => x.Value).FirstOrDefault();

                            if (item.HygieneFactor == 0)
                            {
                                result.HygieneFactor = 1;
                            }
                            else if (item.HygieneFactor == 1)
                            {
                                if (temp.Artistic > temp.Average)
                                {
                                    result.HygieneFactor = 1;
                                }
                                else
                                {
                                    result.HygieneFactor = 0;
                                }
                            }
                            else if (item.HygieneFactor == 2)
                            {
                                if (temp.Aesthetic > temp.Average)
                                {
                                    result.HygieneFactor = 1;
                                }
                                else
                                {
                                    result.HygieneFactor = 0;
                                }
                            }
                            else if (item.HygieneFactor == 3)
                            {
                                if (temp.Idea_Generator > temp.Average)
                                {
                                    result.HygieneFactor = 1;
                                }
                                else
                                {
                                    result.HygieneFactor = 0;
                                }
                            }
                            else if (item.HygieneFactor == 4)
                            {
                                if (temp.Team_Player > temp.Average)
                                {
                                    result.HygieneFactor = 1;
                                }
                                else
                                {
                                    result.HygieneFactor = 0;
                                }
                            }
                            else if (item.HygieneFactor == 5)
                            {
                                if (temp.Independent > temp.Average)
                                {
                                    result.HygieneFactor = 1;
                                }
                                else
                                {
                                    result.HygieneFactor = 0;
                                }
                            }
                            else if (item.HygieneFactor == 6)
                            {
                                if (temp.Mentor > temp.Average)
                                {
                                    result.HygieneFactor = 1;
                                }
                                else
                                {
                                    result.HygieneFactor = 0;
                                }
                            }
                            else if (item.HygieneFactor == 7)
                            {
                                if (temp.Analytics > temp.Average)
                                {
                                    result.HygieneFactor = 1;
                                }
                                else
                                {
                                    result.HygieneFactor = 0;
                                }
                            }
                            else if (item.HygieneFactor == 8)
                            {
                                if (temp.Social_Philosophical > temp.Average)
                                {
                                    result.HygieneFactor = 1;
                                }
                                else
                                {
                                    result.HygieneFactor = 0;
                                }
                            }
                            else
                            {
                                if (temp.Practical > temp.Average)
                                {
                                    result.HygieneFactor = 1;
                                }
                                else
                                {
                                    result.HygieneFactor = 0;
                                }
                            }

                            result.Contra1 = list.Where(x => x.Name == item.Contra1).Select(x => x.Value).FirstOrDefault();
                            result.Contra2 = list.Where(x => x.Name == item.Contra2).Select(x => x.Value).FirstOrDefault();

                            if (item.HowItWorks == 1)
                            {
                                if (result.HygieneFactor == 1 && result.Contra1 == 0 && result.Contra2 == 0)
                                {
                                    if (result.Critical1 == 0)
                                    {
                                        result.Result = 25;
                                    }
                                    else
                                    {
                                        if (item.Critical2 != 0 && result.Critical2 == 0)
                                        {
                                            result.Result = 75;
                                        }
                                        else
                                        {
                                            if (item.Critical3 != 0 && result.Critical3 == 0)
                                            {
                                                result.Result = 90;
                                            }
                                            else
                                            {
                                                result.Result = 100;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (result.Critical1 > 0)
                                    {
                                        result.Result = 25;
                                    }
                                    else
                                    {
                                        result.Result = 0;
                                    }
                                }
                            }
                            else if (item.HowItWorks == 2)
                            {
                                if (result.HygieneFactor == 1 && result.Contra1 == 0 && result.Contra2 == 0)
                                {
                                    if (result.Critical1 == 0 && result.Critical2 == 0)
                                    {
                                        result.Result = 25;
                                    }
                                    else
                                    {
                                        if (result.Critical1 == 0 || result.Critical2 == 0)
                                        {
                                            result.Result = 50;
                                        }
                                        else
                                        {
                                            if (item.Critical3 != 0 && result.Critical3 == 0)
                                            {
                                                result.Result = 90;
                                            }
                                            else
                                            {
                                                result.Result = 100;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (result.Critical1 == 0 && result.Critical2 == 0)
                                    {
                                        result.Result = 0;
                                    }
                                    else
                                    {
                                        result.Result = 25;
                                    }
                                }
                            }
                            else if (item.HowItWorks == 3)
                            {
                                if (result.HygieneFactor == 1 && result.Contra1 == 0 && result.Contra2 == 0)
                                {
                                    if (result.Critical1 == 0)
                                    {
                                        if (result.Critical2 == 0)
                                        {
                                            if (result.Critical3 == 0)
                                            {
                                                result.Result = 0;
                                            }
                                            else
                                            {
                                                result.Result = 25;
                                            }
                                        }
                                        else
                                        {
                                            if (result.Critical3 == 0)
                                            {
                                                result.Result = 25;
                                            }
                                            else
                                            {
                                                result.Result = 50;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (result.Critical2 == 0)
                                        {
                                            if (result.Critical3 == 0)
                                            {
                                                result.Result = 25;
                                            }
                                            else
                                            {
                                                result.Result = 50;
                                            }
                                        }
                                        else
                                        {
                                            result.Result = 100;
                                        }
                                    }
                                }
                                else
                                {
                                    if (result.Critical1 == 0 && result.Critical2 == 0 && result.Critical3 == 0)
                                    {
                                        result.Result = 0;
                                    }
                                    else
                                    {
                                        result.Result = 25;
                                    }
                                }
                            }
                            else
                            {
                                if (result.HygieneFactor == 1 && result.Contra1 == 0 && result.Contra2 == 0)
                                {
                                    if (result.Critical1 == 0 && result.Critical2 == 0 && result.Critical3 == 0)
                                    {
                                        result.Result = 25;
                                    }
                                    else
                                    {
                                        if (result.Critical1 > 0 || result.Critical2 > 0 || result.Critical3 > 0)
                                        {
                                            result.Result = 75;
                                        }
                                        else
                                        {
                                            result.Result = 100;
                                        }
                                    }
                                }
                                else
                                {
                                    if (result.Critical1 > 0 || result.Critical2 > 0 || result.Critical3 > 0)
                                    {
                                        result.Result = 25;
                                    }
                                    else
                                    {
                                        result.Result = 0;
                                    }
                                }
                            }

                            db.tblQuestionnaireFieldLogicResults.Add(result);
                            db.SaveChanges();
                        }

                    }

                    List<tblQuestionnaireFieldLogicResult> carrerlist = db.tblQuestionnaireFieldLogicResults.Where(x => x.UserName == UserName).OrderByDescending(x => x.Result).ToList();

                    ViewBag.CarrerList = carrerlist;

                    List<SubStrengthValue> list1 = new List<SubStrengthValue>();

                    SubStrengthValue Artistic1 = new SubStrengthValue();
                    Artistic1.Name = 1;
                    Artistic1.Value = temp.Artistic;
                    list1.Add(Artistic1);

                    SubStrengthValue Aesthetic1 = new SubStrengthValue();
                    Aesthetic1.Name = 2;
                    Aesthetic1.Value = temp.Aesthetic;
                    list1.Add(Aesthetic1);

                    SubStrengthValue Idea_Generator1 = new SubStrengthValue();
                    Idea_Generator1.Name = 3;
                    Idea_Generator1.Value = temp.Idea_Generator;
                    list1.Add(Idea_Generator1);

                    SubStrengthValue Team_Player1 = new SubStrengthValue();
                    Team_Player1.Name = 4;
                    Team_Player1.Value = temp.Team_Player;
                    list1.Add(Team_Player1);

                    SubStrengthValue Independent1 = new SubStrengthValue();
                    Independent1.Name = 5;
                    Independent1.Value = temp.Independent;
                    list1.Add(Independent1);

                    SubStrengthValue Mentor1 = new SubStrengthValue();
                    Mentor1.Name = 6;
                    Mentor1.Value = temp.Mentor;
                    list1.Add(Mentor1);

                    SubStrengthValue Analytics1 = new SubStrengthValue();
                    Analytics1.Name = 7;
                    Analytics1.Value = temp.Analytics;
                    list1.Add(Analytics1);

                    SubStrengthValue Social_Philosophical1 = new SubStrengthValue();
                    Social_Philosophical1.Name = 8;
                    Social_Philosophical1.Value = temp.Social_Philosophical;
                    list1.Add(Social_Philosophical1);

                    SubStrengthValue Practical1 = new SubStrengthValue();
                    Practical1.Name = 9;
                    Practical1.Value = temp.Practical;
                    list1.Add(Practical1);

                    list1 = list1.OrderByDescending(x => x.Value).ToList();

                    List<SubStrengthDescription> listdesc = new List<SubStrengthDescription>();
                    var t = list1.Take(3);
                    foreach (var temmp in t)
                    {
                        SubStrengthDescription sub = new SubStrengthDescription();
                        sub.Name = temmp.Name;
                        sub.Value = db.tblQuestionnaireSubStrengths.Where(x => x.Id == temmp.Name).Select(x => x.Description).FirstOrDefault();
                        listdesc.Add(sub);
                    }

                    ViewBag.Description = listdesc;
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "LoginQuestionnaire");
            }
        }

        public JsonResult GetColumnChartData()
        {
            string UserName = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

            tblQuestionnaireSubStrengthResult temp = db.tblQuestionnaireSubStrengthResults.Where(x => x.UserName == UserName).FirstOrDefault();

            List<SubStrengthValueChart> list = new List<SubStrengthValueChart>();

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

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Career(int id)
        {
            if (Request.IsAuthenticated)
            {
                string UserName = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

                tblQuestionnaireLogin login = db.tblQuestionnaireLogins.Where(x => x.UserName == UserName).FirstOrDefault();
                if (login.IsAdmin == "1")
                {
                    return RedirectToAction("Career", "AdminQuestionnaire");
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



    }
}