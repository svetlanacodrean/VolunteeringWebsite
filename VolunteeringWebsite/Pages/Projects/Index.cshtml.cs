using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VolunteeringWebsite.Models;

namespace VolunteeringWebsite
{
    public class IndexModel : PageModel
    {
        private const int RomaniaId = 176;
        private readonly VolunteeringWebsite.Models.VolunteeringDatabaseContext _context;

        public IndexModel(VolunteeringWebsite.Models.VolunteeringDatabaseContext context)
        {
            _context = context;
        }

        public IList<Project> Project { get;set; }
        public bool HideLocation { get; set; }
        public int Color { get; set; }

        public async Task OnGetAsync(string place)
        {
            IQueryable<Project> proj = _context.Project;

            if (place == "home")
            {
                proj = _context.Project.Where(p => p.LocationId == null);
                HideLocation = true;
                Color = 0;
            }
            else if (place == "romania")
            {
                proj = _context.Project.Where(p => p.Location.City.CountryId == RomaniaId);
                Color = 1;
            }
            else
            {
                proj = _context.Project.Where(p => p.LocationId != null && p.Location.City.CountryId != RomaniaId);
                Color = 2;
            }

            Project = await proj
                .Include(p => p.Topic)
                .Include(p => p.Location)
                .ThenInclude(l => l.City)
                .ThenInclude(c => c.Country)
                .ToListAsync();
        }
    }
}
