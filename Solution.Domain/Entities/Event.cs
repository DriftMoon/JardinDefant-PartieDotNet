using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public enum Category
    {
        Music, Educative, Sport, Workshops

    }

    public class Event
    {
        
        public int EventId { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public int Phone { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateEvent { get; set; }
        public DateTime Debut { get; set; }
        public DateTime Fin { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string qrCode { get; set; }
        public string DirecteurFk { get; set; }

        [ForeignKey("DirecteurFk")]
        public virtual Directeur Directeur { get; set; }






    }

}
