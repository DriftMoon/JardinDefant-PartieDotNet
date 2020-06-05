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
    public class ActiviteWebApiController : ApiController
    {

        IActiviteService activiteService = new ActiviteService();
        
        // GET api/<controller>
        public IEnumerable<ActiviteVM> Get()
        {
            //var t = activiteService.GetMany();
            var Activites = new List<ActiviteVM>();
            foreach (Activite p in activiteService.GetMany())
            {
                //if (p.Description.Length > 37)
                //    p.Description = p.Description.Substring(0, 34) + "...";
                Activites.Add(new ActiviteVM()
                {
                    ActiviteID = p.ActiviteID,
                    Title = p.Title,
                    Description = p.Description,
                    Affiche = p.Affiche,
                    Document = p.Document,
                    Theme = p.Theme,
                    Outils = p.Outils,
                    AgeMin = p.AgeMin,
                    AgeMax = p.AgeMax,
                    ClassSize = p.ClassSize,
                    Duration = p.Duration,
                    Professor = p.Professor,
                    Start = p.Start,
                    Location = p.Location,
                    //    nomuser = User.Identity.GetUserName(),
                    UserId = "f43c21cf-f35a-4897-a9e3-343c00afe7b3"
                });
            }
                return Activites.ToList() ;
        }

      //  GET api/<controller>/5
        public Activite Get(int id)
        {
            return activiteService.GetById(id);
        }

     //   POST api/<controller>
        public IHttpActionResult Post(ActiviteVM ActiviteVM)
        {
            using (var ctx = new MyContext())
            {
                ctx.Activites.Add(new Activite()
                {
                    Title = ActiviteVM.Title,
                    Description = ActiviteVM.Description,
                    Affiche = ActiviteVM.Affiche,
                    Theme = ActiviteVM.Theme,
                    Outils = ActiviteVM.Outils,
                    AgeMin = ActiviteVM.AgeMin,
                    AgeMax = ActiviteVM.AgeMax,
                    ClassSize = ActiviteVM.ClassSize,
                    Duration = ActiviteVM.Duration,
                    Professor = ActiviteVM.Professor,
                    Start = ActiviteVM.Start,
                    Location = ActiviteVM.Location,
                    Document= ActiviteVM.Document,
                    //    nomuser = User.Identity.GetUserName(),
                    UserId = "f43c21cf-f35a-4897-a9e3-343c00afe7b3"
                });
                ctx.SaveChanges();
            }
            return Ok();
        }

        //// PUT api/<controller>/5
        public IHttpActionResult Put(ActiviteVM ActiviteVM)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest("Not a valid model");

            using (var ctx = new MyContext())
            {
                var existing = ctx.Activites.Where(s => s.ActiviteID == ActiviteVM.ActiviteID)
                                                        .FirstOrDefault<Activite>();

                if (existing != null)
                {
                    existing.Title = ActiviteVM.Title;
                    existing.Start = ActiviteVM.Start;
                    existing.Professor = ActiviteVM.Professor;
                    existing.Description = ActiviteVM.Description;
                    existing.Affiche = ActiviteVM.Affiche;
                    existing.Outils = ActiviteVM.Outils;
                    existing.Theme = ActiviteVM.Theme;
                    existing.Location = ActiviteVM.Location;
                    existing.ClassSize = ActiviteVM.ClassSize;
                    existing.AgeMax = ActiviteVM.AgeMax;
                    existing.AgeMin = ActiviteVM.AgeMin;
                    existing.Duration = ActiviteVM.Duration;

                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }

        //// DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
         if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new MyContext())
            {
                var Activite = ctx.Activites
                    .Where(s => s.ActiviteID == id)
                    .FirstOrDefault();
    ctx.Entry(Activite).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}