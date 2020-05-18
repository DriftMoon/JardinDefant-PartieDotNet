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
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Image { get; set; }
        public int Age { get; set; }
        public Sexe Sexe { get; set; }
        public int IdFormation { get; set; }
    }
}