using LibraryModels.DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBLL.Contracts
{
    public interface IAuthorManagement
    {
        IEnumerable<AuthorDTO> GetAllAuthors();
        AuthorDTO GetAuthor(string pn);
        bool AddAuthor(AuthorDTO author);
        bool EditAuthor(string pn, AuthorDTO author);
        bool DeleteAuthor(string pn);
    }
}
