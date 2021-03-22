using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace pjModels
{
    [Table("Job")]
    public class Job
    {
        [Key]
        public int JobId { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public string JobName { get; set; }
        public bool Status { get; set; }
        [Column(TypeName = "Date")]
        public DateTime ? JobDate { get; set; }
        public virtual  ICollection<User> User{ get; set; }
        public virtual Project Project { get; set; }
    }
}
