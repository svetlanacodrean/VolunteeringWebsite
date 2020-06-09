using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteeringWebsite.Models
{
    public partial class User_Project
    {
        [Key]
        public int Id { get; set; }
        [StringLength(450)]
        public string UserId { get; set; }
        public int? ProjectId { get; set; }
        public int? StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        [InverseProperty(nameof(ProjectStatus.User_Project))]
        public virtual ProjectStatus Status { get; set; }
    }
}
