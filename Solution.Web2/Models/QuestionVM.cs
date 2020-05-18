using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Solution.Web2.Models
{
    public class QuestionVM
    {
        [Key]
        public int QuestionID { get; set; }
        public string Text { get; set; }
        public string Opt1 { get; set; }
        public string Opt2 { get; set; }
        public string Opt3 { get; set; }
        public string Opt4 { get; set; }
        public int CorrectOpt { get; set; }
        public int IDQuiz { get; set; }
    }
}