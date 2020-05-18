using Solution.Data;
using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Solution.Web2.Controllers
{
    public class QuestionWebApiController : ApiController
    {
        IQuestionService questionService = new QuestionService();
        // GET api/<controller>
        public IEnumerable<QuestionVM> Get()
        {
            var Questions = new List<QuestionVM>();
            foreach (Question q in questionService.GetMany())
            {
                Questions.Add(new QuestionVM()
                {
                    QuestionID = q.QuestionID,
                    Text= q.Text,
                    Opt1=q.Opt1,
                    Opt2=q.Opt2,
                    Opt3=q.Opt3,
                    Opt4=q.Opt4,
                    CorrectOpt= q.CorrectOpt,
                    IDQuiz=q.IDQuiz
                }
                 );
            }
            return Questions.ToList();
        }



        // GET api/<controller>/5
        public Question Get(int id)
        {
            return questionService.GetById(id);
        }

        // POST api/<controller>
        public IHttpActionResult Post(QuestionVM questionVM)
        {
            using (var ctx = new MyContext())
            {
                ctx.Questions.Add(new Question()
                {
                   CorrectOpt=questionVM.CorrectOpt,
                   IDQuiz=questionVM.IDQuiz,
                   Opt1=questionVM.Opt1,
                   Opt2=questionVM.Opt2,
                   Opt3=questionVM.Opt3,
                   Opt4=questionVM.Opt4,
                   Text=questionVM.Text
                });
                ctx.SaveChanges();
            }
            return Ok();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put( QuestionVM questionVM)
        {
            using (var ctx = new MyContext())
            {
                var existing = ctx.Questions.Where(s => s.QuestionID ==questionVM.QuestionID)
                                                        .FirstOrDefault<Question>();

                if (existing != null)
                {
                    existing.Text = questionVM.Text;
                    existing.IDQuiz = questionVM.IDQuiz;
                    existing.CorrectOpt = questionVM.CorrectOpt;
                    existing.Opt1 = questionVM.Opt1;
                    existing.Opt2 = questionVM.Opt2;
                    existing.Opt3 = questionVM.Opt3;
                    existing.Opt4 = questionVM.Opt4;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid id");

            using (var ctx = new MyContext())
            {
                var Question = ctx.Questions
                    .Where(s => s.QuestionID == id)
                    .FirstOrDefault();
                ctx.Entry(Question).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}