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
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        public int? GenderId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }
        public int? NationalityId { get; set; }
        public int? EducationId { get; set; }
        public int? NumberOfCoins { get; set; }

        [ForeignKey(nameof(EducationId))]
        [InverseProperty("Volunteer")]
        public virtual Education Education { get; set; }
        [ForeignKey(nameof(GenderId))]
        [InverseProperty("Volunteer")]
        public virtual Gender Gender { get; set; }
        [InverseProperty("Volunteer")]
        public virtual ICollection<Volunteer_Language> Volunteer_Language { get; set; }
        [InverseProperty("Volunteer")]
        public virtual ICollection<Volunteer_Skill> Volunteer_Skill { get; set; }

        [InverseProperty("Volunteer")]
        public VolunteeringWebsiteUser User { get; set; }
    }
}
