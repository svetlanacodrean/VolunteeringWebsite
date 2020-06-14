using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteeringWebsite.Models
{
    public partial class Education
    {
        public Education()
        {
            Volunteer = new HashSet<Volunteer>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string InstituteName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
        public int? BackgroundId { get; set; }
        public int? CountryId { get; set; }
        public int? LevelOfEducation { get; set; }

        [ForeignKey(nameof(BackgroundId))]
        [InverseProperty("Education")]
        public virtual Background Background { get; set; }
        [ForeignKey(nameof(LevelOfEducation))]
        [InverseProperty("Education")]
        public virtual LevelOfEducation LevelOfEducationNavigation { get; set; }
        [InverseProperty("Education")]
        public virtual ICollection<Volunteer> Volunteer { get; set; }
    }
}
