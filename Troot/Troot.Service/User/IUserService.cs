using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Troot.Model;
using Troot.Model.User;

namespace Troot.Service.User
{
    public interface IUserService
    {
        //Interface olmadan dependency injection'da sorun yaşanıyor. Bu nedenle UserService'te kullanacağımız metotları burada tanımlıyoruz.
        public General<LoginViewModel> Login(LoginViewModel user);
        public General<UserViewModel> GetUsers();
        public General<UserViewModel> InsertUser(UserViewModel newUser);
        public General<UserViewModel> UpdateUser(int id, UserViewModel user);
        public General<UserViewModel> DeleteUser(int id, UserViewModel user);
    }
}
