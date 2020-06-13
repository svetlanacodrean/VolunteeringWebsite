using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VolunteeringWebsite.Models;

namespace VolunteeringWebsite.Vacancies
{
    public class CreateModel : PageModel
    {
        private readonly VolunteeringWebsite.Models.VolunteeringDatabaseContext _context;

        public CreateModel(VolunteeringWebsite.Models.VolunteeringDatabaseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Vacancy Vacancy { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Vacancy.Add(Vacancy);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
