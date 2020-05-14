﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteeringWebsite.Models
{
    public partial class Skill
    {
        public Skill()
        {
            //Project_Skill = new HashSet<Project_Skill>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        //[InverseProperty("Skill")]
        //public virtual ICollection<Project_Skill> Project_Skill { get; set; }
    }
}
