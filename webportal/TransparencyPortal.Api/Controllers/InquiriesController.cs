using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransparencyPortal.Api.Contracts;
using TransparencyPortal.Api.Data;
using TransparencyPortal.Api.Models;

namespace TransparencyPortal.Api.Controllers;

[ApiController]
[Route("api/inquiries")]
public class InquiriesController : ControllerBase
{
    private readonly AppDbContext _db;

    public InquiriesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<ActionResult<CreateInquiryResponse>> Create(
        [FromBody] CreateInquiryRequest body,
        CancellationToken cancellationToken)
    {
        var question = await _db.DefaultQuestions
            .AsNoTracking()
            .FirstOrDefaultAsync(q => q.Id == body.DefaultQuestionId && q.IsActive, cancellationToken);
        if (question is null)
            return BadRequest(new { error = "Unknown or inactive default question." });

        var preset = await _db.PresetResponses
            .AsNoTracking()
            .FirstOrDefaultAsync(
                p => p.Id == body.PresetResponseId && p.DefaultQuestionId == body.DefaultQuestionId,
                cancellationToken);
        if (preset is null)
            return BadRequest(new { error = "Preset response does not match the selected question." });

        var elaboration = string.IsNullOrWhiteSpace(body.ElaborationText) ? null : body.ElaborationText.Trim();
        var custom = string.IsNullOrWhiteSpace(body.CustomQuestionText) ? null : body.CustomQuestionText.Trim();

        var pathType = ResolvePathType(elaboration, custom);
        var id = Guid.NewGuid().ToString("D");
        var createdAt = DateTime.UtcNow;

        var inquiry = new Inquiry
        {
            Id = id,
            PathType = pathType,
            DefaultQuestionId = body.DefaultQuestionId,
            ElaborationText = elaboration,
            CustomQuestionText = custom,
            PresetResponseId = body.PresetResponseId,
            FirstResponseAt = null,
            CreatedAt = createdAt,
        };
        _db.Inquiries.Add(inquiry);
        await _db.SaveChangesAsync(cancellationToken);

        return Ok(new CreateInquiryResponse(id, pathType, createdAt));
    }

    private static string ResolvePathType(string? elaboration, string? custom)
    {
        var hasE = !string.IsNullOrEmpty(elaboration);
        var hasC = !string.IsNullOrEmpty(custom);
        if (hasE && hasC)
            return InquiryPathTypes.PresetFull;
        if (hasE)
            return InquiryPathTypes.PresetElab;
        if (hasC)
            return InquiryPathTypes.PresetCust;
        return InquiryPathTypes.Preset;
    }
}
