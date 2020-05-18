using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public enum Sexe    
    {
        male,female
    }
    public class Enfant
    {
        [Key]
        public int EnfantId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Image { get; set; }
        public int Age { get; set; }
        public Sexe Sexe { get; set; }
        public string ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Parent Parent { get; set; }
	//ay haja
        public virtual ICollection<Participation> Participations { get; set; }
        public virtual ICollection<Activite> Activites { get; set; }

    }
}
