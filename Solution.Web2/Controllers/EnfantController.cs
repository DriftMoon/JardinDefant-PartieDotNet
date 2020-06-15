using Microsoft.AspNet.Identity;
using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solution.Web2.Controllers
{
    public class EnfantController : Controller
    {
        public IUserService MyUser;
        public FormationService MyFormationService;
        public ParticipationService MyParticipationService;
        public EnfantService MyEnfantService;
        // GET: Enfant

        public EnfantController()
        {
            MyUser = new UserService();
            MyFormationService = new FormationService();
            MyEnfantService = new EnfantService();
            MyParticipationService = new ParticipationService();
        }


        public ActionResult Index()
        {
            var Enfats = new List<EnfantVM>();
            IEnumerable<Enfant> pm = MyEnfantService.SearchEnfantByParent(User.Identity.GetUserId<int>());
            foreach (Enfant E in pm)
            {
                Enfats.Add(new EnfantVM()
                {
                    EnfantId = E.EnfantId,
                    Age = E.Age,
                    Image = E.Image,
                    Nom = E.Nom,
                    Prenom = E.Prenom,
                    Sexe = E.Sexe
                }
                );
            }
            return View(Enfats);
        }

        // GET: Enfant/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Enfant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Enfant/Create
        [HttpPost]
        public ActionResult Create(EnfantVM enfantVM, HttpPostedFileBase Image, FormCollection collection)
        {
            try
            {
                //if (!ModelState.IsValid || Image == null || Image.ContentLength == 0)
                //{
                //    RedirectToAction("Create");
                //}

                Enfant EnfantDomain = new Enfant()
                {
                    Nom = enfantVM.Nom,
                    Prenom = enfantVM.Prenom,
                    Image = Image.FileName,
                    Age = enfantVM.Age,
                    Sexe = enfantVM.Sexe,
                    //    nomuser = User.Identity.GetUserName(),
                    ParentId = User.Identity.GetUserId<int>()
            };

                MyEnfantService.Add(EnfantDomain);
                MyEnfantService.Commit();

                var path = Path.Combine(Server.MapPath("~/Content/Uploads"), Image.FileName);
                Image.SaveAs(path);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(enfantVM);
            }
        }

        // GET: Enfant/Edit/5
        public ActionResult Edit(int id)
        {
            Enfant p = MyEnfantService.GetById((int)id);
            EnfantVM pm = new EnfantVM()
            {
                Nom = p.Nom,
                Prenom = p.Prenom,
                Age = p.Age,
                Image = p.Image,
                Sexe = p.Sexe
            };
            return View(pm);
        }

        // POST: Enfant/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EnfantVM pm, HttpPostedFileBase Image)
        {
            try
            {
                Enfant p = MyEnfantService.GetById((int)id);
                p.Nom = pm.Nom;
                p.Prenom = pm.Prenom;
                p.Age = pm.Age;
                p.Sexe = pm.Sexe;
                p.Image = Image.FileName;
                p.ParentId = User.Identity.GetUserId<int>();

                MyEnfantService.Update(p);
                MyEnfantService.Commit();

                var path = Path.Combine(Server.MapPath("~/Content/Uploads"), Image.FileName);
                Image.SaveAs(path);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(pm);
            }
        }

        // GET: Enfant/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Enfant/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Enfant p = MyEnfantService.GetById(id);
            MyEnfantService.Delete(p);
            MyEnfantService.Commit();
            // TODO: Add delete logic here
            return RedirectToAction("Index");
        }

        public ActionResult Participation(int id)
        {
            var parts = new List<ParticipationVM>();
            IEnumerable<Participation> pp = MyParticipationService.getByEnfantID(id);
            foreach (Participation p in pp)
            {
                parts.Add(new ParticipationVM()
                {
                    EnfantId = p.EnfantId,
                    FormationID = p.FormationID,
                    Title = p.Title
                });
            }

            return View(parts);
        }

        public ActionResult Annuler(int IdEnfant, int IdFormation)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Annuler(int IdEnfant,int IdFormation, FormCollection collection)
        {
            Participation p = MyParticipationService.Get(x=>x.EnfantId==IdEnfant && x.FormationID==IdFormation);
            MyParticipationService.Delete(p);
            MyParticipationService.Commit();
            // TODO: Add delete logic here

            return RedirectToAction("Index");
        }

    }
}
