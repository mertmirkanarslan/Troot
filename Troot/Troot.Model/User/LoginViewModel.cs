using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troot.Model.User
{
    public class LoginViewModel
    {
        //Kullanıcı girişi işlemlerini yapacağımız Login View Model
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
