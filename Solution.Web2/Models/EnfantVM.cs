using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Solution.Web2.Models
{
    public class EnfantVM
    {
        [Key]
        public int EnfantId { get; set; }

        [Required(ErrorMessage = "Le nom de l'enfant doit être saisi")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le prenom de l'enfant doit être saisi")]
        public string Prenom { get; set; }

        public string Image { get; set; }

        [Range(3, 10)]
        [Display(Name = "Age: 3-10")]
        public int Age { get; set; }

        public Sexe Sexe { get; set; }

        public int IdFormation { get; set; }
    }
}