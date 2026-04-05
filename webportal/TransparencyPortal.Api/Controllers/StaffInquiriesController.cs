using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransparencyPortal.Api.Contracts;
using TransparencyPortal.Api.Data;

namespace TransparencyPortal.Api.Controllers;

[ApiController]
[Route("api/staff/inquiries")]
[Authorize(AuthenticationSchemes = StaffAuthController.StaffScheme, Roles = "Staff")]
public class StaffInquiriesController : ControllerBase
{
    private readonly AppDbContext _db;

    public StaffInquiriesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<StaffInquiryListItemDto>>> List(
        [FromQuery] int take = 100,
        CancellationToken cancellationToken = default)
    {
        take = Math.Clamp(take, 1, 500);
        var list = await _db.Inquiries
            .AsNoTracking()
            .OrderByDescending(i => i.CreatedAt)
            .Take(take)
            .Select(i => new StaffInquiryListItemDto(
                i.Id,
                i.PathType,
                i.DefaultQuestionId,
                i.DefaultQuestion != null ? i.DefaultQuestion.Title : null,
                i.CreatedAt))
            .ToListAsync(cancellationToken);

        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StaffInquiryDetailDto>> Get(string id, CancellationToken cancellationToken)
    {
        var row = await _db.Inquiries
            .AsNoTracking()
            .Where(i => i.Id == id)
            .Select(i => new StaffInquiryDetailDto(
                i.Id,
                i.PathType,
                i.DefaultQuestionId,
                i.DefaultQuestion != null ? i.DefaultQuestion.Slug : null,
                i.DefaultQuestion != null ? i.DefaultQuestion.Title : null,
                i.PresetResponseId,
                i.PresetResponse != null ? i.PresetResponse.Version : null,
                i.PresetResponse != null ? i.PresetResponse.BodyText : null,
                i.ElaborationText,
                i.CustomQuestionText,
                i.FirstResponseAt,
                i.CreatedAt))
            .FirstOrDefaultAsync(cancellationToken);

        return row is null ? NotFound() : Ok(row);
    }
}
