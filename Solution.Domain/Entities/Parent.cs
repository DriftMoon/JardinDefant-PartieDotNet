using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class Parent:User
    {
        public int ParentId { get; set; }
        public ICollection<Enfant> Enfants { get; set; }


    }
}
