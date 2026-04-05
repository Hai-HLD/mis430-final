using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransparencyPortal.Api.Contracts;
using TransparencyPortal.Api.Data;

namespace TransparencyPortal.Api.Controllers;

[ApiController]
[Route("api/default-questions")]
public class DefaultQuestionsController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IWebHostEnvironment _env;

    public DefaultQuestionsController(AppDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    [HttpGet]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        try
        {
            var items = await _db.DefaultQuestions
                .AsNoTracking()
                .Where(q => q.IsActive)
                .OrderBy(q => q.DisplayOrder)
                .ThenBy(q => q.Id)
                .Select(q => new DefaultQuestionListItemDto(q.Id, q.Slug, q.Title, q.DisplayOrder))
                .ToListAsync(cancellationToken);
            return Ok(items);
        }
        catch (Exception ex)
        {
            if (_env.IsDevelopment())
            {
                return Problem(
                    title: "Failed to load default questions",
                    detail: ex.ToString(),
                    statusCode: StatusCodes.Status500InternalServerError);
            }

            throw;
        }
    }

    [HttpGet("{id:ulong}")]
    public async Task<IActionResult> Get(ulong id, CancellationToken cancellationToken)
    {
        try
        {
            var q = await _db.DefaultQuestions
                .AsNoTracking()
                .Where(x => x.Id == id && x.IsActive)
                .Select(x => new { x.Id, x.Slug, x.Title })
                .FirstOrDefaultAsync(cancellationToken);
            if (q is null)
                return NotFound();

            var preset = await _db.PresetResponses
                .AsNoTracking()
                .Where(p => p.DefaultQuestionId == id)
                .OrderByDescending(p => p.Version)
                .ThenByDescending(p => p.Id)
                .Select(p => new PresetResponseDto(p.Id, p.Version, p.BodyText, p.PublishedAt))
                .FirstOrDefaultAsync(cancellationToken);
            if (preset is null)
                return NotFound();

            return Ok(new DefaultQuestionDetailDto(q.Id, q.Slug, q.Title, preset));
        }
        catch (Exception ex)
        {
            if (_env.IsDevelopment())
            {
                return Problem(
                    title: "Failed to load question detail",
                    detail: ex.ToString(),
                    statusCode: StatusCodes.Status500InternalServerError);
            }

            throw;
        }
    }
}
