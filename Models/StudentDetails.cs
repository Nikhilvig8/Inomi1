﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StudentDetails
    {
        public int ID { get; set; }
        public string StudentCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Grade { get; set; }
        public string School { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Product { get; set; }
        public string Picture { get; set; }
        public string Parent1Name { get; set; }
        public string Parent1Occupation { get; set; }
        public string Parent1Phone { get; set; }
        public string Parent1Email { get; set; }
        public string Parent2Name { get; set; }
        public string Parent2Occupation { get; set; }
        public string Parent2Phone { get; set; }
        public string Parent2Email { get; set; }
        public string CareerApplying { get; set; }
        public string CountryApplying { get; set; }
        public string ApplicationYear { get; set; }
        public string FinacialAidNeeded { get; set; }
        public string Counsellor { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDateTime { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public string Attribute4 { get; set; }
        public string Attribute5 { get; set; }
        public string InstallmentCount{ get; set; }
        public string InsAmt { get; set; }
        public string InsDate { get; set; }
        public string IsConcession { get; set; }
        public string ConcessionAmount { get; set; }
        public string InsertStatus { get; set; }
    }
}
