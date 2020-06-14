using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteeringWebsite.Models
{
    public partial class Volunteer_Language
    {
        [Key]
        public int Id { get; set; }
        public int? VolunteerId { get; set; }
        public int? LanguageId { get; set; }

        [ForeignKey(nameof(VolunteerId))]
        [InverseProperty("Volunteer_Language")]
        public virtual Volunteer Volunteer { get; set; }

        [ForeignKey(nameof(LanguageId))]
        public virtual Language Language { get; set; }
    }
}
