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

        [Required (ErrorMessage = "Le Titre est obligatoire")]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Outils { get; set; }

        public string Type { get; set; }

        public string Theme { get; set; }

        [Range(3, 10 , ErrorMessage ="veuillez saisir un age correct")]
        public int AgeMin { get; set; }

        [Range(3, 10, ErrorMessage = "veuillez saisir un age correct")]
        public int AgeMax { get; set; }

        [Range(1, 35, ErrorMessage = "veuillez saisir une valeur correct")]
        public int ClassSize { get; set; }

        [Range(1, 12, ErrorMessage = "veuillez saisir une valeur correct")]
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