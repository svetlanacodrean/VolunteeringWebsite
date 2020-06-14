using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteeringWebsite.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Volunteer = new HashSet<Volunteer>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(2)]
        public string ShortName { get; set; }

        [InverseProperty("Gender")]
        public virtual ICollection<Volunteer> Volunteer { get; set; }
    }
}
