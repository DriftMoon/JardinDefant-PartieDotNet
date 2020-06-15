using Microsoft.AspNet.Identity;
using Solution.Domain.Entities;
using Solution.Web2.Models;
using Solution.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kaliko.ImageLibrary;
using Kaliko.ImageLibrary.Scaling;
using PagedList;

namespace Solution.Web2.Controllers
{
    public class FormationController : Controller
    {
        public IUserService MyUser;
        public FormationService MyFormationService;
        public  ParticipationService MyParticipationService;
        public EnfantService MyEnfantService;
        public ActiviteService MyActiviteService;


        public FormationController()
        {
            MyUser = new UserService();
            MyFormationService = new FormationService();
            MyEnfantService = new EnfantService();
            MyParticipationService = new ParticipationService();
            MyActiviteService = new ActiviteService();
        }
        // GET: Formation
        public ActionResult Index(string searchString, int? i)
        {
            var Formations = new List<FormationVM>();
            foreach (Formation p in MyFormationService.SearchFormByName(searchString))
            {

                if (string.IsNullOrEmpty(p.Affiche))
                {
                    var path = Path.Combine(Server.MapPath("~/Content/Front/images/event/event_02.jpg"));
                    KalikoImage image = new KalikoImage(path);
                    KalikoImage thumb = image.Scale(new CropScaling(250, 250));
                    var path2 = Path.Combine(Server.MapPath("~/Content/Uploads"), p.Title + "thumb.jpg");
                    thumb.SaveJpg(path2, 99);
                }
                else
                {
                    var path = Path.Combine(Server.MapPath("~/Content/Uploads"), p.Affiche);
                    KalikoImage image = new KalikoImage(path);
                    KalikoImage thumb = image.Scale(new CropScaling(250, 250));
                    var path2 = Path.Combine(Server.MapPath("~/Content/Uploads"), p.Title + "thumb.jpg");
                    thumb.SaveJpg(path2, 99);
                }
                

                Formations.Add(new FormationVM()
                {
                    FormationID = p.FormationID,
                    Title = p.Title,
                    Start = p.Start,
                    End = p.End,
                    Description = p.Description,
                    // Affiche = p.Affiche,
                    Affiche = p.Title + "thumb.jpg",
                    NbrMax = p.NbrMax,
                    Theme = p.Theme,
                    Location = p.Location,
                    Price = p.Price
                }) ;

            }
            return View(Formations.ToPagedList(i ?? 1, 3));

        }




        //calendar
        public ActionResult Calendar()
        {
            return View();
        }
        public JsonResult GetFormations()
        {
            var Formationss = new List<FormationVM>();
            foreach (Formation p in MyFormationService.GetMany())
            {
                Formationss.Add(new FormationVM()
                {
                    FormationID = p.FormationID,
                    Title = p.Title,
                    Start = p.Start,
                    End = p.End,
                    Description = p.Description,
                    Affiche = p.Affiche,
                    //Affiche = p.Title + "thumb.jpg",
                    NbrMax = p.NbrMax,
                    Theme = p.Theme,
                    Location = p.Location,
                    Price = p.Price,
                    Reserved = p.Reserved,
                    UserId=p.UserId
                }) ;

            }
            return new JsonResult { Data = Formationss, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }





        // GET: Formation/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formation p;
            p = MyFormationService.GetById((int)id);
            if (p == null) { return HttpNotFound(); }
            FormationVM pvm = new FormationVM()
            {
                FormationID= p.FormationID,
                Title = p.Title,
                Affiche = p.Affiche,
                Start = p.Start,
                End = p.End,
                Description = p.Description,
                Location = p.Location,
                Price = p.Price,
                Theme = p.Theme,
                NbrMax = p.NbrMax

            };
             var t = MyActiviteService.GetMany();
            foreach(Activite A in t)
            {
                if (string.IsNullOrEmpty(A.Affiche))
                {
                    var path = Path.Combine(Server.MapPath("~/Content/Front/images/event/event_02.jpg"));
                    KalikoImage image = new KalikoImage(path);
                    KalikoImage thumb = image.Scale(new CropScaling(90, 80));
                    var path2 = Path.Combine(Server.MapPath("~/Content/Uploads"), A.Title + "latest.jpg");
                    thumb.SaveJpg(path2, 99);
                    A.Affiche = A.Title + "latest.jpg";
                }
                else
                {
                    var path = Path.Combine(Server.MapPath("~/Content/Uploads"), A.Affiche);
                    KalikoImage image = new KalikoImage(path);
                    KalikoImage thumb = image.Scale(new CropScaling(90, 80));
                    var path2 = Path.Combine(Server.MapPath("~/Content/Uploads"), A.Title + "latest.jpg");
                    thumb.SaveJpg(path2, 99);
                    A.Affiche = A.Title + "latest.jpg";
                }

            }
            List<Activite> Courses = t.OrderByDescending(act => act.Start).Take(5).ToList();
            ViewData["Courses"] = Courses;

            return View(pvm);
        }

        //get
        public ActionResult Paricipate(int IdFormation)
        {
            var Enfats = new List<EnfantVM>();
            Formation f = MyFormationService.GetById((int)IdFormation);
            IEnumerable<Enfant> pm = MyEnfantService.SearchEnfantByParent(User.Identity.GetUserId<int>());
            foreach (Enfant E in pm)
            {
                if ((f.NbrMax > f.Reserved)&& (!MyParticipationService.dejaInscrit(E.EnfantId,f.FormationID))) { 
                    Enfats.Add(new EnfantVM()
                  {
                     EnfantId= E.EnfantId,
                     Age = E.Age,
                     Image= E.Image,
                     Nom=E.Nom,
                     Prenom = E.Prenom,
                     Sexe=E.Sexe,
                     IdFormation= IdFormation
                    }
                    );
            
                }
            }
            return View(Enfats);
        }

        public ActionResult Paricipate1(int IdEnfant, int IdFormation)
        {

            Formation f = MyFormationService.GetById((int)IdFormation);
            f.Reserved ++;
            Participation p = new Participation()
            {
                EnfantId = IdEnfant,
                FormationID= IdFormation,
                Title= f.Title

            };
            MyFormationService.Update(f);
            MyFormationService.Commit();
            MyParticipationService.Add(p);
            MyParticipationService.Commit();

            return RedirectToAction("Paricipate", new { IdFormation = IdFormation });

        }

        // GET: Formation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Formation/Create
        [HttpPost]
        public ActionResult Create(FormationVM FormationVM, HttpPostedFileBase Affiche)
        {
            //if (!ModelState.IsValid || Affiche == null || Affiche.ContentLength == 0)
            //{
            //    RedirectToAction("Create");
            //}
            if (FormationVM.Start > FormationVM.End)
            {
                ModelState.AddModelError("Start", "Date de début est erronée");
                ModelState.AddModelError("End", "Date de Fin est erronée");
                return View(FormationVM);
            }
                
                    Formation FormationDomain = new Formation()
                    {
                        Title = FormationVM.Title,
                        Start = FormationVM.Start,
                        End = FormationVM.End,
                        Description = FormationVM.Description,
                        Affiche = Affiche.FileName,
                        NbrMax = FormationVM.NbrMax,
                        Theme = FormationVM.Theme,
                        Location = FormationVM.Location,
                        Price = FormationVM.Price,

                        //    nomuser = User.Identity.GetUserName(),
                        UserId = User.Identity.GetUserId<int>()
        };

                    MyFormationService.Add(FormationDomain);
                    MyFormationService.Commit();
                
                    var path = Path.Combine(Server.MapPath("~/Content/Uploads"), Affiche.FileName);
                    Affiche.SaveAs(path);
                    return RedirectToAction("Index");
                

        }

        // GET: Formation/Edit/5
        public ActionResult Edit(int id)
        {

            Formation p = MyFormationService.GetById((int)id);
            FormationVM pm = new FormationVM()
            {
                
                Start = p.Start,
                End = p.End,
                Description = p.Description,
                Affiche = p.Affiche,
                UserId = p.UserId,
                FormationID = p.FormationID,
                Theme = p.Theme,
                Title = p.Title,
                Location = p.Location,
                NbrMax = p.NbrMax,
                Price = p.Price
            };
            return View(pm);
        }

        // POST: Formation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormationVM pm, HttpPostedFileBase Affiche)
        {
            

                Formation p = MyFormationService.GetById((int)id);

                p.Start = pm.Start;
                p.End = pm.End;
                p.Description = pm.Description;
                p.Affiche = Affiche.FileName;
                p.UserId = pm.UserId;
                p.FormationID = pm.FormationID;
                p.Theme = pm.Theme;
                p.Title = pm.Title;
                p.Location = pm.Location;
                p.NbrMax = pm.NbrMax;
                p.Price = pm.Price;
                p.UserId = User.Identity.GetUserId<int>();
            MyFormationService.Update(p);
                MyFormationService.Commit();

                var path = Path.Combine(Server.MapPath("~/Content/Uploads"), Affiche.FileName);
                Affiche.SaveAs(path);
                return RedirectToAction("Index");
          
        }

        // GET: Formation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Formation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Formation p = MyFormationService.GetById(id);
            MyFormationService.Delete(p);
            MyFormationService.Commit();
            // TODO: Add delete logic here

            return RedirectToAction("Index");
        }





        [HttpPost]
        public JsonResult SaveEvent(FormationVM e)
        {
            var status = false;


            if (e.FormationID > 0)
            {
                //Update the event
                            
                Formation v = MyFormationService.GetById(e.FormationID);
                if (v != null)
                {
                    v.Title = e.Title;
                    v.Start = (DateTime)e.Start;
                    v.End = e.End;
                    v.Description = e.Description;
                  //  v.IsFullDay = e.IsFullDay;
                    v.Theme = e.Theme;
                    v.Price = e.Price;
                    v.Location = e.Location;
                    v.NbrMax = e.NbrMax;
                    v.Affiche = e.Affiche;
                  //  v.UserId = User.Identity.GetUserId();
                    MyFormationService.Update(v);

                }
            }
            else
            {
                Formation v = new Formation()
                {
                    Title = e.Title,
                    Start = e.Start,
                    End = e.End,
                    Description = e.Description,
                    Affiche = e.Affiche,
                    NbrMax = e.NbrMax,
                    Theme = e.Theme,
                    Location = e.Location,
                    Price = e.Price,

                    //    nomuser = User.Identity.GetUserName(),
                    UserId = User.Identity.GetUserId<int>()


            };
                MyFormationService.Add(v);
                
            }

            MyFormationService.Commit();
           
            status = true;
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteFormation(int ID)
        {
            var status = false;

            var v = MyFormationService.GetById(ID);
            if (v != null)
            {
                MyFormationService.Delete(v);
                MyFormationService.Commit();
                status = true;
            }

            return new JsonResult { Data = new { status = status } };
        }


    }
}
