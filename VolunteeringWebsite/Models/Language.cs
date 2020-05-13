using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteeringWebsite.Models
{
    public partial class Language
    {
        public Language()
        {
            //Project_Language = new HashSet<Project_Language>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public string Name { get; set; }

        //[InverseProperty("Language")]
        //public virtual ICollection<Project_Language> Project_Language { get; set; }
    }
}
