using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VolunteeringWebsite.Areas.Identity.Data;

namespace VolunteeringWebsite.Models
{
    public partial class Volunteer
    {
        public Volunteer()
        {
            Volunteer_Language = new HashSet<Volunteer_Language>();
            Volunteer_Skill = new HashSet<Volunteer_Skill>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        public int? GenderId { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Nationality")]
        public int? NationalityId { get; set; }

        public int? EducationId { get; set; }

        [Display(Name = "Number Of Coins")]
        public int? NumberOfCoins { get; set; }

        [ForeignKey(nameof(EducationId))]
        [InverseProperty("Volunteer")]
        public virtual Education Education { get; set; }

        [ForeignKey(nameof(GenderId))]
        [InverseProperty("Volunteer")]
        [Display(Name = "Gender")]
        public virtual Gender Gender { get; set; }

        [InverseProperty("Volunteer")]
        public virtual ICollection<Volunteer_Language> Volunteer_Language { get; set; }
        [InverseProperty("Volunteer")]
        public virtual ICollection<Volunteer_Skill> Volunteer_Skill { get; set; }

        [InverseProperty("Volunteer")]
        public VolunteeringWebsiteUser User { get; set; }
    }
}
