namespace TransparencyPortal.Api.Models;

public class Inquiry
{
    public string Id { get; set; } = string.Empty;
    public string PathType { get; set; } = string.Empty;
    public ulong? DefaultQuestionId { get; set; }
    public string? ElaborationText { get; set; }
    public string? CustomQuestionText { get; set; }
    public ulong? PresetResponseId { get; set; }
    public DateTime? FirstResponseAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public DefaultQuestion? DefaultQuestion { get; set; }
    public PresetResponse? PresetResponse { get; set; }
}
