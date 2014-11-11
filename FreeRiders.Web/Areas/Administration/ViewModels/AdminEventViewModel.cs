namespace FreeRiders.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using FreeRiders.Models;
    using FreeRiders.Web.Infrastructure.Mapping;

    public class AdminEventViewModel : IMapFrom<Event>
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string CreatorID { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public bool Passed { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime DateOfEvent { get; set; }
    }
}