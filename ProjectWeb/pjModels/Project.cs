using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace pjModels
{
    [Table("Project")]
    public class Project
    {
        [Key]
        public  int ProjectId { get; set; }
        public string ProjectName { get; set; }
        
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public bool ProjectStatus { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Job> job { get; set; }
    }
}
