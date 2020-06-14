using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VolunteeringWebsite.Areas.Identity.Data;
using VolunteeringWebsite.Models;

namespace VolunteeringWebsite.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly VolunteeringDatabaseContext _context;
        private readonly UserManager<VolunteeringWebsiteUser> _userManager;


        public ProfileModel(VolunteeringDatabaseContext context, UserManager<VolunteeringWebsiteUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Volunteer Volunteer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (id == null && user != null)
            {
                id = user.VolunteerId;
            }

            if (id == null)
            {
                return RedirectToPage("/Account/Manage/Index", new { area = "Identity" });
            }

            Volunteer = await _context.Volunteer
                .Include(v => v.Education)
                .Include(v => v.Gender).FirstOrDefaultAsync(m => m.Id == id);

            if (Volunteer == null)
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
                
                return Page();
            }

            _context.Attach(Volunteer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolunteerExists(Volunteer.Id))
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

        public override PageResult Page()
        {
            ViewData["GenderId"] = new SelectList(_context.Set<Gender>(), "Id", "Name");
            ViewData["NationalityId"] = new SelectList(_context.Set<Country>(), "Id", "Name");
            ViewData["BackgroundId"] = new SelectList(_context.Set<Background>(), "Id", "Name");
            ViewData["CountryId"] = new SelectList(_context.Set<Country>(), "Id", "Name");
            ViewData["LevelOfEducation"] = new SelectList(_context.Set<LevelOfEducation>(), "Id", "Name");

            //ViewData["EducationId"] = new SelectList(_context.Set<Education>(), "Id", "Id");
            return base.Page();
        }

        private bool VolunteerExists(int id)
        {
            return _context.Volunteer.Any(e => e.Id == id);
        }
    }
}
