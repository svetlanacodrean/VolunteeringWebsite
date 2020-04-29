using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteeringWebsite.Models
{
    public partial class Country
    {
        public Country()
        {
            City = new HashSet<City>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(2)]
        public string Iso { get; set; }
        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        [InverseProperty("Country")]
        public virtual ICollection<City> City { get; set; }
    }
}
