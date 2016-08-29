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
using PagedList;
using System.Configuration;

namespace TM_IssueTracker.Controllers
{
    [AutoLoginAttribute]
    [Authorize(Roles = "admin")]
    public class IssuesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected void IncludeProject(int pid, int? sid = null)
        {
            var project = db.Projects.Where(p => p.Id == pid).Include(p => p.CreatedBy).FirstOrDefault();
            ViewBag.Project = project;
            if (sid != null)
            {
                var issue = db.Issues.Where(p => p.Id == sid).Include(p => p.CreatedBy).FirstOrDefault();
                ViewBag.Issue = issue;
            }
            else
            {
                ViewBag.Issue = null;
            }
        }

        // GET: Issues
        [AllowAnonymous]
        public ActionResult Index(int pid, int? state, string search, int? page)
        {
            IncludeProject(pid);
            var pageNumber = page ?? 1;

            Session["Issues_vc"] = new PageInfo()
            {
                Page = page,
                State = state,
                Search = search
            };

            var states = db.IssueStates.ToList();
            states.Insert(0, new IssueState() { Id = 0, Name = "All" });
            ViewBag.States = states;

            ViewBag.FilterSearch = search;

            IEnumerable<TM_IssueTracker.Models.Issue> issues = null;

            if (state == null)
            {
                state = 0;
            }

            if ((int)state > 0)
            {
                ViewBag.StateId = state;
                issues = db.Issues.Include(p => p.Comments).Include(p => p.Project).Include(p => p.State).Where(p => p.Project.Id == pid && p.State.Id == state).Include(p => p.CreatedBy).OrderByDescending(p => p.CreatedOn);
            }
            else
            {
                ViewBag.StateId = 0;
                issues = db.Issues.Include(p => p.Comments).Include(p => p.Project).Include(p => p.State).Where(p => p.Project.Id == pid).Include(p => p.CreatedBy).OrderByDescending(p => p.CreatedOn);
            }

            if (search != null && search != "")
            {
                issues = issues.Where(p => p.Title.ToLower().IndexOf(search.ToLower()) >= 0);
            }

            issues = issues.ToPagedList(pageNumber, int.Parse(ConfigurationManager.AppSettings["PageSize"]));

            foreach (Issue i in issues)
            {
                i.CommentsCount = i.Comments.AsQueryable().Count();
            };

            return View(issues);
        }

        // GET: Issues/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id, int pid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncludeProject(pid, id);
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        // GET: Issues/Create
        public ActionResult Create(int pid)
        {
            IncludeProject(pid);
            return View();
        }

        // POST: Issues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description")] IssueViewModel issue, int pid)
        {
            IncludeProject(pid);
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();
                var project = db.Projects.Where(p => p.Id == pid).FirstOrDefault();
                var state = db.IssueStates.Where(p => p.Name == "New").FirstOrDefault();

                db.Issues.Add(
                    new Issue()
                    {
                        CreatedBy = user,
                        CreatedOn = DateTime.Now,
                        Title = issue.Title,
                        Description = issue.Description,
                        State = state,
                        Project = project
                    }
                );

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(issue);
        }

        // GET: Issues/Edit/5
        public ActionResult Edit(int? id, int pid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncludeProject(pid, id);
            var states = db.IssueStates;
            ViewBag.States = states;
            IssueViewModel issue = db.Issues.Where(p => p.Id == id).Select(p => new IssueViewModel() { Id = p.Id, Description = p.Description, Title = p.Title, StateId = p.State.Id }).FirstOrDefault();
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,StateId")] IssueViewModel issue, int pid)
        {
            IncludeProject(pid);
            if (ModelState.IsValid)
            {
                IssueState state = db.IssueStates.Where(p => p.Id == issue.StateId).FirstOrDefault();
                Issue iss = db.Issues.Where(p => p.Id == issue.Id).Include(p => p.Project).Include(p => p.CreatedBy).Include(p => p.State).Include(p => p.Comments).FirstOrDefault();
                iss.Title = issue.Title;
                iss.Description = issue.Description;
                iss.State = state;
                db.Entry(iss).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(issue);
        }

        // GET: Issues/Delete/5
        public ActionResult Delete(int? id, int pid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncludeProject(pid, id);
            Issue issue = db.Issues.Find(id);
            if (issue == null)
            {
                return HttpNotFound();
            }
            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Issue issue = db.Issues.Find(id);
            db.Issues.Remove(issue);
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
