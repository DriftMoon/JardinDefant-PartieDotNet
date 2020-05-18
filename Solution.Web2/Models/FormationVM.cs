using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Solution.Web2.Models
{
    public class FormationVM
    {
        [Key]
        public int FormationID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        [Display(Name = "Starting Date")]
        public DateTime Start { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        [Display(Name = "Ending Date")]
        public DateTime End { get; set; }
        public string Theme { get; set; }
        public bool IsFullDay { get; set; }
        public int NbrMax { get; set; }
        public double Price { get; set; }
        public string Location { get; set; }
        public string Affiche { get; set; }
        public string UserId { get; set; }
        public int Reserved { get; set; }
    }
}