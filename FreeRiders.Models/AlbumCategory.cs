namespace FreeRiders.Models
{
    using System.ComponentModel.DataAnnotations;

    using FreeRiders.Data.Common.Models;

    public class AlbumCategory : DeletableEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
