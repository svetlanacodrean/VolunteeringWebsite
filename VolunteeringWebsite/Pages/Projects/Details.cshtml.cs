using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VolunteeringWebsite.Models;

namespace VolunteeringWebsite
{
    public class DetailsModel : PageModel
    {
        private readonly VolunteeringWebsite.Models.VolunteeringDatabaseContext _context;

        public DetailsModel(VolunteeringWebsite.Models.VolunteeringDatabaseContext context)
        {
            _context = context;
        }

        public Project Project { get; set; }
        public string Activities { get; set; }

        public string Place { get; set; }

        [Display(Name = "Languages")]
        public string ProjectLanguage { get; set; }

        [Display(Name = "Skills Required")]
        public string ProjectSkills { get; set; }

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

            if (Project.Activities != null)
                Activities = "• " + Project.Activities.Replace("\n", "\n• ");

            var projectLanguage = await _context.Project_Language.Where(l => l.ProjectId == Project.Id).Include(pl => pl.Language).ToListAsync();
            ProjectLanguage = string.Join(", ", projectLanguage.Select(pl => pl.Language.Name));

            var projectSkill = await _context.Project_Skill.Where(l => l.ProjectId == Project.Id).Include(pl => pl.Skill).ToListAsync();
            ProjectSkills = "• " + string.Join("\n• ", projectSkill.Select(pl => pl.Skill.Name));

            return Page();
        }
    }
}
