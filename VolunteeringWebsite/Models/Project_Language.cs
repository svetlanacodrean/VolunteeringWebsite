using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteeringWebsite.Models
{
    public partial class Project_Language
    {
        public int? ProjectId { get; set; }
        public int? LanguageId { get; set; }
        [Key]
        public int? Id { get; set; }

        [ForeignKey(nameof(LanguageId))]
        //[InverseProperty("Project_Language")]
        public virtual Language Language { get; set; }
        [ForeignKey(nameof(ProjectId))]
        //[InverseProperty("Project_Language")]
        public virtual Project Project { get; set; }
    }
}
