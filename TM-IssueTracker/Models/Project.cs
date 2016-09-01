using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [StringLength(2048)]
        public string Description { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-dd-MM HH:mm}")]
        public DateTime CreatedOn { get; set; }

        [Required]
        public ApplicationUser CreatedBy { get; set; }

        public ICollection<Issue> Issues { get; set; }

        [NotMapped]
        public int IssuesCount { get; set; }
    }
}