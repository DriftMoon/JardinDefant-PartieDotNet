using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solution.Web2.Models
{
    public class UserVM
    {

        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }
    }
}