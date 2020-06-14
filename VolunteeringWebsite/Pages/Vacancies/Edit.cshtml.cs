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
    public class EditModel : PageModel
    {
        private readonly VolunteeringWebsite.Models.VolunteeringDatabaseContext _context;

        public EditModel(VolunteeringWebsite.Models.VolunteeringDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Vacancy Vacancy { get; set; }

        [Display(Name = "Country")]
        [BindProperty]
        [Required]
        public int CountryId { get; set; }

        [Display(Name = "City")]
        [BindProperty]
        [Required]
        public string CityName { get; set; }

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

            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name");

            CityName = Vacancy.Location.City.Name;
            CountryId = Vacancy.Location.City.Country.Id;

            if (Vacancy == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name");
                return Page();
            }

            var location = await _context.Location
                .Include(l => l.City)
                .FirstOrDefaultAsync(m => m.Id == Vacancy.LocationId);

            if (location == null)
            {
                location = new Location();
                Vacancy.Location = location;
            }

            City city = await _context.City.FirstOrDefaultAsync(c => c.Name == CityName && c.CountryId == CountryId);
            if (city == null)
            {
                city = new City();
                city.Name = CityName;
                city.CountryId = CountryId;
            }

            location.City = city;

            _context.Attach(Vacancy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VacancyExists(Vacancy.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool VacancyExists(int id)
        {
            return _context.Vacancy.Any(e => e.Id == id);
        }
    }
}
