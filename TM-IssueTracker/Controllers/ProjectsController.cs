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
    [AutoLogin]
    [Authorize(Roles = "admin")]
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected void IncludeProject(int pid)
        {
            var project = db.Projects.Where(p => p.Id == pid).Include(p => p.CreatedBy).FirstOrDefault();
            ViewBag.Project = project;
            ViewBag.Issue = null;
        }

        // GET: Projects
        [AllowAnonymous]
        public ActionResult Index(int? page)
        {            
            var pageNumber = page ?? 1;
            var projects = db.Projects.Include(p => p.CreatedBy).Include(p => p.Issues).OrderByDescending(p => p.CreatedOn).ToPagedList(pageNumber, int.Parse(ConfigurationManager.AppSettings["PageSize"]));

            foreach (Project p in projects)
            {
                p.IssuesCount = p.Issues.AsQueryable().Count();
            }

            return View(projects);
        }

        // GET: Projects/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncludeProject((int)id);
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();

                db.Projects.Add(new Project()
                {
                    Name = project.Name,
                    CreatedOn = DateTime.Now,
                    CreatedBy = user,
                    Description = project.Description
                });
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncludeProject((int)id);
            ProjectViewModel project = db.Projects.Where(p => p.Id == id).Select(p => new ProjectViewModel() { Id = p.Id, Description = p.Description, Name = p.Name }).FirstOrDefault();
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                Project prj = db.Projects.Where(p => p.Id == project.Id).Include(p => p.CreatedBy).FirstOrDefault();
                prj.Name = project.Name;
                prj.Description = project.Description;
                db.Entry(prj).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncludeProject((int)id);
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
