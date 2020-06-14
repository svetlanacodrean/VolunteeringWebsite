using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using VolunteeringWebsite.Areas.Identity.Data;

namespace VolunteeringWebsite.Models
{
    public partial class VolunteeringDatabaseContext : IdentityDbContext<VolunteeringWebsiteUser>
    {
        public VolunteeringDatabaseContext()
        {
        }

        public VolunteeringDatabaseContext(DbContextOptions<VolunteeringDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User_Vacancy> User_Vacancy { get; set; }
        public virtual DbSet<Vacancy> Vacancy { get; set; }
        public virtual DbSet<ProjectStatus> ProjectStatus { get; set; }
        public virtual DbSet<User_Project> User_Project { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Level> Level { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Project_Language> Project_Language { get; set; }
        public virtual DbSet<Project_Skill> Project_Skill { get; set; }
        public virtual DbSet<Skill> Skill { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }
        public virtual DbSet<Background> Background { get; set; }
        public virtual DbSet<Education> Education { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<LevelOfEducation> LevelOfEducation { get; set; }
        public virtual DbSet<Volunteer> Volunteer { get; set; }
        public virtual DbSet<Volunteer_Language> Volunteer_Language { get; set; }
        public virtual DbSet<Volunteer_Skill> Volunteer_Skill { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=VolunteeringDatabase;Trusted_Connection=True;MultipleActiveResultSets=true");
            }

            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_city_country");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Iso).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.StreetName).IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Location)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("fk_location_city");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.Activities).IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("fk_project_location");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.TopicId)
                    .HasConstraintName("fk_project_topic");

                entity.HasMany(d => d.Project_Language)
                    .WithOne(pl => pl.Project)
                    .HasForeignKey(pl => pl.ProjectId)
                    .HasConstraintName("project_fk")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.Project_Skill)
                    .WithOne(pl => pl.Project)
                    .HasForeignKey(pl => pl.ProjectId)
                    .HasConstraintName("projectt_fk")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Project_Language>(entity =>
            {
                entity.HasOne(d => d.Language)
                    .WithMany()
                    //.WithMany(p => p.Project_Language)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("language_fk");

                //entity.HasOne(d => d.Project)
                //    .WithMany(p => p.Project_Language)
                //    .HasForeignKey(d => d.ProjectId)
                //    .HasConstraintName("project_fk")
                //    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Project_Skill>(entity =>
            {
                //entity.HasOne(d => d.Project)
                //    .WithMany(p => p.Project_Skill)
                //    .HasForeignKey(d => d.ProjectId)
                //    .HasConstraintName("projectt_fk");

                entity.HasOne(d => d.Skill)
                    .WithMany()
                    .HasForeignKey(d => d.SkillId)
                    .HasConstraintName("skill_fk");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<ProjectStatus>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<User_Project>(entity =>
            {
                entity.HasOne(d => d.Status)
                    .WithMany(p => p.User_Project)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("project_status_fk");
            });

            modelBuilder.Entity<User_Vacancy>(entity =>
            {
                entity.HasOne(d => d.Vacancy)
                    .WithMany(p => p.User_Vacancy)
                    .HasForeignKey(d => d.VacancyId)
                    .HasConstraintName("user_vacancy_fk");
            });

            modelBuilder.Entity<Vacancy>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });


            modelBuilder.Entity<Background>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.Property(e => e.InstituteName).IsUnicode(false);

                entity.HasOne(d => d.Background)
                    .WithMany(p => p.Education)
                    .HasForeignKey(d => d.BackgroundId)
                    .HasConstraintName("education_background_fk");

                entity.HasOne(d => d.LevelOfEducationNavigation)
                    .WithMany(p => p.Education)
                    .HasForeignKey(d => d.LevelOfEducation)
                    .HasConstraintName("education_level_fk");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.ShortName).IsUnicode(false);
            });

            modelBuilder.Entity<LevelOfEducation>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Volunteer>(entity =>
            {
                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.HasOne(d => d.Education)
                    .WithMany(p => p.Volunteer)
                    .HasForeignKey(d => d.EducationId)
                    .HasConstraintName("volunteer_education_fk");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Volunteer)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("volunteer_gender_fk");
            });

            modelBuilder.Entity<Volunteer_Language>(entity =>
            {
                entity.HasOne(d => d.Volunteer)
                    .WithMany(p => p.Volunteer_Language)
                    .HasForeignKey(d => d.VolunteerId)
                    .HasConstraintName("volunteer_language_fk");
            });

            modelBuilder.Entity<Volunteer_Skill>(entity =>
            {
                entity.HasOne(d => d.Volunteer)
                    .WithMany(p => p.Volunteer_Skill)
                    .HasForeignKey(d => d.VolunteerId)
                    .HasConstraintName("volunteer_skill_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
