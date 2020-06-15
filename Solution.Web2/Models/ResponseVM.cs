using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solution.Web2.Models
{

    public class ResponseVM
    {
        public int ResponseId { get; set; }
        public string response { get; set; }
        public DateTime DateResponse { get; set; }
        public int? reclamationID { get; set; }
        public int? authorID { get; set; }

        public virtual Reclamation reclamation { get; set; }
    }
}