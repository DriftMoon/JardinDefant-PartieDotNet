using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data.Configurations
{
    public class ResponseConfiguration : EntityTypeConfiguration<Response>
    {
        public ResponseConfiguration()
        {
            HasRequired(p => p.reclamation).WithMany(q => q.response).HasForeignKey(p => p.reclamationID).WillCascadeOnDelete(false);
            HasRequired(p => p.author).WithMany(q => q.responsesSent).HasForeignKey(p => p.authorId).WillCascadeOnDelete(false);
        }
    }
}
