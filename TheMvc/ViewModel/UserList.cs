using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheApi.DTO;

namespace TheMvc.ViewModel
{
    public class UserList
    {

        public UserToReturnDto User { get; set; }
        public IEnumerable<UserToReturnDto> AllUsers { get; set; }
    }
}
