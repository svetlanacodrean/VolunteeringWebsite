using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteeringWebsite.Models
{
    public partial class User_Vacancy
    {
        [Key]
        public int Id { get; set; }
        [StringLength(450)]
        public string UserId { get; set; }
        public int? VacancyId { get; set; }

        [ForeignKey(nameof(VacancyId))]
        [InverseProperty("User_Vacancy")]
        public virtual Vacancy Vacancy { get; set; }
    }
}
