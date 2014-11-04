﻿namespace FreeRiders.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Album
    {
        private ICollection<Review> reviews;
        private ICollection<Picture> pictures;

        public Album()
        {
            this.reviews = new HashSet<Review>();
            this.pictures = new HashSet<Picture>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int CategoryID { get; set; }

        public virtual AlbumCategory Category { get; set; }

        [Required]
        public int PictureID { get; set; }

        public virtual Picture Picture { get; set; }

        public ICollection<Review> Reviews
        {
            get { return this.reviews; }
            set { this.reviews = value; }
        }

        public ICollection<Picture> Pictures
        {
            get { return this.pictures; }
            set { this.pictures = value; }
        }
    }
}
