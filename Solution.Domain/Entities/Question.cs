using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
  public  class Question
    {
        public int QuestionID { get; set; }
        public string Text { get; set; }
        public string Opt1 { get; set; }
        public string Opt2 { get; set; }
        public string Opt3 { get; set; }
        public string Opt4 { get; set; }
        public int CorrectOpt { get; set; }
        public int IDQuiz { get; set; }


        [ForeignKey("IDQuiz")]
        public virtual Quiz Quiz { get; set; }
    }
}
