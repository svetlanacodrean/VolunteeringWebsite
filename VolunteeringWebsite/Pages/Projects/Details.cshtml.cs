using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VolunteeringWebsite.Models;
using Microsoft.AspNetCore.Identity;
using VolunteeringWebsite.Areas.Identity.Data;

namespace VolunteeringWebsite
{
    public class DetailsModel : PageModel
    {
        private readonly VolunteeringWebsite.Models.VolunteeringDatabaseContext _context;
        private readonly UserManager<VolunteeringWebsiteUser> _userManager;

        public DetailsModel(VolunteeringDatabaseContext context, UserManager<VolunteeringWebsiteUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Project Project { get; set; }
        public string Activities { get; set; }

        public bool IsApplied { get; set; }
        public bool IsFavourite { get; set; }
        public bool IsFinished { get; set; }

        public string Place { get; set; }

        [Display(Name = "Languages")]
        public string ProjectLanguage { get; set; }

        [Display(Name = "Skills Required")]
        public string ProjectSkills { get; set; }

        public async Task<IActionResult> OnPostFinishAsync(int? id)
        {
            if (!id.HasValue)
                return new JsonResult(new { finished = false });

            var user = await _userManager.GetUserAsync(User);

            Volunteer volunteer = null;

            if (user != null && user.VolunteerId.HasValue)
                volunteer = await _context.Volunteer.FirstOrDefaultAsync(v => v.Id == user.VolunteerId);

            if (volunteer == null)
            {
                return new JsonResult(new { finished = false });
            }

            if (!volunteer.NumberOfCoins.HasValue)
                volunteer.NumberOfCoins = 0;

            Project = await _context.Project.FirstOrDefaultAsync(m => m.Id == id);
            volunteer.NumberOfCoins += Project.CoinsGiven;

            _context.Attach(volunteer).State = EntityState.Modified;

            var userProject = await _context.User_Project.FirstOrDefaultAsync(up => up.ProjectId == id.Value && up.UserId == user.Id);

            if (userProject != null)
            {
                userProject.StatusId = Const.ProjectStatus.finished;
                _context.Attach(userProject).State = EntityState.Modified;


                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return new JsonResult(new { finished = false });
                }
            }
            else
            {
                _context.User_Project.Add(new User_Project
                {
                    ProjectId = id.Value,
                    StatusId = Const.ProjectStatus.finished,
                    UserId = user.Id
                });
                await _context.SaveChangesAsync();
            }

            return new JsonResult(new { finished = true });
        }

        public async Task<IActionResult> OnPostApplyAsync(int? id)
        {
            if (!id.HasValue)
                return new JsonResult(new { applied = false });

            var user = await _userManager.GetUserAsync(User);

            var userProject = await _context.User_Project.FirstOrDefaultAsync(up => up.ProjectId == id.Value && up.UserId == user.Id);

            if (userProject != null)
            {
                userProject.StatusId = Const.ProjectStatus.applied;
                _context.Attach(userProject).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return new JsonResult(new { applied = false });
                }
            }
            else
            {
                _context.User_Project.Add(new User_Project 
                {
                    ProjectId = id.Value,
                    StatusId = Const.ProjectStatus.applied,
                    UserId = user.Id
                });
                await _context.SaveChangesAsync();
            }

            return new JsonResult(new { applied = true });
        }

        public async Task<IActionResult> OnPostAddToFavouriteAsync(int? id)
        {
            if (!id.HasValue)
                return new JsonResult(new { applied = false });

            var user = await _userManager.GetUserAsync(User);

            var userProject = await _context.User_Project.FirstOrDefaultAsync(up => up.ProjectId == id.Value && up.UserId == user.Id);

            if (userProject == null)
            {
                _context.User_Project.Add(new User_Project
                {
                    ProjectId = id.Value,
                    StatusId = Const.ProjectStatus.favourites,
                    UserId = user.Id
                });
                await _context.SaveChangesAsync();
                return new JsonResult(new { added = true, removed = false });
            }
            else if (userProject.StatusId != Const.ProjectStatus.favourites)
            {
                userProject.StatusId = Const.ProjectStatus.favourites;
                _context.Attach(userProject).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return new JsonResult(new { added = true, removed = false });
            }
            else
            {
                _context.Remove(userProject);
                await _context.SaveChangesAsync();
                return new JsonResult(new { added = false, removed = true});
            }
        }

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

            if (Project.Activities != null)
                Activities = "• " + Project.Activities.Replace("\n", "\n• ");

            var projectLanguage = await _context.Project_Language.Where(l => l.ProjectId == Project.Id).Include(pl => pl.Language).ToListAsync();
            ProjectLanguage = string.Join(", ", projectLanguage.Select(pl => pl.Language.Name));

            var projectSkill = await _context.Project_Skill.Where(l => l.ProjectId == Project.Id).Include(pl => pl.Skill).ToListAsync();
            ProjectSkills = "• " + string.Join("\n• ", projectSkill.Select(pl => pl.Skill.Name));

            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var userProject = await _context.User_Project.FirstOrDefaultAsync(up => up.ProjectId == id.Value && up.UserId == user.Id);

                IsApplied = userProject != null && userProject.StatusId == Const.ProjectStatus.applied;
                IsFavourite = userProject != null && userProject.StatusId == Const.ProjectStatus.favourites;
                IsFinished = userProject != null && userProject.StatusId == Const.ProjectStatus.finished;
            }

            return Page();
        }
    }
}
