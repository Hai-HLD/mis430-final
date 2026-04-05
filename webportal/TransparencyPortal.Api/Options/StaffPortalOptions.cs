namespace TransparencyPortal.Api.Options;

public class StaffPortalOptions
{
    public const string SectionName = "StaffPortal";

    /// <summary>Prototype-only shared password for staff sign-in.</summary>
    public string Password { get; set; } = "ChangeMe!";
}
