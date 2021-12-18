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
                    product.Message = "Ürünler listelendi.";
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
                //Permission tanımını burada da yapıp ürün eklemeyi yetkiye bağlarsak:
                //id ile Iuser(insert user) karşılaştırması yapıp klasik olan aktif ve silinmemiş olma şartını da eklersek
                var permission = context.User.Any(x => x.Id == insertProduct.Iuser && x.IsActive && !x.IsDeleted);
                if (permission)
                {
                    insertProduct.Idate = DateTime.Now;
                    insertProduct.IsActive = true;
                    context.Product.Add(insertProduct);
                    context.SaveChanges();

                    data.Entity = mapper.Map<ProductViewModel>(insertProduct);
                    data.IsSuccess = true;
                    data.Message = "Ürün başarıyla eklendi.";
                }
                else data.ExceptionMessage = "Ürün eklemek için yetkili değilsiniz.";
                
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
                var permission = context.Product.Any(p => p.Iuser == product.IUser);
                if (permission)
                {
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
                        data.ExceptionMessage = "Aradığınız ürün bulunamadı. Lütfen tekrar deneyiniz.";
                    }
                }
                else
                {
                    data.ExceptionMessage = "Bu işlemi gerçekleştirmek için yetkiniz yok.";
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
                var permission = context.Product.Any(x => x.Iuser == product.IUser);
                if (permission)
                {
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
                        result.ExceptionMessage = "Silinmek istenen ürün bulunamadı. Lütfen tekrar deneyiniz.";
                    }
                }
                else
                {
                    result.ExceptionMessage = "Bu işlemi yapmak için yetkiniz bulunmamaktadır. Lütfen tekrar deneyiniz.";
                }
            }
            return result;
        }

        //Ürün sıralama (sorting)
        public General<ListProductViewModel> SortProduct(string param)
        {
            var result = new General<ListProductViewModel>();
            using (var context = new TrootContext())
            {
                var products = context.Product.Where(x => x.IsActive && !x.IsDeleted && x.Id > 0); //idsi olan, aktif ve silinmemiş ürünler
                if (param.Equals("NameASC")) //isme göre artan
                {
                    products = products.OrderBy(x => x.Name);
                }
                else if (param.Equals("NameDESC")) //isme göre azalan
                {
                    products = products.OrderByDescending(x => x.Name);
                }
                else if (param.Equals("PriceASC")) //fiyata göre artan
                {
                    products = products.OrderBy(x => x.Price);
                }
                else if (param.Equals("PriceDESC")) //fiyata göre azalan
                {
                    products = products.OrderByDescending(x => x.Price);
                }
                else
                {
                    result.ExceptionMessage = "Bir hata oluştu";
                    return result;
                }
                result.IsSuccess = true;
                result.List = mapper.Map<List<ListProductViewModel>>(products);
                result.Message = "Sıralama başarıyla yapıldı.";
            }
            return result;
        }

        //Ürün filtreleme (filtering)
        public General<ListProductViewModel> FilterProduct(string param)
        {
            var result = new General<ListProductViewModel>();
            using (var context = new TrootContext())
            {
                var product = context.Product.Where(x => x.IsActive && !x.IsDeleted && x.Id > 0);
                product = product.Where(x => x.Name.StartsWith(param));
                if (product.Any())
                {
                    result.IsSuccess = true;
                    result.List = mapper.Map<List<ListProductViewModel>>(product);
                    result.Message = "Filtreleme başarıyla yapıldı.";
                }
                else
                {
                    result.ExceptionMessage = "Bir hata oluştu. Lütfen tekrar deneyiniz.";
                    return result;
                }
            }
            return result;
        }

        //Pagination (Sayfalama). 
        // Sayfalama işlemi. Eğer istenilen sayfa sayısı ve sayfadaki ürün sayısı özellikleri toplam ürün sayısına
        // uygun değilse hataya özel hata mesajları döner. X. sayfa Y adet ürün şeklinde çalışır.
        public General<ListProductViewModel> PaginateProduct(int productByPage, int pageNumber)
        {
            var result = new General<ListProductViewModel>();
            decimal pageCount = 0;
            decimal productCount = 0;

            using (var context = new TrootContext())
            {
                result.ProductCount = context.Product.Count();
                productCount = result.ProductCount;
                pageCount = Math.Ceiling(productCount / productByPage);
                var product = context.Product.OrderBy(x => x.Id).Skip((int)((pageNumber - 1) * productByPage)).Take((int)productByPage).ToList();

                if (productByPage <= 0 || pageNumber <= 0)
                {
                    result.ExceptionMessage = "Değerler 0'dan küçük olamaz. Hatalı işlem.";
                }
                else if (productByPage > productCount)
                {
                    result.ExceptionMessage = "Ürün sayfa sayısı ürün sayısından büyük olamaz. Hatalı işlem.";
                }
                else if (pageNumber > productCount)
                {
                    result.ExceptionMessage = "Bu kadar sayfa bulunmamaktadır. Hatalı işlem.";
                }
                else
                {
                    result.List = mapper.Map<List<ListProductViewModel>>(product);
                    result.IsSuccess = true;
                    result.Message = "Sayfalama başarıyla yapıldı.";
                }
            }
            result.PageCount = pageCount;
            return result;
        }

    }
}
