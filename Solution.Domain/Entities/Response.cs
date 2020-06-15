using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class Response
    {
        [Key]
        public int ResponseId { get; set; }
        [Display(Name = "response")]
        [Required(ErrorMessage = "this field is required")]
        public string response { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateResponse { get; set; }
        public int? reclamationID { get; set; }

        public virtual Reclamation reclamation { get; set; }

        public int? authorId { get; set; }

        public virtual User author { get; set; }


    }
}
