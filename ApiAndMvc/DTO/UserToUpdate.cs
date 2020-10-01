using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheApi.DTO
{
    public class UserToUpdate
    {
      
        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

      
        [EmailAddress]
        public string Email { get; set; }
    }
}
