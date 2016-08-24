using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TM_IssueTracker.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public ApplicationUser CreatedBy { get; set; }
    }
}