using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Solution.Web2.Models
{
    public class QuizVM
    {
        [Key]
        public int ID { get; set; }
        public string Titre { get; set; }
    }
}