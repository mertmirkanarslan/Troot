using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Troot.Model;
using Troot.Model.Product;

namespace Troot.Service.Product
{
    public interface IProductService
    {
        //ProductService in arayüzü:
        public General<ProductViewModel> GetProducts();
        public General<ProductViewModel> InsertProduct(ProductViewModel product);
        public General<ProductViewModel> UpdateProduct(int id, ProductViewModel product);
        public General<ProductViewModel> GetProductListById(int id, ProductViewModel product);
        public General<ProductViewModel> DeleteProduct(int id, ProductViewModel product);
        //Sorting için eklendi
        public General<ListProductViewModel> SortProduct(string sortingParameter);
        //Filtering için eklendi
        public General<ListProductViewModel> FilterProduct(string param);
    }
}
