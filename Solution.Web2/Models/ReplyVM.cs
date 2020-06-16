using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solution.Web2.Models
{
    public class ReplyVM
    {
        public int ReplyId { get; set; }
        public string Contenu { get; set; }
        public int? CommentId { get; set; }

        public string ownerimg { get; set; }
        public string nomuser { get; set; }

        public string OwnerId { get; set; }
    }
}