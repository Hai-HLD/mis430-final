using Microsoft.EntityFrameworkCore;
using TransparencyPortal.Api.Models;

namespace TransparencyPortal.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<DefaultQuestion> DefaultQuestions => Set<DefaultQuestion>();
    public DbSet<PresetResponse> PresetResponses => Set<PresetResponse>();
    public DbSet<Inquiry> Inquiries => Set<Inquiry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DefaultQuestion>(e =>
        {
            e.ToTable("default_questions");
            e.Property(x => x.Slug).HasMaxLength(128);
            e.Property(x => x.Title).HasMaxLength(512);
            e.HasIndex(x => x.Slug).IsUnique();
            e.Property(x => x.CreatedAt).HasPrecision(6);
            e.Property(x => x.UpdatedAt).HasPrecision(6);
        });

        modelBuilder.Entity<PresetResponse>(e =>
        {
            e.ToTable("preset_responses");
            e.Property(x => x.PublishedAt).HasPrecision(6);
            e.HasOne(x => x.DefaultQuestion)
                .WithMany(q => q.PresetResponses)
                .HasForeignKey(x => x.DefaultQuestionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Inquiry>(e =>
        {
            e.ToTable("inquiries");
            e.Property(x => x.Id).HasMaxLength(36);
            e.Property(x => x.PathType).HasMaxLength(16);
            e.Property(x => x.FirstResponseAt).HasPrecision(6);
            e.Property(x => x.CreatedAt).HasPrecision(6);
            e.HasIndex(x => x.CreatedAt);
            e.HasIndex(x => x.PathType);
            e.HasOne(x => x.DefaultQuestion)
                .WithMany(q => q.Inquiries)
                .HasForeignKey(x => x.DefaultQuestionId)
                .OnDelete(DeleteBehavior.SetNull);
            e.HasOne(x => x.PresetResponse)
                .WithMany(p => p.Inquiries)
                .HasForeignKey(x => x.PresetResponseId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }
}
