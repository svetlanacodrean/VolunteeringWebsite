using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteeringWebsite.Models
{
    public partial class Vacancy
    {
        public Vacancy()
        {
            User_Vacancy = new HashSet<User_Vacancy>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public int? Price { get; set; }
        public int? LocationId { get; set; }

        [InverseProperty("Vacancy")]
        public virtual ICollection<User_Vacancy> User_Vacancy { get; set; }
    }
}
