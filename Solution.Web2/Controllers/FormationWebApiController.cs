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
    public class FormationWebApiController : ApiController
    {

        IFormationService MyService = null;
        private FormationService es = new FormationService();
        List<FormationVM> formations = new List<FormationVM>();

        public FormationWebApiController()
        {
            MyService = new FormationService();
            Index();
            formations = Index().ToList();
        }

        private List<FormationVM> Index()
        {
            List<Formation> mandates = es.GetMany().ToList();
            List<FormationVM> mandatesXml = new List<FormationVM>();
            foreach (Formation p in mandates)
            {
                mandatesXml.Add(new FormationVM
                {
                    FormationID = p.FormationID,
                    Title = p.Title,
                    Start = p.Start,
                    End = p.End,
                    Description = p.Description,
                    // Affiche = p.Affiche,
                    Affiche = p.Affiche,
                    NbrMax = p.NbrMax,
                    Theme = p.Theme,
                    Location = p.Location,
                    Price = p.Price
                    
                });
            }
            return mandatesXml;
        }



        // GET api/<controller>
        [HttpGet]
        public IEnumerable<FormationVM> Get()
        {          
            return Index().ToList();
        }

        // GET api/<controller>/5
        public Formation Get(int id)
        {
            Formation f = MyService.GetById(id);
            return f;
        }

        // POST api/<controller>
        [Route("api/FormationPost")]
        public IHttpActionResult Post(FormationVM FormationVM)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest("Invalid data.");

            using (var ctx = new MyContext())
            {
                ctx.Formations.Add(new Formation()
                {
                    Title = FormationVM.Title,
                    Start = DateTime.UtcNow,
                    End = FormationVM.End,
                    Description = FormationVM.Description,
                    Affiche = FormationVM.Affiche,
                    NbrMax = FormationVM.NbrMax,
                    Theme = FormationVM.Theme,
                    Location = FormationVM.Location,
                    Price = FormationVM.Price,

                    //    nomuser = User.Identity.GetUserName(),
                    UserId = "f43c21cf-f35a-4897-a9e3-343c00afe7b3"
                });
                ctx.SaveChanges();
            }
            return Ok();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(FormationVM FormationVM)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest("Not a valid model");

            using (var ctx = new MyContext())
            {
                var existingFormation = ctx.Formations.Where(s => s.FormationID == FormationVM.FormationID)
                                                        .FirstOrDefault<Formation>();

                if (existingFormation != null)
                {
                    existingFormation.Title = FormationVM.Title;
                    existingFormation.Start = DateTime.UtcNow;
                    existingFormation.End = FormationVM.End;
                    existingFormation.Description = FormationVM.Description;
                    existingFormation.Affiche = FormationVM.Affiche;
                    existingFormation.NbrMax = FormationVM.NbrMax;
                    existingFormation.Theme = FormationVM.Theme;
                    existingFormation.Location = FormationVM.Location;
                    existingFormation.Price = FormationVM.Price;
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
                return BadRequest("Not a valid student id");

            using (var ctx = new MyContext())
            {
                var Formation = ctx.Formations
                    .Where(s => s.FormationID == id)
                    .FirstOrDefault();
                ctx.Entry(Formation).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}