using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VolunteeringWebsite.Areas.Identity.Data;
using VolunteeringWebsite.Models;

namespace VolunteeringWebsite.Vacancies
{
    public class DetailsModel : PageModel
    {
        private readonly VolunteeringWebsite.Models.VolunteeringDatabaseContext _context;
        private readonly UserManager<VolunteeringWebsiteUser> _userManager;

        public DetailsModel(VolunteeringWebsite.Models.VolunteeringDatabaseContext context, UserManager<VolunteeringWebsiteUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Vacancy Vacancy { get; set; }
        public bool IsApplied { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vacancy = await _context.Vacancy
                .Include(p => p.Location)
                .ThenInclude(l => l.City)
                .ThenInclude(c => c.Country)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Vacancy == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var userVacancy = await _context.User_Vacancy.FirstOrDefaultAsync(uv => uv.VacancyId == id.Value && uv.UserId == user.Id);
                IsApplied = userVacancy != null;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostApplyAsync(int? id)
        {
            if (!id.HasValue)
                return new JsonResult(new { applied = false });

            var user = await _userManager.GetUserAsync(User);

            var userVacancy = await _context.User_Vacancy.FirstOrDefaultAsync(uv => uv.VacancyId == id.Value && uv.UserId == user.Id);

            if (userVacancy != null)
            {
                return new JsonResult(new { applied = false });
            }
            else
            {
                _context.User_Vacancy.Add(new User_Vacancy
                {
                    VacancyId = id.Value,
                    UserId = user.Id
                });
                await _context.SaveChangesAsync();
            }

            return new JsonResult(new { applied = true });
        }
    }
}
