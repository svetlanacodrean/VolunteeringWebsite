using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteeringWebsite.Models
{
    public partial class Volunteer_Skill
    {
        [Key]
        public int Id { get; set; }
        public int? VolunteerId { get; set; }
        public int? SkillId { get; set; }

        [ForeignKey(nameof(VolunteerId))]
        [InverseProperty("Volunteer_Skill")]
        public virtual Volunteer Volunteer { get; set; }

        [ForeignKey(nameof(SkillId))]
        public virtual Skill Skill { get; set; }
    }
}
