namespace FreeRiders.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FreeRiders.Data.Common.Models;

    public class Album : DeletableEntity
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
        [StringLength(150, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(10000, MinimumLength = 50)]
        public string Description { get; set; }

        [Range(0, 10)]
        public double Rating { get; set; }

        [Required]
        public int LocationID { get; set; }

        public virtual Location Location { get; set; }

        //[Required]
        public string CreatorID { get; set; }

        public virtual User Creator { get; set; }

        [Required]
        public int CategoryID { get; set; }

        public virtual AlbumCategory Category { get; set; }

        [Required]
        public int PictureID { get; set; }

        public virtual Picture Picture { get; set; }

        public virtual ICollection<Review> Reviews
        {
            get { return this.reviews; }
            set { this.reviews = value; }
        }

        public virtual ICollection<Picture> Pictures
        {
            get { return this.pictures; }
            set { this.pictures = value; }
        }
    }
}
