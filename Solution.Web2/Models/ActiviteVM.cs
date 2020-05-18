using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Solution.Web2.Models
{
    public class ActiviteVM
    {
        [Key]
        public int ActiviteID { get; set; }
        [Required(ErrorMessage = "This field is required")]

        public string Title { get; set; }
        public string Description { get; set; }
        public string Outils { get; set; }
        public string Type { get; set; }
        public string Theme { get; set; }
        public int AgeMin { get; set; }
        public int AgeMax { get; set; }
        public int ClassSize { get; set; }
        public int Duration { get; set; }
        public string Location { get; set; }
        public string Affiche { get; set; }
        public string Document { get; set; }
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }
        public string Professor { get; set; }

        public string UserId { get; set; }

    }
}