using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Solution.Web2.Models
{
    public enum VisibilityVM
    {
        Public,
        Private

    }
    public class PublicationVM
    {
        [Key]
        public int PublicationId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string title { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public VisibilityVM visibility { get; set; }

        public DateTime? creationDate { get; set; }

        public string ownerimg { get; set; }
        public string nomuser { get; set; }

        public string OwnerId { get; set; }

    }
}