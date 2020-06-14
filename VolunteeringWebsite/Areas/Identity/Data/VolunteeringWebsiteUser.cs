using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VolunteeringWebsite.Models;

namespace VolunteeringWebsite.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the VolunteeringWebsiteUser class
    public class VolunteeringWebsiteUser : IdentityUser
    {
        public bool IsAdmin { get; set; }
        public int? VolunteerId { get; set; }

        [ForeignKey(nameof(VolunteerId))]
        [InverseProperty("User")]
        public Volunteer Volunteer { get; set; }
    }
}
