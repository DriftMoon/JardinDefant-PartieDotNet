using Kaliko.ImageLibrary;
using Kaliko.ImageLibrary.Scaling;
using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using Gnostice.StarDocsSDK;
using Microsoft.AspNet.Identity;

namespace Solution.Web2.Controllers
{
    public class ActiviteController : Controller
    {
        public IUserService MyUser;
        //public ActiviteService MyActiviteService;
        IActiviteService activiteService = new ActiviteService();
        // GET: Activite
        public ActionResult Index(string searchString, int? i)
        {
            var Activites = new List<ActiviteVM>();
            foreach (Activite p in activiteService.SearchClassByName(searchString))
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
                    ClassSize= p.ClassSize,
                    Duration=p.Duration,
                    Professor=p.Professor,
                    Start= p.Start,
                    Location = p.Location,
                    //    nomuser = User.Identity.GetUserName(),
                    UserId = User.Identity.GetUserId<int>()
            });

            }
            return View(Activites.ToPagedList(i ?? 1 ,3));

        }

        // GET: Activite/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activite A;
            A = activiteService.GetById((int)id);
            if (A == null) { return HttpNotFound(); }
            ActiviteVM pvm = new ActiviteVM()
            {
                ActiviteID = A.ActiviteID,
                Title = A.Title,
                Description = A.Description,
                Affiche = A.Affiche,
                Document = A.Document,
                Theme = A.Theme,
                Outils = A.Outils,
                AgeMin = A.AgeMin,
                AgeMax = A.AgeMax,
                ClassSize = A.ClassSize,
                Duration = A.Duration,
                Professor = A.Professor,
                Start = A.Start,
                Location = A.Location,
                //    nomuser = User.Identity.GetUserName(),
                UserId = User.Identity.GetUserId<int>()
        };
            var t = activiteService.GetMany();
            foreach (Activite Act in t)
            {
                var path1 = Path.Combine(Server.MapPath("~/Content/Uploads"), Act.Affiche);
                KalikoImage image = new KalikoImage(path1);
                KalikoImage thumb = image.Scale(new CropScaling(90, 80));
                var path2 = Path.Combine(Server.MapPath("~/Content/Uploads"), Act.Title + "latest.jpg");
                thumb.SaveJpg(path2, 99);
            }
            List<Activite> Courses = t.ToList();
            ViewData["Courses"] = Courses;
            return View(pvm);
        }

        // GET: Activite/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Activite/Create

        [HttpPost]
        public ActionResult Create(ActiviteVM ActiviteVM, HttpPostedFileBase Affiche)
        {
            //if (!ModelState.IsValid || Affiche == null || Affiche.ContentLength == 0|| ActiviteVM.AgeMin>ActiviteVM.AgeMax)
            //{
            //    //ModelState.AddModelError("", "Verify your form !");
            //    RedirectToAction("Create");
            //}
            if (ActiviteVM.AgeMin > ActiviteVM.AgeMax)
            {
                ModelState.AddModelError("AgeMin", "Age Minimum est erronée");
                ModelState.AddModelError("AgeMax", "Age Maximum est erronée");
                return View(ActiviteVM);
            }
            try
            {
                Activite ActiviteDomain = new Activite()
            {

                Title = ActiviteVM.Title,
                Description = ActiviteVM.Description,
                Affiche = Affiche.FileName,
                Theme = ActiviteVM.Theme,
                Type = ActiviteVM.Type,
                Outils = ActiviteVM.Outils,
                AgeMin = ActiviteVM.AgeMin,
                AgeMax = ActiviteVM.AgeMax,
                ClassSize = ActiviteVM.ClassSize,
                Duration = ActiviteVM.Duration,
                Professor = ActiviteVM.Professor,
                Start = ActiviteVM.Start,
                Location = ActiviteVM.Location,
                Document= "",
                //    nomuser = User.Identity.GetUserName(),
                UserId = User.Identity.GetUserId<int>()
            };

            activiteService.Add(ActiviteDomain);
            activiteService.Commit();

            var path = Path.Combine(Server.MapPath("~/Content/Uploads"), Affiche.FileName);
            Affiche.SaveAs(path);
            return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ActiviteVM);
            }

        }


        // GET: Activite/Edit/5
        public ActionResult Edit(int id)
        {

            Activite p = activiteService.GetById((int)id);
            ActiviteVM pm = new ActiviteVM()
            {
                ActiviteID = p.ActiviteID,
                Title = p.Title,
                Description = p.Description,
                Affiche = p.Affiche,
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
                UserId = User.Identity.GetUserId<int>()
        };
            return View(pm);
        }

        // POST: Activite/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ActiviteVM p, HttpPostedFileBase Affiche)
        {
          
                Activite pm = activiteService.GetById((int)id);

                pm.Title = p.Title;
                pm.Description = p.Description;
                pm.Affiche = Affiche.FileName;
                pm.Theme = p.Theme;
                pm.Outils = p.Outils;
                pm.AgeMin = p.AgeMin;
                pm.AgeMax = p.AgeMax;
                pm.ClassSize = p.ClassSize;
                pm.Duration = p.Duration;
                pm.Professor = p.Professor;
                pm.Start = p.Start;
                pm.Location = p.Location;
                    //    nomuser = User.Identity.GetUserName(),
                pm.UserId = User.Identity.GetUserId<int>();

            activiteService.Update(pm);
                activiteService.Commit();

                var path = Path.Combine(Server.MapPath("~/Content/Uploads"), Affiche.FileName);
                Affiche.SaveAs(path);
                return RedirectToAction("Index");
          
        }

        // GET: Activite/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Activite/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Activite p = activiteService.GetById(id);
            activiteService.Delete(p);
            activiteService.Commit();
            // TODO: Add delete logic here

            return RedirectToAction("Index");
        }
        public ActionResult AjoutDocument(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult AjoutDocument(int id, HttpPostedFileBase Doc)
        {
            if ( Doc == null || Doc.ContentLength == 0)
            {
                RedirectToAction("Details", new { id = id });
            }
            try
            {
                Activite pm = activiteService.GetById((int)id);
                pm.Document = Doc.FileName;
                activiteService.Update(pm);
                activiteService.Commit();

                var path = Path.Combine(Server.MapPath("~/Content/Uploads"), Doc.FileName);
                Doc.SaveAs(path);
                return RedirectToAction("Details", new { id = id });

            }
            catch (Exception ex)
            {
                return RedirectToAction("Details", new { id = id });
            }
        }

        public ActionResult SuppDocument(int id)
        {
            try
            {
                Activite pm = activiteService.GetById((int)id);
                pm.Document = null;
                activiteService.Update(pm);
                activiteService.Commit();
                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Details", new { id = id });
            }
        }

        public ActionResult PdfViewer(string Doc)
        {
            var path = Path.Combine(Server.MapPath("~/Content/Uploads"), Doc);
            ViewerSettings viewerSettings = new ViewerSettings();
            viewerSettings.VisibleFileOperationControls.Open = true;
            ViewResponse viewResponse = MvcApplication.StarDocs.Viewer.CreateView(
                new FileObject(path), null, viewerSettings);
            return new RedirectResult(viewResponse.Url);
        }

    }
}
