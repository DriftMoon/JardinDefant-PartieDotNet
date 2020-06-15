using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class Reply
    {
        [Key]
        public int ReplyId { get; set; }
        public string Contenu { get; set; }
        public int? CommentId { get; set; }
        [ForeignKey("CommentId ")]
        public virtual Comment comment { get; set; }
        public string ownerimg { get; set; }
        public string nomuser { get; set; }

        public int OwnerId { get; set; }
    }
}
