using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troot.Model.User
{
    public class UserViewModel
    {
        //Genel olarak kullanıcılar üzerinde yapacağımız işlemlerde kullanacağımız User View Model:
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
