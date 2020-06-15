using Microsoft.AspNet.Identity;
using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace Solution.Web2.Controllers
{
  
    [Authorize]
    public class ReclamationController : Controller
    {

        IReclamationService Service = new ReclamationService();
        ReclamationService rs = new ReclamationService();
        ResponseService responseService = new ResponseService();
        IUserService us = new UserService();
        // GET: Reclamation
        public ActionResult Index()
        {
            List<ReclamationVM> reclams = new List<ReclamationVM>();
            List<Reclamation> reclamations = Service.GetMany().ToList();
            foreach (var reclam in reclamations)
            {
                ReclamationVM r = new ReclamationVM();
                r.Comment = reclam.Comment;
                r.DateReclamation = reclam.DateReclamation;
                r.ComplaintType = reclam.ComplaintType;
                r.ReclamationId = reclam.ReclamationId;
                reclams.Add(r);

            }
            return View(reclams);
        }

        public ActionResult sentComplaints()
        {
            int connectedUserID = User.Identity.GetUserId<int>();
            List<ReclamationVM> reclams = new List<ReclamationVM>();
            List<Reclamation> reclamations = Service.GetMany(r => r.senderID == connectedUserID).ToList();
            foreach (var reclam in reclamations)
            {
                ReclamationVM r = new ReclamationVM();
                r.Comment = reclam.Comment;
                r.DateReclamation = reclam.DateReclamation;
                r.ComplaintType = reclam.ComplaintType;
                r.ReclamationId = reclam.ReclamationId;
                r.state = reclam.state;
                r.sender = reclam.sender;
                r.receiver = reclam.receiver;
                reclams.Add(r);

            }
            return View(reclams);
        }


        public ActionResult recievedComplaints()
        {
            int connectedUserID = User.Identity.GetUserId<int>();
            List<ReclamationVM> reclams = new List<ReclamationVM>();
            List<Reclamation> reclamations = Service.GetMany(r => r.receiverID == connectedUserID).ToList();
            foreach (var reclam in reclamations)
            {
                ReclamationVM r = new ReclamationVM();
                r.Comment = reclam.Comment;
                r.DateReclamation = reclam.DateReclamation;
                r.ComplaintType = reclam.ComplaintType;
                r.ReclamationId = reclam.ReclamationId;
                r.state = reclam.state;
                r.sender = reclam.sender;
                r.receiver = reclam.receiver;
                reclams.Add(r);

            }
            return View(reclams);
        }

        // GET: Reclamation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamation reclam = rs.GetById((int)id);
            if (reclam == null)
            {

                return HttpNotFound();
            }
            ViewBag.Reclamation = reclam;
            ReclamationVM r = new ReclamationVM();
            r.Comment = reclam.Comment;
            r.DateReclamation = reclam.DateReclamation;
            r.ComplaintType = reclam.ComplaintType;
            r.ReclamationId = reclam.ReclamationId;
            r.state = reclam.state;
            r.sender = reclam.sender;
            r.receiver = reclam.receiver;
            return View();
        }
        // POST: Reclamation/Details/5
        [HttpPost]
        public ActionResult Details(int? id, ResponseVM reponseVM)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamation reclam = rs.GetById((int)id);
            if (reclam == null)
            {

                return HttpNotFound();
            }
            int connedtedUserID = User.Identity.GetUserId<int>();
            var sender = us.GetById(connedtedUserID);
            Response r = new Response();
            r.response = reponseVM.response;
            r.DateResponse = DateTime.Now;
            r.authorId = connedtedUserID;
            r.reclamationID = id;

            responseService.Add(r);
            responseService.Commit();
            if (reclam.receiver.Id == connedtedUserID)
            {
                reclam.state = ComplaintState.traited;
                rs.Update(reclam);
                rs.Commit();
            }
            return (RedirectToAction("sentComplaints"));

        }

        // GET: Reclamation/Create
        public ActionResult Create(int userId)
        {
            var user = us.GetById(userId);
            ViewBag.user = user;

            return View();
        }

        // POST: Reclamation/Create
        [HttpPost]
        public ActionResult Create(int userId, ReclamationVM ReclamVM)
        {
            int senderId = User.Identity.GetUserId<int>();

            var sender = us.GetById(senderId);
            var receiver = us.GetById(userId);
            Reclamation r = new Reclamation();
            r.Comment = ReclamVM.Comment;
            r.DateReclamation = DateTime.Now;
            r.ComplaintType = (Complaint)ReclamVM.ComplaintType;
            r.senderID = senderId;
            r.receiverID = userId;
            rs.Add(r);
            rs.Commit();
            return (RedirectToAction("sentComplaints"));
        }

        // GET: Reclamation/Edit/5
        public ActionResult Edit(int id)
        {
            Reclamation p = Service.GetById((int)id);

            ReclamationVM pm = new ReclamationVM()
            {
                ComplaintType = p.ComplaintType,
                Comment = p.Comment,
            };
            return View(pm);
        }

        // POST: Reclamation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Reclamation comp)
        {

            try
            {

                Reclamation r = Service.GetById((int)id);
                r.ComplaintType = (Complaint)comp.ComplaintType;
                r.DateReclamation = DateTime.Now;
                r.Comment = comp.Comment;
                Service.Update(r);
                Service.Commit();
                return RedirectToAction("sentComplaints");
            }
            catch (Exception ex)
            {
                return View(comp);
            }


        }

        // GET: Reclamation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reclamation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Reclamation comp = Service.GetById(id);


            Service.Delete(comp);
            Service.Commit();
            return RedirectToAction("Index");


            return View();
        }

        public ActionResult Dashboard()
        {
            var list = Service.GetMany();
            List<int> repartition = new List<int>();
            var states = list.Select(x => x.ComplaintType).Distinct();
            foreach (var item in states)
            {

                repartition.Add(list.Count(x => x.ComplaintType == item));
            }
            var rep = repartition;
            ViewBag.type = states;
            ViewBag.REP = repartition.ToList();


            return View();
        }


    }
}
