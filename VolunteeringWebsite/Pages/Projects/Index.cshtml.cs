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

        public string Place { get; set; }

        public async Task OnGetAsync(string place)
        {
            Place = place;
            IQueryable<Project> proj = _context.Project;

            switch (place)
            {
                case Const.Place.home:
                    {
                        proj = _context.Project.Where(p => p.LocationId == null);
                        HideLocation = true;
                        break;
                    }

                case Const.Place.romania:
                    {
                        proj = _context.Project.Where(p => p.Location.City.CountryId == RomaniaId);
                        break;
                    }

                case Const.Place.abroad:
                    {
                        proj = _context.Project.Where(p => p.LocationId != null && p.Location.City.CountryId != RomaniaId);
                        break;
                    }

                case Const.Place.favourites:
                    {
                        proj = GetProjectsByStatus(Const.ProjectStatus.favourites);
                        break;
                    }

                case Const.Place.applied:
                    {
                        proj = GetProjectsByStatus(Const.ProjectStatus.applied);
                        break;
                    }

                case Const.Place.finished:
                    {
                        proj = GetProjectsByStatus(Const.ProjectStatus.finished);
                        break;
                    }

                default:
                    proj = _context.Project;
                    break;
            }

            Project = await proj
                .Include(p => p.Topic)
                .Include(p => p.Location)
                .ThenInclude(l => l.City)
                .ThenInclude(c => c.Country)
                .ToListAsync();
        }

        private IQueryable<Project> GetProjectsByStatus(int status)
        {
            return _context.Project
                .Join(_context.User_Project
                    , p => p.Id
                    , up => up.ProjectId
                    , (p, up) => new { project = p, up.Status })
                .Where(p => p.Status.Id == status)
                .Select(p => p.project);
        }
    }
}
