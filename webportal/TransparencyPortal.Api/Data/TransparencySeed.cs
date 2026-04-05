using Microsoft.EntityFrameworkCore;
using TransparencyPortal.Api.Models;

namespace TransparencyPortal.Api.Data;

public static class TransparencySeed
{
    public static async Task EnsureSeedAsync(AppDbContext db, CancellationToken cancellationToken = default)
    {
        if (await db.DefaultQuestions.AnyAsync(cancellationToken))
            return;

        var now = DateTime.UtcNow;
        var q1 = new DefaultQuestion
        {
            Slug = "how-prioritization-works",
            Title = "How does prioritization work when I browse or search?",
            DisplayOrder = 10,
            IsActive = true,
            CreatedAt = now,
            UpdatedAt = now,
        };
        var q2 = new DefaultQuestion
        {
            Slug = "how-content-is-shown",
            Title = "How is content chosen or ordered for me?",
            DisplayOrder = 20,
            IsActive = true,
            CreatedAt = now,
            UpdatedAt = now,
        };
        db.DefaultQuestions.AddRange(q1, q2);
        await db.SaveChangesAsync(cancellationToken);

        db.PresetResponses.AddRange(
            new PresetResponse
            {
                DefaultQuestionId = q1.Id,
                BodyText =
                    "This is a fixed, pre-approved response (not generated for your specific case). Our systems may use signals such as relevance, quality, and operational factors to order or surface items. For more detail, see our published transparency materials. Follow-up questions on this topic are reviewed by our team.",
                Version = 1,
                PublishedAt = now,
            },
            new PresetResponse
            {
                DefaultQuestionId = q2.Id,
                BodyText =
                    "This is a fixed, pre-approved response. What you see can depend on product rules, your settings, and system signals. This answer does not address a single account decision. For individualized questions, use a custom inquiry; those are handled by staff from the first substantive response.",
                Version = 1,
                PublishedAt = now,
            });
        await db.SaveChangesAsync(cancellationToken);
    }
}
