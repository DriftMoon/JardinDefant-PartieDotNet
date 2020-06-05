using Service.Pattern;
using Solution.Data.Infrastructure;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Service
{
   public class ParticipationService : Service<Participation>, IParticipationService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();//l'usine de fabrication du context
        static IUnitOfWork utk = new UnitOfWork(Factory);//unité de travail a besoin du factory pour communiquer avec la base
        public ParticipationService() : base(utk)
        {

        }

        public bool dejaInscrit(int EnfantId, int FormationId)
        {
            if (GetMany(x => x.EnfantId.Equals(EnfantId) && x.FormationID.Equals(FormationId)).Count() > 0)
                return true;
            return false;

        }
        


        public List<Participation> getByEnfantID(int EnfantID)
        {
            IEnumerable<Participation> m = (from Participations in utk.GetRepositoryBase<Participation>().GetMany()
                                        select Participations);
                                       m = m.Where(x => x.EnfantId == EnfantID);
            List<Participation> list = m.ToList<Participation>();
            return list;
        }

    }
}
