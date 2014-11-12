namespace FreeRiders.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FreeRiders.Data.Common.Models;

    public class Event : DeletableEntity
    {
        private ICollection<User> joinedUsers;
        private ICollection<Message> messages;

        public Event()
        {
            this.joinedUsers = new HashSet<User>();
            this.messages = new HashSet<Message>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        public string CreatorID { get; set; }

        public virtual User Creator { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        //[Required]
        //public int LocationID { get; set; }

        //public virtual Location Location { get; set; }

        [Required]
        public DateTime DateOfEvent { get; set; }

        public ICollection<User> JoinedUsers
        {
            get { return this.joinedUsers; }
            set { this.joinedUsers = value; }
        }

        public ICollection<Message> Messages
        {
            get { return this.messages; }
            set { this.messages = value; }
        }
    }
}
