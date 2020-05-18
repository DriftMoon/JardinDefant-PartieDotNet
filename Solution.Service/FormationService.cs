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
    public class FormationService : Service<Formation>, IFormationService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();//l'usine de fabrication du context
        static IUnitOfWork utk = new UnitOfWork(Factory);//unité de travail a besoin du factory pour communiquer avec la base
        public FormationService() : base(utk)
        {

        }

      

        public List<Formation> getMandates()
        {
            IEnumerable<Formation> m = (from Formations in utk.GetRepositoryBase<Formation>().GetAll()
                                        select Formations);
            List<Formation> list = m.ToList<Formation>();
            return list;
        }

        public IEnumerable<Formation> SearchFormByName(string searchString)
        {
            IEnumerable<Formation> FormationDoamin = GetMany();
            if (!String.IsNullOrEmpty(searchString))
            {
                FormationDoamin = GetMany(x => x.Title.Contains(searchString));
            }
            return FormationDoamin;
        }
    }
}
