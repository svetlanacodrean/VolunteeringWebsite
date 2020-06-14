using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name");

            return Page();
        }

        [BindProperty]
        public Vacancy Vacancy { get; set; }

        [BindProperty]
        [Display(Name = "Country")]
        [Required]
        public int CountryId { get; set; }

        [BindProperty]
        [Display(Name = "City")]
        [Required]
        public string CityName { get; set; }


        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name");
                return Page();
            }

            Location location = null;
            City city = await _context.City.FirstOrDefaultAsync(c => c.Name == CityName && c.CountryId == CountryId);
            if (city == null)
            {
                city = new City();
                city.Name = CityName;
                city.CountryId = CountryId;
            }

            location = new Location();
            location.City = city;

            Vacancy.Location = location;

            _context.Vacancy.Add(Vacancy);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
