using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Troot.Model;
using Troot.Model.Category;

namespace Troot.Service.Category
{
    public interface ICategoryService
    {
        //Category Service'in arayüzü:
        public General<CategoryViewModel> GetCategories();
        public General<CategoryViewModel> InsertCategory(CategoryViewModel category);
        public General<CategoryViewModel> UpdateCategory(int id, CategoryViewModel category);
        public General<CategoryViewModel> DeleteCategory(int id, CategoryViewModel category);
    }
}
