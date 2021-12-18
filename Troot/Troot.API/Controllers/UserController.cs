using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Troot.Model;
using Troot.Model.User;
using Troot.Service.User;

namespace Troot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        //Kullanıcı girişi(login)
        [HttpPost("Login")]
        public General<LoginViewModel> Login([FromBody] LoginViewModel user)
        {
            return userService.Login(user);
        }

        //Kullanıcı listeleme
        [HttpGet]
        public General<UserViewModel> GetUsers()
        {
            return userService.GetUsers();
        }

        //Kullanıcı ekleme
        [HttpPost("Insert")]
        public General<UserViewModel> InsertUser([FromBody] UserViewModel newUser)
        {
            return userService.InsertUser(newUser);
        }

        //Kullanıcı güncelleme (verilen id'ye göre)
        [HttpPut("{id}")]
        public General<UserViewModel> UpdateUser(int id, [FromBody] UserViewModel user)
        {
            return userService.UpdateUser(id, user);
        }

        //Kullanıcı silme
        [HttpPost("Delete")]
        public General<UserViewModel> DeleteUser(int id, [FromBody] UserViewModel user)
        {
            return userService.DeleteUser(id, user);
        }

    }
}
