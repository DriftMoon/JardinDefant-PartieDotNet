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
   
    public class PubWebApiController : ApiController
    {

        IPublicationService MyService = null;


        private PublicationService ms = new PublicationService();
        List<PublicationVM> reclams = new List<PublicationVM>();
        public PubWebApiController()
        {
            MyService = new PublicationService();
            Index();
            reclams = Index().ToList();
        }


        public List<PublicationVM> Index()
        {
            List<Publication> mandates = ms.getMandates();
            List<PublicationVM> mandatesXml = new List<PublicationVM>();
            foreach (Publication i in mandates)
            {
                mandatesXml.Add(new PublicationVM
                {
                    creationDate = i.creationDate,
                    description = i.description,
                    image = i.image,
                    ownerimg = i.ownerimg,
                    nomuser = i.nomuser,
                    OwnerId = i.OwnerId,

                    PublicationId = i.PublicationId,
                    title = i.title,

                });
            }
            return mandatesXml;
        }
        [HttpPost]
        [Route("api/PubPost")]
        public Publication Post(Publication proj)
        {


            MyService.Add(proj);
            MyService.Commit();

            return proj;
        }
        // GET: api/RecWebApi
        [Route("Publication/GetPublications")]
        public IEnumerable<PublicationVM> Get()
        {
            return reclams;
        }

        // GET: api/RecWebApi/5

        public Publication Get(int id)
        {
            Publication comp = MyService.GetById(id);

            return comp;
        }
        // POST: api/RecWebApi

        // PUT: api/RecWebApi/5
        // PUT: api/RecWebApi/5
        [HttpPut]
        public IHttpActionResult Put(PublicationVM PublicationVM)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new MyContext())
            {
                var existingFormation = ctx.Publications.Where(s => s.PublicationId == PublicationVM.PublicationId)
                                                        .FirstOrDefault<Publication>();

                if (existingFormation != null)
                {
                    existingFormation.title = PublicationVM.title;
                    existingFormation.creationDate = DateTime.UtcNow;
                    existingFormation.description = PublicationVM.description;
                    existingFormation.visibility = (Visibility)PublicationVM.visibility;
                    existingFormation.image = PublicationVM.image;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }


        // DELETE: api/RecWebApi/5
        // [Route("api/Publicationwebapi")]
        public IHttpActionResult Delete(int id)

        {
            Publication comp = MyService.GetById(id);

            MyService.Delete(comp);
            MyService.Commit();

            return Ok(comp);


        }
    }

}