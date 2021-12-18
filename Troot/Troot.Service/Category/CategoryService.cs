using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Troot.DB.Entities.DataContext;
using Troot.Model;
using Troot.Model.Category;

namespace Troot.Service.Category
{
    public class CategoryService : ICategoryService
    {
        //Automapper injection
        private readonly IMapper mapper;
        public CategoryService(IMapper _mapper)
        {
            mapper = _mapper;
        }
        //aşağıda da crud işlemlerimiz olacak

        //Kategorileri Listeleme
        public General<CategoryViewModel> GetCategories()
        {
            var category = new General<CategoryViewModel>();
            using (var context = new TrootContext())
            {
                var data = context.Category
                    .Where(x => x.IsActive && !x.IsDeleted)
                    .OrderBy(x => x.Id);

                if (data.Any())
                {
                    category.List = mapper.Map<List<CategoryViewModel>>(data);
                    category.IsSuccess = true;
                    category.Message = "Kategoriler listelendi.";
                }
                else
                {
                    category.ExceptionMessage = "Bir hata oluştu.";
                }
            }
            return category;
        }

        //Kategori Ekleme
        public General<CategoryViewModel> InsertCategory(CategoryViewModel category)
        {
            var result = new General<CategoryViewModel>();
            var insertCategory = mapper.Map<Troot.DB.Entities.Category>(category);
            using (var context = new TrootContext())
            {
                insertCategory.Idate = DateTime.Now;
                insertCategory.IsActive = true;
                context.Category.Add(insertCategory);
                context.SaveChanges();

                result.Entity = mapper.Map<CategoryViewModel>(insertCategory);
                result.IsSuccess = true;
                result.Message = "Kategori eklendi.";
            }
            return result;
        }

        //Kategori Güncelleme
        public General<CategoryViewModel> UpdateCategory(int id, CategoryViewModel category)
        {
            var data = new General<CategoryViewModel>();
            using (var context = new TrootContext())
            {
                var updatedCategory = context.Category.FirstOrDefault(x => x.Id == id);
                if (updatedCategory is not null)
                {
                    updatedCategory.Name = category.Name;
                    updatedCategory.DisplayName = category.DisplayName;

                    context.SaveChanges();
                    //Değişiklikler kaydedildi. aşağıda da mapleme yapıldı.
                    data.Entity = mapper.Map<CategoryViewModel>(updatedCategory);
                    data.IsSuccess = true;
                    data.Message = "Kategori güncelleme başarılı";
                }
                else
                {
                    data.ExceptionMessage = "Aradığınız kategori bulunamadı. Lütfen tekrar deneyiniz.";
                }
            }
            return data;
        }

        //Kategori Silme => kategori db'den tamamen silinmez, sadece aktifliği ve isdeleted değeri değiştirilir:
        public General<CategoryViewModel> DeleteCategory(int id, CategoryViewModel category)
        {
            var result = new General<CategoryViewModel>();
            using (var context = new TrootContext())
            {
                var categoryActivity = context.Category.SingleOrDefault(x => x.Id == id);
                if (categoryActivity is not null)
                {
                    categoryActivity.IsActive = false;
                    categoryActivity.IsDeleted = true;
                    context.SaveChanges();

                    result.Entity = mapper.Map<CategoryViewModel>(categoryActivity);
                    result.IsSuccess = true;
                    result.Message = "Kategori başarıyla silindi.";
                }
                else
                {
                    result.ExceptionMessage = "Bir hata oluştu. Lütfen tekrar deneyiniz.";
                }
            }
            return result;
        }
    }
}
