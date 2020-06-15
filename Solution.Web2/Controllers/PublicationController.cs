using Microsoft.AspNet.Identity;
using PagedList;
using Solution.Domain.Entities;
using Solution.Service;
using Solution.Web2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Solution.Web2.Controllers
{
    public class PublicationController : Controller


    {

        ICommentService comserv = new CommentService();
        IUserService userserv = new UserService();
        IPublicationService pubserv = new PublicationService();
        IReplyService repserv = new ReplyService();
        ILikeService likeserv = new LikeService();

        // GET: Publication

        // GET: Formation
        public ActionResult Index(string searchString, int? i)
        {
            var Publications = new List<PublicationVM>();
            foreach (Publication p in pubserv.SearchFormByName(searchString))
            {

                Publications.Add(new PublicationVM()
                {
                    title = p.title,
                    description = p.description,
                    creationDate = p.creationDate,
                    image = p.image,
                    nomuser = p.nomuser,
                    ownerimg = p.ownerimg,
                    OwnerId = p.OwnerId,
                    PublicationId = p.PublicationId,
                    visibility = (VisibilityVM)p.visibility
                });

            }
            return View(Publications.ToPagedList(i ?? 1, 3));

        }


        // GET: Publication/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publication p;
            p = pubserv.GetById((int)id);
            if (p == null) { return HttpNotFound(); }
            PublicationVM pvm = new PublicationVM()
            {
                creationDate = p.creationDate,
                description = p.description,
                image = p.image,
                //OwnerId=p.OwnerId,
                PublicationId = p.PublicationId,
                title = p.title,
                visibility = (VisibilityVM)p.visibility
            };
            GetComments((int)id);
            return View(pvm);
        }

        // GET: Publication/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Publication/Create
        [HttpPost]
        public ActionResult Create(PublicationVM PublicationVM, HttpPostedFileBase Image)
        {
            Publication PublicationDomain = new Publication();


            PublicationDomain.title = PublicationVM.title;
            PublicationDomain.creationDate = DateTime.UtcNow;
            PublicationDomain.description = PublicationVM.description;
            PublicationDomain.visibility = (Visibility)PublicationVM.visibility;
            PublicationDomain.image = Image.FileName;
            PublicationDomain.nomuser = User.Identity.GetUserName();
            PublicationDomain.OwnerId = User.Identity.GetUserId<int>(); //User.Identity.GetUserId();
            PublicationDomain.ownerimg = "exp.png"; // MyUser.GetById(User.Identity.GetUserId()).image;



            pubserv.Add(PublicationDomain);
            //notifserv.Add(comp2);
            pubserv.Commit();


            var path = Path.Combine(Server.MapPath("~/Content/Uploads"), Image.FileName);
            Image.SaveAs(path);
            return RedirectToAction("Index");
        }

        // GET: Publication/Edit/5
        public ActionResult Edit(int id)
        {
            Publication p = pubserv.GetById((int)id);
            PublicationVM pm = new PublicationVM()
            {
                creationDate = p.creationDate,
                description = p.description,
                image = p.image,
                OwnerId = p.OwnerId,
                PublicationId = p.PublicationId
            ,
                title = p.title,
                visibility = (VisibilityVM)p.visibility
            };
            return View(pm);
        }

        // POST: Publication/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PublicationVM pm, HttpPostedFileBase Image)
        {
            try
            {

                Publication p = pubserv.GetById((int)id);
                p.description = pm.description;
                p.title = pm.title;
                p.visibility = (Visibility)pm.visibility;
                p.image = Image.FileName;

                pubserv.Update(p);
                pubserv.Commit();

                var path = Path.Combine(Server.MapPath("~/Content/Uploads"), Image.FileName);
                Image.SaveAs(path);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(pm);
            }
        }

        // GET: Publication/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Publication/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Publication p = pubserv.GetById(id);
            pubserv.Delete(p);
            pubserv.Commit();
            // TODO: Add delete logic here

            return RedirectToAction("Create");
        }

        [HttpPost]
        public ActionResult CreateComment(string con, int id)
        {


            Comment comment = new Comment()
            {
                Contenu = con,
                PublicationId = id

            };

            comserv.Add(comment);
            comserv.Commit();



            return Json(comment, JsonRequestBehavior.AllowGet);

        }

        public PartialViewResult GetComments(int idpub)
        {
            var Comments = new List<CommentVM>();
            foreach (Comment p in comserv.GetMany())
            {
                if (p.PublicationId == idpub)
                {
                    Comments.Add(new CommentVM()
                    {
                        nomuser = p.nomuser,
                        ownerimg = p.ownerimg,
                        OwnerId = p.OwnerId,
                        PublicationId = p.PublicationId,
                        CommentId = p.CommentId,
                        Contenu = p.Contenu
                    });
                }


            }

            return PartialView("Comments", Comments);
        }

        [HttpPost]
        public PartialViewResult PostComment(string contenu, int idpub)
        {



            Comment comment = new Comment()
            {
                Contenu = contenu,
                PublicationId = idpub,
                nomuser = User.Identity.GetUserName(),
                OwnerId = User.Identity.GetUserId<int>(),
            ownerimg = "img"

            };

            comserv.Add(comment);
            comserv.Commit();

            //var Comments = new List<CommentVM>();
            //foreach (Comment p in MyCommentService.GetMany().ToList())
            //{
            //    if (p.PublicationId == idpub)
            //    {
            //        Comments.Add(new CommentVM()
            //        {
            //            CommentId = p.CommentId,
            //            Contenu = p.Contenu
            //        });
            //    }


            //}

            return PartialView("SingleComment", new CommentVM
            {
                CommentId = comment.CommentId,
                PublicationId = comment.PublicationId,
                Contenu = comment.Contenu
            });

        }

        [HttpPost]
        public PartialViewResult DeleteComment(int id, int idpub, FormCollection collection)
        {


            Comment comment = comserv.GetById(id);


            comserv.Delete(comment);
            comserv.Commit();

            var Comments = new List<CommentVM>();
            foreach (Comment p in comserv.GetMany())
            {
                if (p.PublicationId == idpub)
                {
                    Comments.Add(new CommentVM()
                    {
                        PublicationId = idpub,
                        CommentId = p.CommentId,
                        Contenu = p.Contenu
                    });
                }


            }
            Console.WriteLine(id + "     " + idpub);
            return PartialView("Comments", Comments);

        }

        public PartialViewResult GetLikes(int idpub)
        {
            LikeVM like;
            Like test = null;
            foreach (Like l in likeserv.GetMany())
            {

                if ((l.idPub == idpub) && (l.idUser.Equals("f43c21cf-f35a-4897-a9e3-343c00afe7b3")))
                {
                    test = l;
                }


            }

            if (test == null)
            {
                return PartialView("AddLike", new LikeVM { idPub = idpub });
            }
            else
            {
                return PartialView("DellLike", new LikeVM { idPub = idpub });
            }


        }


        [HttpPost]
        public PartialViewResult DeleteLike(int idp, FormCollection collection)
        {


            Like test = null;
            foreach (Like l in likeserv.GetMany())
            {
                if ((l.idPub == idp) && (l.idUser == User.Identity.GetUserId<int>()))
                {
                    test = l;
                }


            }


            likeserv.Delete(test);
            likeserv.Commit();


            return PartialView("AddLike", new LikeVM { idPub = idp });

        }

        [HttpPost]
        public PartialViewResult AddLike(int idpa)
        {


            Like like = new Like()
            {
                idPub = idpa,
                idUser = User.Identity.GetUserId<int>(),

        };

            likeserv.Add(like);
            likeserv.Commit();




            return PartialView("DellLike", new LikeVM { idPub = idpa });

        }

        public int nbLike(int idpub)
        {
            return likeserv.LikeNumber(idpub);
        }

        public int nbcom(int idpub)
        {
            return likeserv.LikeNumber(idpub);
        }

        public PartialViewResult GetReplies(int idCom)
        {
            var replies = new List<ReplyVM>();
            foreach (Reply p in repserv.GetMany())
            {
                if (p.CommentId == idCom)
                {
                    replies.Add(new ReplyVM()
                    {
                        OwnerId = p.OwnerId,
                        ownerimg = p.ownerimg,
                        nomuser = p.nomuser,
                        CommentId = p.CommentId,
                        Contenu = p.Contenu,
                        ReplyId = p.ReplyId

                    });
                }


            }

            return PartialView("Replies", replies);
        }


        [HttpPost]
        public PartialViewResult PostReply(string contenu, int idcom)
        {


            Reply rep = new Reply()
            {
                Contenu = contenu,
                CommentId = idcom,
                nomuser = User.Identity.GetUserName(),
                OwnerId = User.Identity.GetUserId<int>(),
            ownerimg = "img"

            };

            repserv.Add(rep);
            repserv.Commit();



            return PartialView("SingleReply", new ReplyVM
            {
                ReplyId = rep.ReplyId,
                CommentId = rep.CommentId,
                Contenu = rep.Contenu
            });

        }

        [HttpPost]
        public PartialViewResult DeleteReply(int id, int idcom, FormCollection collection)
        {


            Reply rep = repserv.GetById(id);


            repserv.Delete(rep);
            repserv.Commit();

            var reps = new List<ReplyVM>();
            foreach (Reply r in repserv.GetMany())
            {
                if (r.CommentId == idcom)
                {
                    reps.Add(new ReplyVM()
                    {
                        ReplyId = r.ReplyId,
                        CommentId = r.CommentId,
                        Contenu = r.Contenu
                    });
                }


            }

            return PartialView("Replies", reps);

        }





    }
}
