using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Inomi.Models.Student
{
    public class Dashboard
    {


        public Count Counts;
        public Milestone Milestone;
        public List<Project> Project;
        public List<ranklist> ranklist; 
        public List<Collegeshortlist> Collegeshortlist;
        public EditAcademics EditAcademics;
        public List<Academics> Academics;
        public List<TestReport> TestReport;
        public List<ExtraCurricularActivites> ExtraCurricularActivites;
        public List<LeadershipPositions> LeadershipPositions;

        public EditExtraCurricularActivites EditExtraCurricularActivites;
        public EditLeadershipPositions EditLeadershipPositions;
        public Counsellor Counsellor;
        public List<UpcomingDeadLineReport> UpcomingDeadLineReport;
        public List<DeadLineEssay> DeadLineEssay;
        public List<DeadLineEssayview> DeadLineEssayview;
        public List<Studentmessage> Studentmessage;
        public List<Interestinglinksview> Interestinglinksview;
        public List<CallNote> CallNote;
        public List<BragsheetDetail> BragsheetDetail;
        public List<LORDetail> LORDetail;
        public EditTestReport EditTestReport;



        public Dashboard()
        {
            EditExtraCurricularActivites = new EditExtraCurricularActivites();
            EditLeadershipPositions = new EditLeadershipPositions();
            EditAcademics = new EditAcademics();
            Counts = new Count();
            EditTestReport = new EditTestReport();
            Milestone = new Milestone();
            ranklist = new List<ranklist>();
            BragsheetDetail = new List<BragsheetDetail>();
            Collegeshortlist = new List<Collegeshortlist>();
            LORDetail = new List<LORDetail>();
            CallNote = new List<CallNote>();
            Project = new List<Project>();
            Academics = new List<Academics>();
            TestReport = new List<TestReport>();
            ExtraCurricularActivites = new List<ExtraCurricularActivites>();
            LeadershipPositions = new List<LeadershipPositions>();
            Counsellor = new Counsellor();
            UpcomingDeadLineReport = new List<UpcomingDeadLineReport>();
            DeadLineEssay = new List<DeadLineEssay>();
            DeadLineEssayview = new List<DeadLineEssayview>();
            Studentmessage = new List<Studentmessage>();
            Interestinglinksview = new List<Interestinglinksview>();
        }

    }
    public class Count
    {
        public string UpcomingDeadlineCount { get; set; }
        public string DeadlineEssayCount { get; set; }
        public string MessageCount { get; set; }


    }

    public class Project
    {

        public int Projectid { get; set; }
        public string ID { get; set; }
        public string Count { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Milestone> Milestone;
        public Project()
        {
            Milestone = new List<Milestone>();
        }

    }
    public class Milestone
    {
        public int ID { get; set; }

        public string MilestoneName { get; set; }
        public DateTime? Milestonedate { get; set; }

        public string Description { get; set; }
        public Boolean? Status { get; set; }

    }
    public class EditAcademics
    {
        public string Id { get; set; }
        public int? StudentID { get; set; }
        public string Class { get; set; }
        public string Year { get; set; }
        public string Board { get; set; }
        public string School { get; set; }
        public string Transcript { get; set; }
        public string OverAllGPAMark { get; set; }
        public string Subject { get; set; }



    }
    public class Academics
    {
        public string Id { get; set; }
        public int? StudentID { get; set; }
        public string Class { get; set; }
        public string Year { get; set; }
        public string Board { get; set; }
        public string School { get; set; }
        public string Transcript { get; set; }
        public string OverAllGPAMark { get; set; }
        public string Subject { get; set; }



    }
    public class TestReport
    {
        public string TestName { get; set; }
        public string Score { get; set; }
        public string id { get; set; }



    }
    public class EditTestReport
    {
        public string TestName { get; set; }
        public string Score { get; set; }
        public int id { get; set; }



    }
    public class ExtraCurricularActivites
    {
        public string Activities { get; set; }
        public string Achievements { get; set; }
        public string Details { get; set; }
        public string id { get; set; }



    }
    public class EditExtraCurricularActivites
    {
        public string Activities { get; set; }
        public string Achievements { get; set; }
        public string Details { get; set; }
        public int id { get; set; }



    }
    public class LeadershipPositions
    {
        public string Position { get; set; }
        public int id { get; set; }

    }

    public class EditLeadershipPositions
    {
        public string Position { get; set; }
        public int id { get; set; }

    }

    public class Counsellor
    {
        public string Name { get; set; }
        public string Experience { get; set; }
        public string Education { get; set; }
        public string Picture { get; set; }



    }
    public class UpcomingDeadLineReport
    {
        public string University { get; set; }
        public string Campus { get; set; }
        public string EarlyDeadLine { get; set; }
        public string LateDeadLine { get; set; }
        public string FinalDeadLine { get; set; }
    }

    public class DeadLineEssay
    {
        public int ID { get; set; }

        public string NameOfCollege { get; set; }
        public string EssayName { get; set; }
        public string WordCount { get; set; }
        public string deadline { get; set; }
        public string LatestFile { get; set; }
        public string Status { get; set; }

    }

    public class DeadLineEssayview
    {
        public int ID { get; set; }
        public string LatestFile { get; set; }
        public DateTime? CreatedDateTime { get; set; }
    }

    public class UploadEssay
    {

        public string UploadId { get; set; }
        public HttpPostedFileBase Fichier1 { get; set; }

    }

    public class TestReportDetails
    {
        [Required]
        public string TestName { get; set; }
        [Required]
        public string Score { get; set; }
    }

    public class LeadershipPositionsDetails
    {
        [Required]
        public string Position { get; set; }
    }

    public class Academicdetails
    {

        [Required]
        public string Class { get; set; }
        [Required]
        public string Subject { get; set; }

        [Required]
        public string Year { get; set; }
        [Required]
        public string Board { get; set; }
        [Required]
        public string School { get; set; }

        public string Transcript { get; set; }
        [Required]
        public string OverAllGPAMark { get; set; }
        [Required]
        public HttpPostedFileBase Fichier1 { get; set; }



    }


    public class EditAcademicdetails
    {

        [Required]
        public string Class { get; set; }
        [Required]
        public string Subject { get; set; }

        [Required]
        public string Year { get; set; }
        [Required]
        public string Board { get; set; }
        [Required]
        public string School { get; set; }

        public string Transcript { get; set; }
        [Required]
        public string OverAllGPAMark { get; set; }
        [Required]
        public HttpPostedFileBase Fichier1 { get; set; }
        [Required]
        public int Id { get; set; }



    }


    public class ExtraCurricularActivitesDetails
    {
        [Required]
        public string Activities { get; set; }
        [Required]
        public string Achievements { get; set; }
        [Required]
        public string Details { get; set; }
    }

    public class Studentmessage
    {

        public string Message { get; set; }


    }

    public class Interestinglinksdetails
    {

        [Required]
        public string Title { get; set; }
        [Required]
        [AllowHtml]
        public string ckeExample { get; set; }





    }
    public class Interestinglinksview
    {


        public string Title { get; set; }

        public string description { get; set; }
        public DateTime? Createddate { get; set; }





    }

    public class CallNote
    {

        public int id { get; set; }
        public int STD { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Academics { get; set; }
        public string Testing { get; set; }
        public string College { get; set; }
        public string Project1 { get; set; }
        public string Project2 { get; set; }
        public string ExtraCurricular { get; set; }
        public string Misc { get; set; }
        public string Suggestedcourse { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }

    }


    public class CallNoteDetail
    {

        public string Date { get; set; }

        public string Academics { get; set; }

        public string Testing { get; set; }

        public string College { get; set; }

        public string Project1 { get; set; }

        public string Project2 { get; set; }

        public string ExtraCurricular { get; set; }

        public string Misc { get; set; }

        public string Suggestedcourse { get; set; }


    }

    public class LORDetail
    {

        public string LoRs { get; set; }
        public string LoRspath { get; set; }
        public string LoRsSize { get; set; }
    }
    public class BragsheetDetail
    {
        public string Bragsheet { get; set; }

        public string BragsheetSize { get; set; }
        public string BragsheetPath { get; set; }




    }

    public class DropDownModel
    {
        public string Id { get; set; }

        public string Value { get; set; }
    }

    public class Collegeshortlist
    {
        public int? Id { get; set; }

        public string NameOfCollege { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int? sequenceid { get; set; }
        public int? counsellorid { get; set; }
        public string counsellorstatus { get; set; }
        public int? adminid { get; set; }
        public string adminstatus { get; set; }
    }

    public class milestoneupload
    {
        public HttpPostedFileBase[] salaryslip { get; set; }
        public int MID { get; set; }
        public int? STD { get; set; }
        public string Upload { get; set; }



    }

    public class ranklist
    {
        public string Data { get; set; }
    }

}