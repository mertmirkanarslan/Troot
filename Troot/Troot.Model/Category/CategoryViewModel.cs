using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troot.Model.Category
{
    public class CategoryViewModel
    {
        //İşlemlerimizi dbden ne kadar uzaklaştırırsak o kadar iyi. Bu nedenle model katmanında db operasyonlarımızı yapacağımız işlemleri düzenli şekilde proplar halinde yazıyoruz.
        //Kategori View Modelimiz
        public int Id { get; set; }
        public int IUser { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
