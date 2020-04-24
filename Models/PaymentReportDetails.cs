using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PaymentReportDetails
    {
        public string Student { get; set; }
        public string Product { get; set; }
        public string Counsellor { get; set; }
        public string IntallmentNo { get; set; }
        public string Amount { get; set; }
        public string CreatedDateTime { get; set; }
    }
}
