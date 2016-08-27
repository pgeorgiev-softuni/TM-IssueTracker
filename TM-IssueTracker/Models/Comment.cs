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

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public Issue Issue { get; set; }
    }
}