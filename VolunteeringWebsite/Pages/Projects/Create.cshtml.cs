using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VolunteeringWebsite.Models;

namespace VolunteeringWebsite
{
    public class CreateModel : PageModel
    {
        private readonly VolunteeringWebsite.Models.VolunteeringDatabaseContext _context;

        public CreateModel(VolunteeringWebsite.Models.VolunteeringDatabaseContext context)
        {
            _context = context;
        }

        [Display(Name = "Country")]
        public int CountryId { get; set; }
        [Display(Name = "City")]
        public string CityName { get; set; }
        [Display(Name = "Street")]
        public string StreetName { get; set; }
        [Display(Name = "Number")]
        public string StreetNumber { get; set; }

        public IActionResult OnGet()
        {
        ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name");
        ViewData["TopicId"] = new SelectList(_context.Topic, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Project Project { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Project.Add(Project);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
