using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
   public class Quiz
    {
        [Key]
        public int ID { get; set; }
        public string Titre { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

    }
}
