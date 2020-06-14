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

        [BindProperty]
        public string VolunteerLanguageList { get; set; }

        [BindProperty]
        public string LanguageList { get; set; }

        [BindProperty]
        public string VolunteerSkillList { get; set; }

        [BindProperty]
        public string SkillList { get; set; }

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

            var languages = await _context.Language.ToListAsync();
            LanguageList = Newtonsoft.Json.JsonConvert.SerializeObject(languages);

            var volunteerLanguage = _context.Volunteer_Language
                .Where(l => l.VolunteerId == Volunteer.Id)
                .Include(vl => vl.Language);

            foreach (var pl in volunteerLanguage)
            {
                pl.Volunteer = null;
            }
            VolunteerLanguageList = Newtonsoft.Json.JsonConvert.SerializeObject(volunteerLanguage);

            var skills = await _context.Skill.ToListAsync();
            SkillList = Newtonsoft.Json.JsonConvert.SerializeObject(skills);

            var volunteerSkill = _context.Volunteer_Skill
                .Where(s => s.VolunteerId == Volunteer.Id)
                .Include(vs => vs.Skill);

            foreach (var vs in volunteerSkill)
            {
                vs.Volunteer = null;
            }
            VolunteerSkillList = Newtonsoft.Json.JsonConvert.SerializeObject(volunteerSkill);

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

            var projectLanguageDB = await _context.Volunteer_Language.Where(l => l.VolunteerId == Volunteer.Id).ToListAsync();
            var projectLanguagePage = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Volunteer_Language>>(VolunteerLanguageList);
            foreach (var pl in projectLanguagePage)
            {
                pl.LanguageId = pl.Language.Id;
                pl.VolunteerId = Volunteer.Id;
                pl.Language = null;
            }
            foreach (var pl in projectLanguageDB)
            {
                if (projectLanguagePage.Where(l => l.LanguageId == pl.LanguageId).Count() == 0)
                    _context.Volunteer_Language.Remove(pl);
            }
            foreach (var pl in projectLanguagePage)
            {
                if (projectLanguageDB.Where(l => l.LanguageId == pl.LanguageId).Count() == 0)
                    _context.Volunteer_Language.Add(pl);
            }

            var projectSkillDB = await _context.Volunteer_Skill.Where(l => l.VolunteerId == Volunteer.Id).ToListAsync();
            var projectSkillPage = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Volunteer_Skill>>(VolunteerSkillList);
            foreach (var pl in projectSkillPage)
            {
                pl.SkillId = pl.Skill.Id;
                pl.VolunteerId = Volunteer.Id;
                pl.Skill = null;
            }
            foreach (var pl in projectSkillDB)
            {
                if (projectSkillPage.Where(l => l.SkillId == pl.SkillId).Count() == 0)
                    _context.Volunteer_Skill.Remove(pl);
            }
            foreach (var pl in projectSkillPage)
            {
                if (projectSkillDB.Where(l => l.SkillId == pl.SkillId).Count() == 0)
                    _context.Volunteer_Skill.Add(pl);
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

            return base.Page();
        }

        private bool VolunteerExists(int id)
        {
            return _context.Volunteer.Any(e => e.Id == id);
        }
    }
}
