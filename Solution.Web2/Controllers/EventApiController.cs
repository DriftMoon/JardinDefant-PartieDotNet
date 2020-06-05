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
    public class EventApiController : ApiController
    {
        IServiceEvent MyService = null;
        private EventService ks = new EventService();
        IUserService us = new UserService();
        List<EventModel> EventModels = new List<EventModel>();
        MyContext db = new MyContext();
        public EventApiController()
        {
            MyService = new EventService();
            Index();
            EventModels = Index().ToList();

        }
        [HttpGet]
        [Route("api/UserApi")]
        public IEnumerable<UserModel> GetUsers()
        {List<User> users= us.GetMany().ToList();
            List<UserModel> usersM = new List<UserModel>();
            foreach(User u in users)
            {
                usersM.Add(new UserModel { Email = u.Email });

            }
            return usersM;
        }
        [HttpGet]
        public List<EventModel> Index()
        {
            List<Event> events = ks.GetMany().ToList();
            List<EventModel> eventModels1 = new List<EventModel>();
            foreach (Event e in events)
            {
                eventModels1.Add(new EventModel
                {
                    EventId = e.EventId,
                    Category = (Category)e.Category,
                    Description = e.Description,
                    Name = e.Name,
                    Image = e.Image,
                    DateEvent = e.DateEvent,
                    Debut = e.Debut,
                    Fin = e.Fin,
                    Phone = e.Phone,
                    qrCode=e.qrCode

                });
            }
            return eventModels1;
        }
        [HttpGet]
        [Route("api/EventApi")]
        public IEnumerable<EventModel> Get()
        {
            return EventModels;
        }
        [HttpGet]
        [Route("api/EventApi/Details")]
        public Event Get(int id)
        {
            Event Kinder = MyService.GetById(id);

            return Kinder;
        }
        [HttpGet]
        [Route("api/EventApi/QrCode")]
        public IEnumerable<Event> GetByqrCode(String id)
        {
            List<Event> Kinder = new List<Event>();
            Kinder.Add(MyService.FindEventByQrCode(id));

            return Kinder;
        }

        [Route("api/EventApi/Create")]
        public IHttpActionResult Create(EventModel e)
        {


            using (var ctx = new MyContext())
            {
                ctx.Events.Add(new Event()
                {
                    Category = (Category)e.Category,
                    Description = e.Description,
                    Name = e.Name,
                    Image = e.Image,
                    DateEvent = e.DateEvent,
                    Debut = e.Debut,
                    Fin = e.Fin,
                    Phone = e.Phone,
                    DirecteurFk = "f43c21cf-f35a-4897-a9e3-343c00afe7b3",
                    qrCode=e.qrCode
                    

            });

                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                ctx.SaveChanges();


            }

            return Ok();
        }
        [Route("api/EventApi/Put")]

        public IHttpActionResult Put(int id, EventModel em)
        {

            Event ev = MyService.GetById(id);

                if (ev != null)
                {
                   ev.Name = em.Name;
            ev.Category = em.Category;
            ev.DateEvent = em.DateEvent;
            ev.Description = em.Description;
            ev.Image = em.Image;
            ev.Phone = em.Phone;
            ev.Debut = em.Debut;
            ev.Fin = em.Fin;
            ev.DirecteurFk = "f43c21cf-f35a-4897-a9e3-343c00afe7b3";
                MyService.Update(ev);
                MyService.Commit();
                }
                else
                {
                    return NotFound();
                }
            

            return Ok();
        }
        [Route("api/EventApi/Delete")]

        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Event id");

            using (var ctx = new MyContext())
            {
                var event1 = ctx.Events
                    .Where(k => k.EventId == id)
                    .FirstOrDefault();
                ctx.Entry(event1).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
