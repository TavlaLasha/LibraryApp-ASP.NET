using LibraryModels.DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBLL.Contracts
{
    public interface IProductManagement
    {
        IEnumerable<ProductDTO> GetAllProducts(string isArchived);
        IEnumerable<ProductDTO> GetAllProducts();
        IEnumerable<ProductDTO> GetAuthorProducts(string author_pn);
        ProductDTO GetProduct(string pn);
        bool AddProduct(ProductDTO product, string author_pn);
        bool ChangeArchiveStatus(string isbn);
        IEnumerable<TypeDTO> GetAllTypes();
        IEnumerable<ProductionDTO> GetAllProductions();
    }
}
