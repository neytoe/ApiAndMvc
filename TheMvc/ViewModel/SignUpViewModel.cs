using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheMvc.ViewModel
{
    public class SignUpViewModel
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Max characters allowed is 30")]
        public string LastName { get; set; }


        [Required]
        [MaxLength(30, ErrorMessage = "Max characters allowed is 30")]
        public string FirstName { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [MaxLength(30, ErrorMessage = "Max characters allowed is 30")]
        public string Gender { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match!")]
        public string ConfirmPassword { get; set; }
    }
}
