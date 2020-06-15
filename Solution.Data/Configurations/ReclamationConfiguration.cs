using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data.Configurations
{
    public class ReclamationConfiguration : EntityTypeConfiguration<Reclamation>
    {
        public ReclamationConfiguration()
        {
            HasRequired(p => p.receiver).WithMany(q => q.Complaintrecevies).HasForeignKey(p => p.receiverID).WillCascadeOnDelete(false);
            HasRequired(p => p.sender).WithMany(q => q.Complaintsendes).HasForeignKey(p => p.senderID).WillCascadeOnDelete(false);
        }
    }
}
