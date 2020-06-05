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
   public class ActiviteService : Service<Activite>, IActiviteService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();//l'usine de fabrication du context
        static IUnitOfWork utk = new UnitOfWork(Factory);//unité de travail a besoin du factory pour communiquer avec la base
        public ActiviteService() : base(utk)
        {

        }

        public List<Activite> getMandates()
        {
            IEnumerable<Activite> m = (from Activites in utk.GetRepositoryBase<Activite>().GetAll()
                                        select Activites);
            List<Activite> list = m.ToList<Activite>();
            return list;
        }

        public IEnumerable<Activite> SearchClassByName(string searchString)
        {
            IEnumerable<Activite> classDoamin = GetMany();
            if (!String.IsNullOrEmpty(searchString))
            {
                classDoamin = GetMany(x => x.Title.Contains(searchString));
            }
            return classDoamin;
        }
    }
}
