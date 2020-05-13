using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteeringWebsite.Models
{
    public partial class Project_Skill
    {
        public int? ProjectId { get; set; }
        public int? SkillId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; }
        [ForeignKey(nameof(SkillId))]
        public virtual Skill Skill { get; set; }
    }
}
