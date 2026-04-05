namespace TransparencyPortal.Api.Models;

public class DefaultQuestion
{
    public ulong Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<PresetResponse> PresetResponses { get; set; } = new List<PresetResponse>();
    public ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();
}
