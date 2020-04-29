using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteeringWebsite.Models
{
    public partial class City
    {
        public City()
        {
            Location = new HashSet<Location>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public string Name { get; set; }
        public int CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        [InverseProperty("City")]
        public virtual Country Country { get; set; }
        [InverseProperty("City")]
        public virtual ICollection<Location> Location { get; set; }
    }
}
