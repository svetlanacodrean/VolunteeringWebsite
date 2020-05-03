using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteeringWebsite.Models
{
    public partial class Location
    {
        public Location()
        {
            Project = new HashSet<Project>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(80)]
        public string StreetName { get; set; }
        public int? StreetNumber { get; set; }
        public int? CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        [InverseProperty("Location")]
        public virtual City City { get; set; }
        [InverseProperty("Location")]
        public virtual ICollection<Project> Project { get; set; }

        public string FullName 
        { 
            get 
            {
                if (City != null)
                    return City.Name + ", " + City.Country.Name;
                else
                    return string.Empty;
            } 
        }
    }
}
