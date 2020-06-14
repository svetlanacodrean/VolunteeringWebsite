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
        [Display(Name = "Institute Name")]
        public string InstituteName { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }


        [Display(Name = "Background")]
        public int? BackgroundId { get; set; }

        [Display(Name = "Institute Country")]
        public int? CountryId { get; set; }

        [Display(Name = "Level Of Education")]
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
