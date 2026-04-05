using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TransparencyPortal.Api.Contracts;
using TransparencyPortal.Api.Options;

namespace TransparencyPortal.Api.Controllers;

[ApiController]
[Route("api/staff")]
public class StaffAuthController : ControllerBase
{
    public const string StaffScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    private readonly StaffPortalOptions _options;

    public StaffAuthController(IOptions<StaffPortalOptions> options)
    {
        _options = options.Value;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] StaffLoginRequest body, CancellationToken cancellationToken)
    {
        if (body.Password != _options.Password)
            return Unauthorized();

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "staff"),
            new Claim(ClaimTypes.Role, "Staff"),
        };
        var identity = new ClaimsIdentity(claims, StaffScheme);
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(
            StaffScheme,
            principal,
            new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true,
            });
        return NoContent();
    }

    [Authorize(AuthenticationSchemes = StaffScheme, Roles = "Staff")]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(StaffScheme);
        return NoContent();
    }

    [Authorize(AuthenticationSchemes = StaffScheme, Roles = "Staff")]
    [HttpGet("me")]
    public IActionResult Me() => Ok(new { role = "Staff" });
}
