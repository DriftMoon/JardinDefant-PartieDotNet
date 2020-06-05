using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
   public class Activite
    {
        [Key]
        public int ActiviteID { get; set; }
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
        public DateTime Start { get; set; }
        public string Professor { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User user { get; set; }

        public virtual ICollection<Enfant> Enfants { get; set; }


    }
}
