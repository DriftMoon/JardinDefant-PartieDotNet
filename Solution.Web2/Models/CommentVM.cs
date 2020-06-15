using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solution.Web2.Models
{
    public class CommentVM
    {
        public int CommentId { get; set; }
        public string Contenu { get; set; }
        public int? PublicationId { get; set; }

        public virtual Publication publication { get; set; }

        public string ownerimg { get; set; }
        public string nomuser { get; set; }

        public int OwnerId { get; set; }
    }
}