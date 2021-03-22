using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace pjModels
{
    public class User : IdentityUser
    {
        public bool Status { get; set; }
        public ICollection<Project> Project { get; set; }
        public ICollection<Job> Job { get; set; }
    }
}
