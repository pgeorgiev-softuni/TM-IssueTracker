using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TM_IssueTracker.Models
{
    public class Issue
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(2048)]
        public string Description { get; set; }

        public ApplicationUser CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public IssueState State { get; set; }
    }
}