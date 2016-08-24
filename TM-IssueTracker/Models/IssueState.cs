using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM_IssueTracker.Models
{
    public class IssueState
    {
        public byte Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
