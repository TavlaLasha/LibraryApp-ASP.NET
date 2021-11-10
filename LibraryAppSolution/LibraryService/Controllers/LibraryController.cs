using LibraryBLL.Services;
using LibraryModels.DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LibraryService.Controllers
{
    public class LibraryController : ApiController
    {
        AuthorManagemet author = new AuthorManagemet();
        ProductManagement product = new ProductManagement();
        LocationManagement location = new LocationManagement();
    
        /* Author */

        [Route("api/Library/GetAllAuthors")]
        [HttpGet]
        public IEnumerable<AuthorDTO> GetAllAuthors() => author.GetAllAuthors();

        [Route("api/Library/GetAuthor/{pn}")]
        [HttpGet]
        public AuthorDTO GetAuthor(string pn) => author.GetAuthor(pn);

        [Route("api/Library/AddAuthor")]
        [HttpPost]
        public bool AddAuthor([FromBody] AuthorDTO u) => author.AddAuthor(u);

        [Route("api/Library/EditAuthor/{pn}")]
        [HttpPut]
        public bool EditAuthor(string pn, [FromBody] AuthorDTO u) => author.EditAuthor(pn, u);

        [Route("api/Library/DeleteAuthor/{pn}")]
        [HttpDelete]
        public bool DeleteAuthor(string pn) => author.DeleteAuthor(pn);

        /* Product */

        [Route("api/Library/GetAuthorProducts/{pn}")]
        [HttpGet]
        public IEnumerable<ProductDTO> GetAuthorProducts(string pn) => product.GetAuthorProducts(pn);

        [Route("api/Library/GetAllProducts")]
        [HttpGet]
        public IEnumerable<ProductDTO> GetAllProducts() => product.GetAllProducts();

        [Route("api/Library/GetAllProducts/{isArchived}")]
        [HttpGet]
        public IEnumerable<ProductDTO> GetAllProducts(string isArchived) => product.GetAllProducts(isArchived);

        [Route("api/Library/AddProduct/{pn}")]
        [HttpPost]
        public bool AddProduct([FromBody] ProductDTO u, string pn) => product.AddProduct(u, pn);

        [Route("api/Library/ChangeArchiveStatus/{isbn}")]
        [HttpGet]
        public bool ChangeArchiveStatus(string isbn) => product.ChangeArchiveStatus(isbn);

        [Route("api/Library/GetAllCountries")]
        [HttpGet]
        public IEnumerable<CountryDTO> GetAllCountries() => location.GetAllCountries();

        [Route("api/Library/GetAllCities")]
        [HttpGet]
        public IEnumerable<CityDTO> GetAllCities() => location.GetAllCities();

        [Route("api/Library/GetAllTypes")]
        [HttpGet]
        public IEnumerable<TypeDTO> GetAllTypes() => product.GetAllTypes();

        [Route("api/Library/GetAllProductions")]
        [HttpGet]
        public IEnumerable<ProductionDTO> GetAllProductions() => product.GetAllProductions();
        
    }
}
