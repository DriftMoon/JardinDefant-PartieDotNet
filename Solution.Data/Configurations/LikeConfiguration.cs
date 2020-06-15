using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data.Configurations
{
    class LikeConfiguration : EntityTypeConfiguration<Like>
    
          {
        public LikeConfiguration()
        {
            HasRequired(p => p.user).WithMany(s => s.Likes).HasForeignKey(q => q.idUser).WillCascadeOnDelete(true);
            HasRequired(p => p.publication).WithMany(s => s.Likes).HasForeignKey(q => q.idPub).WillCascadeOnDelete(true);
        }
    

}
}
