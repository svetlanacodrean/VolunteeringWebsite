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

        [ForeignKey(nameof(LanguageId))]
        public virtual Language Language { get; set; }
        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; }
    }
}
