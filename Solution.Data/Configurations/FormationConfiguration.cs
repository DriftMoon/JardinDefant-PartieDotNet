using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data.Configurations
{
    public class FormationConfiguration : EntityTypeConfiguration<Formation>
    {
        public FormationConfiguration()
        {
            HasRequired(p => p.user).WithMany(p => p.Formations).HasForeignKey(p => p.UserId);

        }
    }
}
