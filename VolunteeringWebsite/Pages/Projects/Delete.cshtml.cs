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
    public class DeleteModel : PageModel
    {
        private readonly VolunteeringWebsite.Models.VolunteeringDatabaseContext _context;

        public DeleteModel(VolunteeringWebsite.Models.VolunteeringDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; }

        [BindProperty]
        public string Place { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string place)
        {
            Place = place;

            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Project
                .Include(p => p.Location)
                .ThenInclude(l => l.City)
                .ThenInclude(c => c.Country)
                .Include(p => p.Topic).FirstOrDefaultAsync(m => m.Id == id);

            if (Project == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Project
                .Include(p => p.Project_Language)
                .Include(p => p.Project_Skill)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (Project != null)
            {
                _context.Project.Remove(Project);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new { place = Place });
        }
    }
}
