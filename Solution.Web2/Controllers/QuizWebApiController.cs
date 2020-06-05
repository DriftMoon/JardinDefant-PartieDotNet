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
    public class QuizWebApiController : ApiController
    {

        IQuizService quizService = new QuizService();
        // GET api/<controller>
        public IEnumerable<QuizVM> Get()
        {
            var Quizes = new List<QuizVM>();
            foreach(Quiz q in quizService.GetMany())
            {
                Quizes.Add(new QuizVM()
                {
                    ID = q.ID,
                    Titre = q.Titre
                }
                 );
            }
            return Quizes.ToList();
        }

        // GET api/<controller>/5
        public Quiz Get(int id)
        {
            return quizService.GetById(id);
        }

        // POST api/<controller>
        public IHttpActionResult Post(QuizVM quizVM)
        {
            using (var ctx = new MyContext())
            {
                ctx.Quizzes.Add(new Quiz()
                {
                    Titre = quizVM.Titre
                });
                ctx.SaveChanges();
            }

                return Ok();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(QuizVM quizVM)
        {
            using (var ctx = new MyContext())
            {
                var existing = ctx.Quizzes.Where(s => s.ID == quizVM.ID)
                                                        .FirstOrDefault<Quiz>();

                if (existing != null)
                {
                    existing.Titre = quizVM.Titre;
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
                var Quiz = ctx.Quizzes
                    .Where(s => s.ID == id)
                    .FirstOrDefault();
                ctx.Entry(Quiz).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}