using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public enum Complaint
    {
        Violence, Securite, Maltraitance
    }

    public enum ComplaintState
    {
        demand, traited
    }
    public class Reclamation
    {

        [Key]
        public int ReclamationId { get; set; }
        [Display(Name = "Comment")]
        [Required(ErrorMessage = "this field is required")]
        public string Comment { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateReclamation { get; set; }
        public Complaint ComplaintType { get; set; }

        public ComplaintState state { get; set; }
        public int? senderID { get; set; }

        public virtual User sender { get; set; }

        public int? receiverID { get; set; }

        public virtual User receiver { get; set; }

        public virtual ICollection<Response> response { get; set; }
    }
}
