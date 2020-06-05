using Microsoft.AspNet.Identity;
using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Solution.Web2.Controllers
{
    public class EventController : Controller
    {
        IServiceEvent EventService = new EventService();

        // GET: Event
        public ActionResult Index(string name)
        {

            var events = new List<EventModel>();
            foreach (Event e in EventService.FindEventsByName(name))
            {
                EventModel es = new EventModel()
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



                };
                events.Add(es);
            }

            return View(events.ToList());

        }
        public ActionResult IndexFilter(string category)
        {

            var events = new List<EventModel>();
            foreach (Event e in EventService.FindEventsByCtegory(category))
            {
                EventModel es = new EventModel()
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



                };
                events.Add(es);
            }

            return View(events.ToList());

        }


        // GET: Event/Details/5
        public ActionResult Details(int id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event e = EventService.GetById((int)id);
            int all = EventService.GetMany().Count();
            int sport = EventService.GetMany().Where(e0 => e0.Category.ToString().Equals("Sport")).Count();
            int educative = EventService.GetMany().Where(e1 => e1.Category.ToString().Equals("Educative")).Count();
            int music = EventService.GetMany().Where(e2 => e2.Category.ToString().Equals("Music")).Count();
            int workshops = EventService.GetMany().Where(e3 => e3.Category.ToString().Equals("Workshops")).Count();
            ViewBag.all = all;
            ViewBag.sport = sport;
            ViewBag.music = music;
            ViewBag.educative = educative;
            ViewBag.workshops = workshops;

            TimeSpan span = (e.DateEvent - DateTime.Now);
            int day = span.Days;
            int hour = span.Hours;
            int minute = span.Minutes;
            int second = span.Seconds;
            ViewBag.day = day;
            ViewBag.minute = minute;
            ViewBag.hour = hour;
            ViewBag.second = second;
            if (e == null)
            {
                return HttpNotFound();
            }
            EventModel em = new EventModel()
            {
                DateEvent = e.DateEvent,
                Category = e.Category,
                Image = e.Image,
                Description = e.Description,
                Name = e.Name,
                EventId = e.EventId,
                Debut = e.Debut,
                Fin = e.Fin,



            };
            return View(em);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        public ActionResult Create(EventModel em, HttpPostedFileBase Image)
        {
            Event ev = new Event();


            ev.Name = em.Name;
            ev.Category = em.Category;
            ev.DateEvent = em.DateEvent;
            ev.Description = em.Description;
            ev.Image = Image.FileName;
            ev.Phone = em.Phone;
            ev.Debut = em.Debut;
            ev.Fin = em.Fin;
            ev.DirecteurFk = "f43c21cf-f35a-4897-a9e3-343c00afe7b3";
            EventService.Add(ev);
            EventService.Commit();

            var path2 = Path.Combine(Server.MapPath("~/Content/Uploads"), Image.FileName);
            Image.SaveAs(path2);
            return RedirectToAction("Index");
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            Event e = EventService.GetById(id);
            EventModel em = new EventModel();
            em.Name = e.Name;
            em.Phone = e.Phone;
            em.Image = e.Image;
            em.Category = e.Category;
            em.DateEvent = e.DateEvent;
            em.Description = e.Description;
            em.Debut = e.Debut;
            em.Fin = e.Fin;
            return View(em);
        }

        // POST: Event/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EventModel e, HttpPostedFileBase Image)
        {
            try
            {
                Event em = EventService.GetById(id);
                em.Name = e.Name;
                em.Image = Image.FileName;
                em.Category = e.Category;
                em.DateEvent = e.DateEvent;
                em.Description = e.Description;
                em.Phone = e.Phone;
                em.Debut = e.Debut;
                em.Fin = e.Fin;
                EventService.Update(em);
                EventService.Commit();
                return RedirectToAction("Index", "Event");
            }
            catch
            {
                return View(e);
            }

        }

        // GET: Event/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Event p = EventService.GetById(id);
            if (p == null)
                return HttpNotFound();
            Console.WriteLine("deletedddddddddddddddddddddddddddddddd");
            EventService.Delete(p);
            EventService.Commit();



            return RedirectToAction("Index");

        }
    }
}
