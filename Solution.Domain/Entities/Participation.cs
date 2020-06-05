using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
  public  class Participation
    {
        [Key, Column(Order = 0)]
        public int EnfantId { get; set; }
        [Key, Column(Order = 1)]
        public int FormationID { get; set; }
        public string Title { get; set; }
        public virtual Enfant Enfant { get; set; }
        public virtual Formation Formation { get; set; }
    }
}
