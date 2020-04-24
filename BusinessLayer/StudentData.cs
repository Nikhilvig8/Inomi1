using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DataLayer;

namespace BusinessLayer
{
    public class StudentData
    {
        public static List<StudentDetails> GetStudentList()
        {
            List<StudentDetails> list = new List<StudentDetails>();
            using (InomiEntities objInomiEntities = new InomiEntities())
            {

                list = (from Stu in objInomiEntities.tblStudents

                        select new Models.StudentDetails
                        {
                            FirstName = Stu.FirstName,
                            Grade = Stu.Grade,
                            School = Stu.School,
                            Phone = Stu.Phone,
                            Email = Stu.Email,
                            Gender = Stu.Gender,
                            Product = Stu.Product,
                            Picture = Stu.Picture,
                            Parent1Name = Stu.Parent1Name,
                            Parent1Occupation = Stu.Parent1Occupation,
                            Parent1Phone = Stu.Parent1Phone,
                            Parent1Email = Stu.Parent1Email,
                            Parent2Name = Stu.Parent2Name,
                            Parent2Occupation = Stu.Parent2Occupation,
                            Parent2Phone = Stu.Parent2Phone,
                            Parent2Email = Stu.Parent2Email,
                            CareerApplying = Stu.CareerApplying,
                            CountryApplying = Stu.CountryApplying,
                            ApplicationYear = Stu.ApplicationYear,
                            FinacialAidNeeded = Stu.FinacialAidNeeded,
                            Counsellor = Stu.Counsellor

                        }).ToList();
            }

            return list;

        }
    }
}
