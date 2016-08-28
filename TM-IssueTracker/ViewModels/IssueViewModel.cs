using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TM_IssueTracker.Models;

namespace TM_IssueTracker.ViewModels
{
    public class IssueViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(2048)]
        public string Description { get; set; }

        public byte StateId { get; set; }
        public IEnumerable<IssueState> State { get; set; }

    }
}