using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TM_IssueTracker.Models;

namespace TM_IssueTracker.ApiControllers
{
    public class DtoNewestIssues
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Url { get; set; }
        public int ProjectId { get; set; }
        public string Project { get; set; }
    }

    public class TrackerController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Projects
        public IQueryable<Project> GetProjects()
        {
            return db.Projects;
        }

        [Route("api/Tracker/NewestProjects")]
        public IQueryable GetNewestProjects()
        {
            return db.Projects.OrderByDescending(p => p.CreatedOn).Take(3).Include(p => p.CreatedBy).Select(p => new { Name = p.Name, CreatedOn = p.CreatedOn, CreatedBy = p.CreatedBy.UserName, Id = p.Id });
        }

        [Route("api/Tracker/NewestIssues")]
        public IQueryable GetNewestIssues()
        {
            return db.Issues.OrderByDescending(p => p.CreatedOn).ThenBy(p => p.Title).Take(3).Include(p => p.CreatedBy).Include(p => p.Project).Select(p => new { Title = p.Title, CreatedOn = p.CreatedOn, CreatedBy = p.CreatedBy.UserName, ProjectId = p.Project.Id, Project = p.Project.Name, Id = p.Id });
        }

        [Route("api/Tracker/MostCommentedIssues")]
        public IQueryable GetMostCommentedIssues()
        {
            var tmp = db.Comments.GroupBy(p => p.Issue).OrderByDescending(p => p.Key.Comments.Count()).ThenBy(p => p.Key.Title).Take(3).Select(p => new { issue = p.Key, ccount = p.Count() }).Include(p => p.issue.CreatedBy).Include(p => p.issue.Project).Select(p => new { Title = p.issue.Title, CreatedOn = p.issue.CreatedOn, CreatedBy = p.issue.CreatedBy.UserName, ProjectId = p.issue.Project.Id, Project = p.issue.Project.Name, Id = p.issue.Id, ccount = p.ccount });
            
            return tmp;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectExists(int id)
        {
            return db.Projects.Count(e => e.Id == id) > 0;
        }
    }
}