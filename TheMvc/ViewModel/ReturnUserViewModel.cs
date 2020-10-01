using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheMvc.ViewModel
{
    public class ReturnUserViewModel
    {
        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Gender { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
