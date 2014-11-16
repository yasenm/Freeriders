namespace FreeRiders.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class EventsUsers
    {
        [Key]
        public int ID { get; set; }

        public string UserID { get; set; }

        public virtual User User { get; set; }

        public int EventID { get; set; }

        public virtual Event Event { get; set; }
    }
}
