﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Required]
        public ApplicationUser CreatedBy { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-dd-MM HH:mm}")]
        public DateTime CreatedOn { get; set; }

        [Required]
        public IssueState State { get; set; }

        [Required]
        public Project Project { get; set; }

        public ICollection<Comment> Comments { get; set; }

        [NotMapped]
        public int CommentsCount { get; set; }
    }
}