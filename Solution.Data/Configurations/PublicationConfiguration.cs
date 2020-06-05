using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Solution.Data.Configurations
{
    class PublicationConfiguration : EntityTypeConfiguration<Publication>
    {
        public PublicationConfiguration()
        {
            HasOptional(p => p.Owner).WithMany(q => q.publications).HasForeignKey(p => p.OwnerId).WillCascadeOnDelete(false);

            HasMany(p => p.comments).WithOptional(q => q.publication).HasForeignKey(p => p.PublicationId).WillCascadeOnDelete(true);
        }
    }
}
