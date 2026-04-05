namespace TransparencyPortal.Api.Models;

public class PresetResponse
{
    public ulong Id { get; set; }
    public ulong DefaultQuestionId { get; set; }
    public string BodyText { get; set; } = string.Empty;
    public int Version { get; set; }
    public DateTime PublishedAt { get; set; }

    public DefaultQuestion DefaultQuestion { get; set; } = null!;
    public ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();
}
