namespace FreeRiders.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Picture
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public byte[] Image { get; set; }

        public DateTime DateCreated { get; set; }
    }
}