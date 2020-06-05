using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Solution.Web2.Models
{
    public class ParticipationVM
    {
        [Key]
        public int EnfantId { get; set; }
        [Key]
        public int FormationID { get; set; }
        public string Title { get; set; }
    }
}