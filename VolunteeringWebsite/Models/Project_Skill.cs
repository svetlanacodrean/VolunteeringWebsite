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
        [Key]
        public int? Id { get; set; }

        [ForeignKey(nameof(SkillId))]
        //[InverseProperty("Project_Skill")]
        public virtual Skill Skill { get; set; }
        [ForeignKey(nameof(ProjectId))]
        //[InverseProperty("Project_Skill")]
        public virtual Project Project { get; set; }
    }
}
