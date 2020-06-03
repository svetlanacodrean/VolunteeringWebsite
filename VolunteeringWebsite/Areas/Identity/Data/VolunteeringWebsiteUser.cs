using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VolunteeringWebsite.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the VolunteeringWebsiteUser class
    public class VolunteeringWebsiteUser : IdentityUser
    {
        public bool IsAdmin { get; set; }
    }
}
