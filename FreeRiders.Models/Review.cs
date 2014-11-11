namespace FreeRiders.Models
{
    using System.ComponentModel.DataAnnotations;

    using FreeRiders.Data.Common.Models;

    public class Review : DeletableEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string AuthorID { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [Required]
        public int AlbumID { get; set; }

        public virtual Album Album { get; set; }

        [Required]
        [Range(0, 5)]
        public int Stars { get; set; }
    }
}
