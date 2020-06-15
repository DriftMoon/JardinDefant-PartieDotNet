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
    public class PublicationService : Service<Publication>, IPublicationService
    {

        static IDataBaseFactory Factory = new DataBaseFactory();//l'usine de fabrication du context
        static IUnitOfWork utk = new UnitOfWork(Factory);//unité de travail a besoin du factory pour communiquer avec la base

        public PublicationService() : base(utk)
        {

        }

        public List<Publication> getMandates()
        {
            IEnumerable<Publication> m = (from publications in utk.GetRepositoryBase<Publication>().GetAll()
                                          select publications);
            List<Publication> list = m.ToList<Publication>();
            return list;
        }
        public IEnumerable<Publication> SearchFormByName(string searchString)
        {
            IEnumerable<Publication> FormationDoamin = GetMany();
            if (!String.IsNullOrEmpty(searchString))
            {
                FormationDoamin = GetMany(x => x.title.Contains(searchString));
            }
            return FormationDoamin;
        }
    }
}

