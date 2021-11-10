namespace LibraryDAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Author_To_Book = new HashSet<Author_To_Book>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Annotation { get; set; }

        public int? Type_Id { get; set; }

        [Required]
        [StringLength(13)]
        public string ISBN { get; set; }

        [Column(TypeName = "date")]
        public DateTime Release_Date { get; set; }

        public int? Production_Id { get; set; }

        public int? PageCount { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        public bool IsArchived { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Author_To_Book> Author_To_Book { get; set; }

        public virtual Production Production { get; set; }

        public virtual Type Type { get; set; }
    }
}
