using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductDetails
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string Fee { get; set; }
        public string Installment1 { get; set; }
        public string DueDate1 { get; set; }
        public string Installment2 { get; set; }
        public string DueDate2 { get; set; }
        public string Installment3 { get; set; }
        public string DueDate3 { get; set; }
        public string Installment4 { get; set; }
        public string DueDate4 { get; set; }
        public string Link { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDateTime { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public string Attribute4 { get; set; }
        public string Attribute5 { get; set; }
        public string ProductDtls { get; set; }
    }
}
