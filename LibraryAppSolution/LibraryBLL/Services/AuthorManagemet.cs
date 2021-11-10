using LibraryBLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryModels.DataViewModels;
using LibraryDAL.EF;

namespace LibraryBLL.Services
{
    public class AuthorManagemet : IAuthorManagement
    {
        LibraryDBContext db = new LibraryDBContext();

        public IEnumerable<AuthorDTO> GetAllAuthors()
        {
            return db.Authors.Select(i => new AuthorDTO
            {
                FirstName = i.FirstName,
                LastName = i.LastName,
                PN = i.PN,
                Phone = i.Phone               
            });
        }

        public AuthorDTO GetAuthor(string pn)
        {
            return db.Authors.Where(i => i.PN.Equals(pn)).Select(i => new AuthorDTO
            {
                FirstName = i.FirstName,
                LastName = i.LastName,
                Gender = db.Genders.Where(j => j.Gender_Id == j.Gender_Id).Select(j => j.Gender_Name).FirstOrDefault(),
                PN = i.PN,
                BirthDate = i.BirthDate,
                Country = db.Countries.Where(j => j.Country_Id == j.Country_Id).Select(j => j.Country_Name).FirstOrDefault(),
                City = db.Cities.Where(j => j.City_Id == j.City_Id).Select(j => j.City_Name).FirstOrDefault(),
                Phone = i.Phone,
                Email = i.Email
            }).FirstOrDefault();
        }

        public bool AddAuthor(AuthorDTO author)
        {
            try
            {
                if (db.Authors.Any(i => i.PN.Equals(author.PN)))
                    throw new Exception($"Author with ID Number {author.PN} already exists!");

                Author udt = new Author
                {
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    Gender_Id = db.Genders.Where(j => j.Gender_Name.Equals(author.Gender)).Select(j => j.Gender_Id).FirstOrDefault(),
                    PN = author.PN,
                    BirthDate = author.BirthDate,
                    Country_Id = db.Countries.Where(j => j.Country_Name.Equals(author.Country)).Select(j => j.Country_Id).FirstOrDefault(),
                    City_Id = db.Cities.Where(j => j.City_Name.Equals(author.City)).Select(j => j.City_Id).FirstOrDefault(),
                    Phone = author.Phone,
                    Email = author.Email
                };
                db.Authors.Add(udt);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteAuthor(string pn)
        {
            if (!db.Authors.Any(i => i.PN.Equals(pn)))
                throw new Exception($"Author with ID Number {pn} not found!");

            var udt = db.Authors.Where(i => i.PN.Equals(pn)).First();
            db.Authors.Remove(udt);
            db.SaveChanges();
            return true;
        }

        public bool EditAuthor(string pn, AuthorDTO author)
        {
            if (!db.Authors.Any(i => i.PN.Equals(pn)))
                throw new Exception($"Author with ID Number {author.PN} not found!");

            var udt = db.Authors.Where(i => i.PN.Equals(pn)).First();

            udt.FirstName = author.FirstName;
            udt.LastName = author.LastName;
            udt.Gender_Id = db.Genders.Where(j => j.Gender_Name.Equals(author.Gender)).Select(j => j.Gender_Id).FirstOrDefault();
            udt.BirthDate = author.BirthDate;
            udt.Country_Id = db.Countries.Where(j => j.Country_Name.Equals(author.Country)).Select(j => j.Country_Id).FirstOrDefault();
            udt.City_Id = db.Cities.Where(j => j.City_Name.Equals(author.City)).Select(j => j.City_Id).FirstOrDefault();
            udt.Phone = author.Phone;
            udt.Email = author.Email;

            db.SaveChanges();
            return true;
        }
    }
}
