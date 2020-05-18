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
  public  class EnfantService : Service<Enfant>, IEnfantService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();//l'usine de fabrication du context
        static IUnitOfWork utk = new UnitOfWork(Factory);//unité de travail a besoin du factory pour communiquer avec la base
        public EnfantService() : base(utk)
        {

        }

        //Recherche
        //State
        public IEnumerable<Enfant> SearchEnfantByParent(string ParentId)
        {
            IEnumerable<Enfant> Enfantlist = GetMany();
            if (!String.IsNullOrEmpty(ParentId))
            {
                Enfantlist = GetMany(x => x.ParentId.Equals(ParentId));
            }
           return Enfantlist;
        }

        public List<Enfant> getMandates()
        {
            IEnumerable<Enfant> m = (from Enfants in utk.GetRepositoryBase<Enfant>().GetAll()
                                        select Enfants);
            List<Enfant> list = m.ToList<Enfant>();
            return list;
        }
        public Enfant GetByID(int EnfantID)
        {
            return utk.GetRepositoryBase<Enfant>().GetById(EnfantID);
        }

    }
}
