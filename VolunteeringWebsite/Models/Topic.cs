using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteeringWebsite.Models
{
    public partial class Topic
    {
        public Topic()
        {
            Project = new HashSet<Project>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(80)]
        public string Name { get; set; }

        [InverseProperty("Topic")]
        public virtual ICollection<Project> Project { get; set; }
    }
}
