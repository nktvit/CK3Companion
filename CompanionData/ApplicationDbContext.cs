using CompanionDomain.Models;
using CompanionDomain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CompanionData
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Trait> Traits { get; set; }
        public DbSet<NonApplicableTrait> NonApplicableTraits { get; set; }
        public DbSet<SkillModifier> SkillModifiers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var databasePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "localStorage.db");
            Console.WriteLine($"Database path: {databasePath}");
            options.UseSqlite($"Data Source={databasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NonApplicableTrait>()
                .HasKey(nat => new { nat.TraitId, nat.OppositeTraitId });

            modelBuilder.Entity<NonApplicableTrait>()
                .HasOne(nat => nat.Trait)
                .WithMany(t => t.NonApplicableTraits)
                .HasForeignKey(nat => nat.TraitId);

            modelBuilder.Entity<NonApplicableTrait>()
                .HasOne(nat => nat.OppositeTrait)
                .WithMany()
                .HasForeignKey(nat => nat.OppositeTraitId);

            modelBuilder.Entity<SkillModifier>()
                .HasKey(sm => new { sm.TraitId, sm.Skill });

            modelBuilder.Entity<SkillModifier>()
                .HasOne(sm => sm.Trait)
                .WithMany(t => t.SkillModifiers)
                .HasForeignKey(sm => sm.TraitId);

            modelBuilder.Entity<SkillModifier>()
                .Property(sm => sm.Skill)
                .HasConversion(
                    skill => skill.ToString(),
                    value => (Skill)Enum.Parse(typeof(Skill), value)
                );
        }
    }
}