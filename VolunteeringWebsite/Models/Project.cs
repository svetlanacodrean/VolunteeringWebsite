using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteeringWebsite.Models
{
    public partial class Project : IValidatableObject
    {
        public Project()
        {
            Project_Language = new HashSet<Project_Language>();
            Project_Skill = new HashSet<Project_Skill>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [StringLength(1000)]
        public string Activities { get; set; }

        [Display(Name = "Coins Given")]
        public int CoinsGiven { get; set; }
        public int? LocationId { get; set; }
        [Display(Name = "Topic")]
        public int? TopicId { get; set; }

        [ForeignKey(nameof(LocationId))]
        [InverseProperty("Project")]
        public virtual Location Location { get; set; }
        public virtual ICollection<Project_Skill> Project_Skill { get; set; }
        public virtual ICollection<Project_Language> Project_Language { get; set; }
        [ForeignKey(nameof(TopicId))]
        [InverseProperty("Project")]
        public virtual Topic Topic { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate > EndDate)
                yield return new ValidationResult(
                    "The start date should be less than end date",
                    new string[] { nameof(StartDate), nameof(EndDate) }
                    );
        }
    }
}
