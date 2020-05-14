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

        [Display(Name = "Remote project")]
        [BindProperty]
        public bool IsRemote { get; set; }


        [BindProperty]
        public string ProjectLanguageList { get; set; }

        [BindProperty]
        public string LanguageList { get; set; }

        [BindProperty]
        public string ProjectSkillList { get; set; }

        [BindProperty]
        public string SkillList { get; set; }

        [BindProperty]
        public string Place { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, string place)
        {

            Place = place;

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
            else
                IsRemote = true;

            var languages = await _context.Language.ToListAsync();
            LanguageList = Newtonsoft.Json.JsonConvert.SerializeObject(languages);

            var projectLanguage = _context.Project_Language.Where(l => l.ProjectId == Project.Id).Include(pl => pl.Language);
            foreach (var pl in projectLanguage)
            {
                pl.Project = null;
            }
            ProjectLanguageList = Newtonsoft.Json.JsonConvert.SerializeObject(projectLanguage);

            var skills = await _context.Skill.ToListAsync();
            SkillList = Newtonsoft.Json.JsonConvert.SerializeObject(skills);

            var projectSkill= _context.Project_Skill.Where(l => l.ProjectId == Project.Id).Include(pl => pl.Skill);
            foreach (var pl in projectSkill)
            {
                pl.Project = null;
            }
            ProjectSkillList = Newtonsoft.Json.JsonConvert.SerializeObject(projectSkill);

            return Page();
        }

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

            var location = await _context.Location
                .Include(l => l.City)
                .FirstOrDefaultAsync(m => m.Id == Project.LocationId);

            if (location == null)
            {
                location = new Location();
                Project.Location = location;
            }

            if (!string.IsNullOrEmpty(CityName))
            {
                City city = await _context.City.FirstOrDefaultAsync(c => c.Name == CityName && c.CountryId == CountryId);
                if (city == null)
                {
                    city = new City();
                    city.Name = CityName;
                    city.CountryId = CountryId;
                }

                location.StreetName = StreetName;
                location.StreetNumber = StreetNumber;
                location.City = city;
            }

            if (IsRemote)
            {
                Project.Location = null;
                Project.LocationId = null;
            }

            var projectLanguageDB = await _context.Project_Language.Where(l => l.ProjectId == Project.Id).ToListAsync();
            var projectLanguagePage = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Project_Language>>(ProjectLanguageList);
            foreach (var pl in projectLanguagePage)
            {
                pl.LanguageId = pl.Language.Id;
                pl.ProjectId = Project.Id;
                pl.Language = null;
            }
            foreach (var pl in projectLanguageDB)
            {
                if (projectLanguagePage.Where(l => l.LanguageId == pl.LanguageId).Count() == 0)
                    _context.Project_Language.Remove(pl);
            }
            foreach (var pl in projectLanguagePage)
            {
                if (projectLanguageDB.Where(l => l.LanguageId == pl.LanguageId).Count() == 0)
                    _context.Project_Language.Add(pl);
            }

            var projectSkillDB = await _context.Project_Skill.Where(l => l.ProjectId == Project.Id).ToListAsync();
            var projectSkillPage = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Project_Skill>>(ProjectSkillList);
            foreach (var pl in projectSkillPage)
            {
                pl.SkillId = pl.Skill.Id;
                pl.ProjectId = Project.Id;
                pl.Skill = null;
            }
            foreach (var pl in projectSkillDB)
            {
                if (projectSkillPage.Where(l => l.SkillId == pl.SkillId).Count() == 0)
                    _context.Project_Skill.Remove(pl);
            }
            foreach (var pl in projectSkillPage)
            {
                if (projectSkillDB.Where(l => l.SkillId == pl.SkillId).Count() == 0)
                    _context.Project_Skill.Add(pl);
            }

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

            return RedirectToPage("./Index", new { place = Place });
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}
