using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data.Configurations
{
    class ReplyConfiguration : EntityTypeConfiguration<Reply>
    {
        public ReplyConfiguration()
        {
            HasRequired(e => e.comment).WithMany(z => z.replies).HasForeignKey(a => a.CommentId).WillCascadeOnDelete(true);
        }
    }
}
