namespace FreeRiders.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using FreeRiders.Models;
    using FreeRiders.Web.Areas.Administration.ViewModels.Base;
    using FreeRiders.Web.Infrastructure.Mapping;

    public class AdminEventViewModel : AdministrationViewModel, IMapFrom<Event>
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        public string CreatorID { get; set; }

        public int LocationID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DateOfEvent { get; set; }
    }
}