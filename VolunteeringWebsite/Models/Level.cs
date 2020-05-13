using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteeringWebsite.Models
{
    public partial class Level
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
    }
}
