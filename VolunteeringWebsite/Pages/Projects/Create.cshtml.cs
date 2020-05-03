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

namespace VolunteeringWebsite
{
    public class CreateModel : PageModel
    {
        private readonly VolunteeringWebsite.Models.VolunteeringDatabaseContext _context;

        public CreateModel(VolunteeringWebsite.Models.VolunteeringDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [BindProperty]
        [Display(Name = "City")]
        public string CityName { get; set; }

        [BindProperty]
        [Display(Name = "Street")]
        public string StreetName { get; set; }

        [BindProperty]
        [Display(Name = "Number")]
        public int StreetNumber { get; set; }

        [Display(Name = "Remote project")]
        [BindProperty]
        public bool IsRemote { get; set; }

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
            if (!IsRemote && string.IsNullOrEmpty(CityName))
            {
                ModelState.AddModelError("CityName", "The field is required");
            }

            if (!ModelState.IsValid)
            {
                ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name");
                ViewData["TopicId"] = new SelectList(_context.Topic, "Id", "Name");
                return Page();
            }
            if (!IsRemote)
            {
                Location location = null;
                if (!string.IsNullOrEmpty(CityName))
                {
                    City city = await _context.City.FirstOrDefaultAsync(c => c.Name == CityName);
                    if (city == null)
                    {
                        city = new City();
                        city.Name = CityName;
                        city.CountryId = CountryId;
                    }

                    location = new Location();
                    location.StreetName = StreetName;
                    location.StreetNumber = StreetNumber;
                    location.City = city;
                }

                Project.Location = location;
            }

            _context.Project.Add(Project);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
