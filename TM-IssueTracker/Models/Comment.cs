using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TM_IssueTracker.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(2048)]
        public string Description { get; set; }

        public ApplicationUser CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public int IssueId { get; set; }

        public Issue Issue { get; set; }
    }
}