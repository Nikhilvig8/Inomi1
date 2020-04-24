using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inomi.Models.Questionnaire
{
    public class SubStrengthValue
    {
        public int? Name { get; set; }
        public int Value { get; set; }
    }

    public class SubStrengthDescription
    {
        public int? Name { get; set; }
        public string Value { get; set; }
    }

    public class SubStrengthValueChart
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class Login
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }

    public class Register
    {
        [Required(ErrorMessage = "Name Should Contains Text Only.")]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Age Should be less than 50.")]
        [RegularExpression(@"([1-9]|[1-4][0-9]|50)$")]
        public Nullable<int> Age { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Mobile No Should be 10 digit number.")]
        [RegularExpression(@"^([0-9]{10})$")]
        public string Mobile { get; set; }
        [Required]
        public string Address { get; set; }
    }
}