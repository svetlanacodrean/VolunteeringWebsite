using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VolunteeringWebsite.Models;

namespace VolunteeringWebsite.Vacancies
{
    public class DetailsModel : PageModel
    {
        private readonly VolunteeringWebsite.Models.VolunteeringDatabaseContext _context;

        public DetailsModel(VolunteeringWebsite.Models.VolunteeringDatabaseContext context)
        {
            _context = context;
        }

        public Vacancy Vacancy { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vacancy = await _context.Vacancy.FirstOrDefaultAsync(m => m.Id == id);

            if (Vacancy == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
