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
        [Required(ErrorMessage = "Le Titre est obligatoire")]
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        [Display(Name = "Date Début")]
        public DateTime Start { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddThh:mm:ss}")]
        [Display(Name = "Date Fin")]
        public DateTime End { get; set; }
        public string Theme { get; set; }
        public bool IsFullDay { get; set; }
        [Range(5, 150, ErrorMessage = "le nombre maximum doit étre entre 5 et 150")]
        [Display(Name = "Nombre maximum: 5-150")]
        public int NbrMax { get; set; }
        [Range(0, 999.99, ErrorMessage ="veuillez saisir un prix valide!")]
        [Display(Name = "Prix: 0-999.99")]
        public double Price { get; set; }
        public string Location { get; set; }
        public string Affiche { get; set; }
        public int UserId { get; set; }
        public int Reserved { get; set; }
    }
}