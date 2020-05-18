using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data.Configurations
{
  public  class ParticipationConfiguration : EntityTypeConfiguration<Participation>
    {
        public ParticipationConfiguration()
        {
            HasRequired(p => p.Enfant).WithMany(p => p.Participations).HasForeignKey(p => p.EnfantId);

        }
    }
}
