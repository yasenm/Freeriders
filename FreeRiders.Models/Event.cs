namespace FreeRiders.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Event
    {
        private ICollection<ApplicationUser> joinedUsers;
        private ICollection<Message> messages;

        public Event()
        {
            this.joinedUsers = new HashSet<ApplicationUser>();
            this.messages = new HashSet<Message>();
        }

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

        public ICollection<ApplicationUser> JoinedUsers
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
