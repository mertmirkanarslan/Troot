using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Troot.DB.Entities.DataContext;
using Troot.Model;
using Troot.Model.User;

namespace Troot.Service.User
{
    //Interface'imizden kalıtım alıp implemente ediyoruz.
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        public UserService(IMapper _mapper)
        {
            mapper = _mapper;
        }

        //Kullanıcıları getirme işlemi
        public General<UserViewModel> GetUsers()
        {
            var result = new General<UserViewModel>();
            using (var context = new TrootContext())
            {
                var data = context.User
                    .Where(x => x.IsActive && !x.IsDeleted)
                    .OrderBy(x => x.Id);

                if (data.Any())
                {
                    result.List = mapper.Map<List<UserViewModel>>(data);
                    result.IsSuccess = true;
                    result.Message = "Kullanıcılar listelendi.";
                }
                else
                {
                    result.ExceptionMessage = "Sisteme kayıtlı kullanıcı bulunamadı.";
                }
            }
            return result;
        }

        //Kullanıcı ekleme
        public General<UserViewModel> InsertUser(UserViewModel newUser)
        {
            var result = new General<UserViewModel>();
            var insertUser = mapper.Map<Troot.DB.Entities.User>(newUser);
            using (var context = new TrootContext())
            {
                insertUser.Idatetime = DateTime.Now;
                insertUser.IsActive = true;
                context.User.Add(insertUser);
                context.SaveChanges();

                result.Entity = mapper.Map<UserViewModel>(insertUser);
                result.IsSuccess = true;
                result.Message = "Yeni kullanıcı eklendi.";
            }
            return result;
        }

        //Kullanıcı giriş (login)
        public General<LoginViewModel> Login(LoginViewModel user)
        {
            var result = new General<LoginViewModel>();
            var loginUser = mapper.Map<Troot.DB.Entities.User>(user);

            using (var context = new TrootContext())
            {
                //izin kontrolü yapıyoruz
                var permission = context.User.Any(x=> x.IsActive && !x.IsDeleted && 
                                         x.UserName == user.UserName &&
                                         x.Password == user.Password);

                var data = context.User.FirstOrDefault(x => !x.IsDeleted &&
                                                x.IsActive &&
                                                x.UserName == user.UserName &&
                                                x.Password == user.Password);
                if (permission)
                {
                    result.Entity = mapper.Map<LoginViewModel>(data);
                    result.IsSuccess = true;
                    result.Message = "Giriş başarılı.";
                }
                else
                {
                    result.ExceptionMessage = "Bir hata oluştu. Lütfen tekrar deneyiniz.";
                }

            }
            return result;
        }

        //Kullanıcı güncelleme
        public General<UserViewModel> UpdateUser(int id, UserViewModel user)
        {
            var result = new General<UserViewModel>();
            using (var context = new TrootContext())
            {
                var updatedUser = context.User.SingleOrDefault(x => x.Id == id);
                if (updatedUser is not null)
                {
                    updatedUser.Name = user.Name;
                    updatedUser.UserName = user.UserName;
                    updatedUser.Email = user.Email;
                    updatedUser.Password = user.Password;

                    context.SaveChanges();
                    result.Entity = mapper.Map<UserViewModel>(updatedUser);
                    result.IsSuccess = true;
                    result.Message = "İşlem başarılı";
                }
                else
                {
                    result.ExceptionMessage = "Bir hata oluştu.";
                }
            }
            return result;
        }

        //Kullanıcı silme
        public General<UserViewModel> DeleteUser(int id, UserViewModel user)
        {
            var result = new General<UserViewModel>();
            using (var context = new TrootContext())
            {
                var userActivity = context.User.SingleOrDefault(x => x.Id == id);
                if (userActivity is not null)
                {
                    userActivity.IsActive = false;
                    userActivity.IsDeleted = true;

                    context.SaveChanges();
                    result.Entity = mapper.Map<UserViewModel>(userActivity);
                    result.IsSuccess = true;
                    result.Message = "Kullanıcı başarıyla silindi.";
                }
                else
                {
                    result.ExceptionMessage = "İşlem yapmak istediğiniz kullanıcı bulunamadı. Lütfen tekrar deneyiniz.";
                }
            }
            return result;
        }
    }
}
