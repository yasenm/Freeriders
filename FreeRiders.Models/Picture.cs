namespace FreeRiders.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using FreeRiders.Data.Common.Models;

    public class Picture : DeletableEntity
    {
        [Key]
        public int ID { get; set; }

        public int? AlbumID { get; set; }

        public virtual Album Album { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}