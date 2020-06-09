using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteeringWebsite.Models
{
    public partial class ProjectStatus
    {
        public ProjectStatus()
        {
            User_Project = new HashSet<User_Project>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(15)]
        public string Name { get; set; }

        [InverseProperty("Status")]
        public virtual ICollection<User_Project> User_Project { get; set; }
    }
}
