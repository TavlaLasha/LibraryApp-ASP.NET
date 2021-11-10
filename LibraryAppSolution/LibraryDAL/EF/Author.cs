namespace LibraryDAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Author")]
    public partial class Author
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Author()
        {
            Author_To_Book = new HashSet<Author_To_Book>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public int Gender_Id { get; set; }

        [Required]
        [StringLength(11)]
        public string PN { get; set; }

        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        public int Country_Id { get; set; }

        public int City_Id { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(80)]
        public string Email { get; set; }

        public virtual City City { get; set; }

        public virtual Country Country { get; set; }

        public virtual Gender Gender { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Author_To_Book> Author_To_Book { get; set; }
    }
}
