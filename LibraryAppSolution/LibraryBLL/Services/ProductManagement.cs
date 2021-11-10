using LibraryBLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryModels.DataViewModels;
using LibraryDAL.EF;
using System.Data.Entity;

namespace LibraryBLL.Services
{
    public class ProductManagement : IProductManagement
    {
        LibraryDBContext db = new LibraryDBContext();

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            return from prod in db.Products
                   join p in db.Productions on prod.Production_Id equals p.Production_Id
                   join type in db.Types on prod.Type_Id equals type.Type_Id
                   select new ProductDTO
                   {
                       Name = prod.Name,
                       Annotation = prod.Annotation,
                       Type = type.Type_Name,
                       ISBN = prod.ISBN,
                       Release_Date = prod.Release_Date,
                       Production = p.Production_Name,
                       PageCount = prod.PageCount,
                       Address = prod.Address,
                       IsArchived = prod.IsArchived
                   };
        }

        public IEnumerable<ProductDTO> GetAllProducts(string isArchived)
        {
            return from prod in db.Products
                   join p in db.Productions on prod.Production_Id equals p.Production_Id
                   join type in db.Types on prod.Type_Id equals type.Type_Id
                   where prod.IsArchived == (isArchived == "1")
                   select new ProductDTO
                   {
                       Name = prod.Name,
                       Annotation = prod.Annotation,
                       Type = type.Type_Name,
                       ISBN = prod.ISBN,
                       Release_Date = prod.Release_Date,
                       Production = p.Production_Name,
                       PageCount = prod.PageCount,
                       Address = prod.Address,
                       IsArchived = prod.IsArchived
                   };
        }
        
        public IEnumerable<ProductDTO> GetAuthorProducts(string author_pn)
        {
            try
            {
                return from author in db.Authors
                       join author_to_book in db.Author_To_Book on author.Id equals author_to_book.Author_Id
                       join prod in db.Products on author_to_book.Product_Id equals prod.Id
                       join p in db.Productions on prod.Production_Id equals p.Production_Id
                       join type in db.Types on prod.Type_Id equals type.Type_Id
                       where author.PN == author_pn
                       select new ProductDTO
                       {
                           Name = prod.Name,
                           Annotation = ((prod.Annotation.Length < 20) ? prod.Annotation.Substring(0, prod.Annotation.Length) : prod.Annotation.Substring(0, 20))+"...",
                           Type = type.Type_Name,
                           ISBN = prod.ISBN,
                           Release_Date = prod.Release_Date,
                           Production = p.Production_Name,
                           PageCount = prod.PageCount,
                           Address = prod.Address,
                           IsArchived = prod.IsArchived
                       };
            }
            catch(Exception ex)
            {
                return new List<ProductDTO>();
            }
        }

        public ProductDTO GetProduct(string isbn)
        {
            return (from prod in db.Products
                   join p in db.Productions on prod.Production_Id equals p.Production_Id
                   join type in db.Types on prod.Type_Id equals type.Type_Id
                   where prod.ISBN == isbn
                   select new ProductDTO
                   {
                       Name = prod.Name,
                       Annotation = prod.Annotation,
                       Type = type.Type_Name,
                       ISBN = prod.ISBN,
                       Release_Date = prod.Release_Date,
                       Production = p.Production_Name,
                       PageCount = prod.PageCount,
                       Address = prod.Address,
                       IsArchived = prod.IsArchived
                   }).FirstOrDefault();
        }
        public IEnumerable<TypeDTO> GetAllTypes()
        {
            return db.Types.Select(i => new TypeDTO
            {
                Type_Id = i.Type_Id,
                Type_Name = i.Type_Name
            });
        }
        public IEnumerable<ProductionDTO> GetAllProductions()
        {
            return db.Productions.Select(i => new ProductionDTO
            {
                Production_Id = i.Production_Id,
                Production_Name = i.Production_Name
            });
        }
        public bool AddProduct(ProductDTO product, string author_pn)
        {
            
            try
            {
                if (db.Products.Any(i => i.ISBN.Equals(product.ISBN)))
                    throw new Exception($"Product with ISBN Number {product.ISBN} already exists!");
                using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    Product udt = new Product();
                    Author_To_Book atb = new Author_To_Book();

                    udt.Name = product.Name;
                    udt.Annotation = product.Annotation;
                    udt.Type_Id = db.Types.Where(j => j.Type_Name.Equals(product.Type)).Select(j => j.Type_Id).FirstOrDefault();
                    udt.ISBN = product.ISBN;
                    udt.Release_Date = product.Release_Date;
                    udt.Production_Id = db.Productions.Where(j => j.Production_Name.Equals(product.Production)).Select(j => j.Production_Id).FirstOrDefault();
                    udt.PageCount = product.PageCount;
                    udt.Address = product.Address;
                    udt.IsArchived = product.IsArchived;
                    atb.Author_Id = db.Authors.Where(i => i.PN.Equals(author_pn)).Select(i => i.Id).First();
                    atb.Added_At = DateTime.Now;

                    db.Products.Add(udt);
                    db.Author_To_Book.Add(atb);
                    db.SaveChanges();
                    transaction.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public bool ChangeArchiveStatus(string isbn)
        {
            try
            {
                if (!db.Products.Any(i => i.ISBN.Equals(isbn)))
                    throw new Exception($"Product with ISBN Number {isbn} does not exists!");

                var prod = db.Products.Where(i => i.ISBN.Equals(isbn)).First();
                if (prod.IsArchived)
                {
                    prod.IsArchived = false;
                }
                else
                {
                    prod.IsArchived = true;
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
