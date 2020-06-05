using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Solution.Web2.Models
{
    public class EventModel
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
        public string DirecteurFk { get; set; }
        public string qrCode { get; set; }

    }
}