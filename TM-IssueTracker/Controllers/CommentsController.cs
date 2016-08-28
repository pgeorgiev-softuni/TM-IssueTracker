using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TM_IssueTracker.Classes;
using TM_IssueTracker.Models;
using TM_IssueTracker.ViewModels;

namespace TM_IssueTracker.Controllers
{

    [Authorize(Roles = "admin")]
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected void IncludeProjectAndIssue(int pid, int sid) {
            var project = db.Projects.Where(p => p.Id == pid).Include(p => p.CreatedBy).FirstOrDefault();
            ViewBag.Project = project;
            var issue = db.Issues.Where(p => p.Id == sid).Include(p => p.CreatedBy).FirstOrDefault();
            ViewBag.Issue = issue;
        }

        // GET: Comments
        [AllowAnonymous]
        public ActionResult Index(int pid, int sid)
        {
            IncludeProjectAndIssue(pid, sid);
            var comms = db.Comments.Include(p => p.Issue).Where(p => p.Issue.Id == sid).OrderByDescending(p => p.CreatedOn).ToList();
            return View(comms);
        }

        // GET: Comments/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id, int pid, int sid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncludeProjectAndIssue(pid, sid);
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        [AllowAnonymous]
        public ActionResult Create(int pid, int sid)
        {
            IncludeProjectAndIssue(pid, sid);
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create([Bind(Include = "Id,Description,CreatedBy")] CommentViewModel comment, int pid, int sid)
        {
            IncludeProjectAndIssue(pid, sid);
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();
                var project = db.Projects.Where(p => p.Id == pid).FirstOrDefault();
                var issue = db.Issues.Where(p => p.Id == sid).FirstOrDefault();

                db.Comments.Add(
                    new Comment()
                    {
                        CreatedBy = user != null ? user.UserName : comment.CreatedBy,
                        CreatedOn = DateTime.Now,
                        Description = comment.Description,
                        Issue = issue
                    }
                );

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id, int pid, int sid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncludeProjectAndIssue(pid, sid);
            CommentViewModel comment = db.Comments.Include(p => p.Issue).Where(p => p.Issue.Id == sid).Select(p => new CommentViewModel() { Id = p.Id, CreatedBy = p.CreatedBy, Description = p.Description }).FirstOrDefault();
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,CreatedBy")] CommentViewModel comment, int pid, int sid)
        {
            if (ModelState.IsValid)
            {
                Comment cmt = db.Comments.Include(p => p.Issue).Where(p => p.Id == comment.Id).FirstOrDefault();
                cmt.Description = comment.Description;
                db.Entry(cmt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id, int pid, int sid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncludeProjectAndIssue(pid, sid);
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
