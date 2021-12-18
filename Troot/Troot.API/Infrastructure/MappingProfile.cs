using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Troot.DB.Entities;
using Troot.Model.Category;
using Troot.Model.Product;
using Troot.Model.User;

namespace Troot.API.Infrastructure
{
    public class MappingProfile:Profile
    {
        //Mappingde yapacağımız işlemleri Api tarafında burada tanımlıyoruz
        public MappingProfile()
        {
            CreateMap<UserViewModel, User>();
            CreateMap<User, UserViewModel>();

            CreateMap<LoginViewModel, User>();
            CreateMap<User, LoginViewModel>();

            CreateMap<ProductViewModel, Product>();
            CreateMap<Product, ProductViewModel>();

            CreateMap<CategoryViewModel, Category>();
            CreateMap<Category, CategoryViewModel>();


        }
    }
}
