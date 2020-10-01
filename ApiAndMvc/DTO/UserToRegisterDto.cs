using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheApi.DTO
{
    public class UserToRegisterDto
    {
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
     
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Max characters allowed is 30")]
        public string Gender { get; set; }

        [Required]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string Password { get; set; }


        [Required]
        [MaxLength(20)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassWord { get; set; }

    }
}
