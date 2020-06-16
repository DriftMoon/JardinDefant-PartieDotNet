using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class Like
    {
        [Key, Column(Order = 1)]
        public string idUser { get; set; }
        [ForeignKey("idUser ")]

        public virtual User user { get; set; }
        [Key, Column(Order = 2)]
        public int idPub { get; set; }
        [ForeignKey("idPub ")]

        public virtual Publication publication { get; set; }
    }
}

