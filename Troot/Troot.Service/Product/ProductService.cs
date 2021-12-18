using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Troot.DB.Entities;
using Troot.DB.Entities.DataContext;
using Troot.Model;
using Troot.Model.Product;

namespace Troot.Service.Product
{
    public class ProductService : IProductService
    {
        //automapper kullanımı
        private readonly IMapper mapper;
        public ProductService(IMapper _mapper)
        {
            mapper = _mapper;
        }

        //Ürünleri listeleme
        public General<ProductViewModel> GetProducts()
        {
            var product = new General<ProductViewModel>();
            using (var context = new TrootContext())
            {
                var data = context.Product
                    .Where(x => x.IsActive && !x.IsDeleted) //Silinmemiş ve aktifleri
                    .OrderBy(x => x.Id);                    //Idye göre getir.

                if (data.Any())
                {
                    product.List = mapper.Map<List<ProductViewModel>>(data);
                    product.IsSuccess = true;
                }
                else
                {
                    product.ExceptionMessage = "Ürün bulunamadı.";
                }
            }
            return product;
        }

        //Ürünleri verilen idye göre getiren metot
        public General<ProductViewModel> GetProductListById(int id, ProductViewModel product)
        {
            var result = new General<ProductViewModel>();
            using (var context = new TrootContext())
            {
                var data = context.User
                    .Where(x => x.IsActive && !x.IsDeleted && x.Iuser == id) //Getteki gibi silinmemiş ve aktif olanlar + insert user'ı idye eşit olanları, id'ye göre getir demek.
                    .OrderBy(x => x.Id);

                if (data.Any())
                {
                    result.List = mapper.Map<List<ProductViewModel>>(data);
                    result.IsSuccess = true;
                    result.Message = "İşlem başarılı.";
                }
                else
                {
                    result.ExceptionMessage = "Ürün bulunamadı.";
                }
            }
            return result;
        }

        //Ürün ekleme
        public General<ProductViewModel> InsertProduct(ProductViewModel product)
        {
            var data = new General<ProductViewModel>();
            var insertProduct = mapper.Map<Troot.DB.Entities.Product>(product);

            using (var context = new TrootContext())
            {
                insertProduct.Idate = DateTime.Now;
                insertProduct.IsActive = true;
                context.Product.Add(insertProduct);
                context.SaveChanges();

                data.Entity = mapper.Map<ProductViewModel>(insertProduct);
                data.IsSuccess = true;
            }
            return data;
        }

        //Ürün güncelleme
        public General<ProductViewModel> UpdateProduct(int id, ProductViewModel product)
        {
            var data = new General<ProductViewModel>();
            using (var context = new TrootContext())
            {
                var updatedProduct = context.Product.SingleOrDefault(x => x.Id == id);
                if (updatedProduct is not null)
                {
                    updatedProduct.Name = product.Name;
                    updatedProduct.DisplayName = product.DisplayName;
                    updatedProduct.Description = product.Description;
                    updatedProduct.Price = product.Price;
                    updatedProduct.Stock = product.Stock;

                    context.SaveChanges();
                    data.Entity = mapper.Map<ProductViewModel>(updatedProduct);
                    data.IsSuccess = true;
                    data.Message = "Ürün başarıyla güncellendi.";
                }
                else
                {
                    data.ExceptionMessage = "Bir hata oluştu. Lütfen tekrar deneyiniz.";
                }
            }
            return data;
        }

        //Ürün silme ==> Veriler aslında db'de kalmaya devam ediyor, tamamen silinmiyor. Sadece IsActive=false, IsDeleted=true oluyor.
        public General<ProductViewModel> DeleteProduct(int id, ProductViewModel product)
        {
            var result = new General<ProductViewModel>();
            using (var context = new TrootContext())
            {
                var productActivity = context.Product.SingleOrDefault(x => x.Id == id);
                if (productActivity is not null)
                {
                    productActivity.IsActive = false;
                    productActivity.IsDeleted = true;

                    context.SaveChanges();
                    result.Entity = mapper.Map<ProductViewModel>(productActivity);
                    result.IsSuccess = true;
                    result.Message = "Ürün başarıyla silindi.";
                }
                else
                {
                    result.ExceptionMessage = "Hata. Lütfen tekrar deneyiniz.";
                }
            }
            return result;
        }
    }
}
