using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class Formation
    {
        [Key]
        public int FormationID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Theme { get; set; }
        public bool IsFullDay { get; set; }
        public int NbrMax { get; set; }
        public int Reserved { get; set; }
        public double Price { get; set; }
        public string Location { get; set; }
        public string Affiche { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId ")]
        public virtual User user { get; set; }

    }
}
