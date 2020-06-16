using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Contenu { get; set; }
        public int? PublicationId { get; set; }
        [ForeignKey("PublicationId ")]
        public virtual Publication publication { get; set; }
        public virtual ICollection<Reply> replies { get; set; }

        public string ownerimg { get; set; }
        public string nomuser { get; set; }

        public string OwnerId { get; set; }
    }
}
