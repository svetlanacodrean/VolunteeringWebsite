using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VolunteeringWebsite.Models
{
    public partial class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string Activities { get; set; }
        public int CoinsGiven { get; set; }
    }
}
