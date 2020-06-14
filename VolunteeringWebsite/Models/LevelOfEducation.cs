using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteeringWebsite.Models
{
    public partial class LevelOfEducation
    {
        public LevelOfEducation()
        {
            Education = new HashSet<Education>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("LevelOfEducationNavigation")]
        public virtual ICollection<Education> Education { get; set; }
    }
}
