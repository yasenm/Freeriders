namespace FreeRiders.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Picture
    {
        [Key]
        public int ID { get; set; }

        public int? AlbumID { get; set; }

        public virtual Album Album { get; set; }

        //public int? LocationID { get; set; }

        //public virtual Location Location { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public DateTime DateCreated { get; set; }
    }
}