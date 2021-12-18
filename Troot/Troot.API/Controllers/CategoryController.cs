using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Troot.Model;
using Troot.Model.Category;
using Troot.Service.Category;

namespace Troot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        //Kategori listeleme
        [HttpGet]
        public General<CategoryViewModel> GetCategories()
        {
            return categoryService.GetCategories();
        }

        //Kategori ekleme
        [HttpPost("Insert")]
        public General<CategoryViewModel> InsertProduct([FromBody] CategoryViewModel newCategory)
        {
            return categoryService.InsertCategory(newCategory);
        }

        //Kategori güncelleme
        [HttpPut("{id}")]
        public General<CategoryViewModel> UpdateProduct(int id, [FromBody] CategoryViewModel category)
        {
            return categoryService.UpdateCategory(id, category);
        }

        //Kategori silme
        [HttpPost("Delete")]
        public General<CategoryViewModel> DeleteCategory(int id, [FromBody] CategoryViewModel category)
        {
            return categoryService.DeleteCategory(id, category);
        }

    }
}
