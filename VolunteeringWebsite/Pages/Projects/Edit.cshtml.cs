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
    public class EditModel : PageModel
    {
        private readonly VolunteeringWebsite.Models.VolunteeringDatabaseContext _context;

        public EditModel(VolunteeringWebsite.Models.VolunteeringDatabaseContext context)
        {
            _context = context;
        }

        [Display(Name = "Country")]
        [BindProperty]
        public int CountryId { get; set; }

        [Display(Name = "City")]
        [BindProperty]
        public string CityName { get; set; }

        [Display(Name = "Street")]
        [BindProperty]
        public string StreetName { get; set; }

        [Display(Name = "Number")]
        [BindProperty]
        public int? StreetNumber { get; set; }

        [BindProperty]
        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name");
            ViewData["TopicId"] = new SelectList(_context.Topic, "Id", "Name");

            if (Project.Location != null)
            {
                StreetName = Project.Location.StreetName;
                StreetNumber = Project.Location.StreetNumber;
                CityName = Project.Location.City.Name;
                CountryId = Project.Location.City.Country.Id;
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var location = await _context.Location
                .Include(l => l.City)
                .FirstOrDefaultAsync(m => m.Id == Project.LocationId);

            location.StreetName = StreetName;
            location.StreetNumber = StreetNumber;
            location.City.Name = CityName;
            location.City.CountryId = CountryId;

            _context.Attach(Project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(Project.Id))
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

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}
