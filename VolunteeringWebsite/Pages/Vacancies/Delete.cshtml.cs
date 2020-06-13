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
    public class DeleteModel : PageModel
    {
        private readonly VolunteeringWebsite.Models.VolunteeringDatabaseContext _context;

        public DeleteModel(VolunteeringWebsite.Models.VolunteeringDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vacancy = await _context.Vacancy.FindAsync(id);

            if (Vacancy != null)
            {
                _context.Vacancy.Remove(Vacancy);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
