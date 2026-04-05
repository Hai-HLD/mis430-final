namespace TransparencyPortal.Api.Contracts;

public record DefaultQuestionListItemDto(
    ulong Id,
    string Slug,
    string Title,
    int DisplayOrder);

public record PresetResponseDto(ulong Id, int Version, string BodyText, DateTime PublishedAt);

public record DefaultQuestionDetailDto(
    ulong Id,
    string Slug,
    string Title,
    PresetResponseDto Preset);

public record CreateInquiryRequest(
    ulong DefaultQuestionId,
    ulong PresetResponseId,
    string? ElaborationText,
    string? CustomQuestionText);

public record CreateInquiryResponse(string Id, string PathType, DateTime CreatedAt);

public record StaffInquiryListItemDto(
    string Id,
    string PathType,
    ulong? DefaultQuestionId,
    string? DefaultQuestionTitle,
    DateTime CreatedAt);

public record StaffInquiryDetailDto(
    string Id,
    string PathType,
    ulong? DefaultQuestionId,
    string? DefaultQuestionSlug,
    string? DefaultQuestionTitle,
    ulong? PresetResponseId,
    int? PresetVersion,
    string? PresetBodyText,
    string? ElaborationText,
    string? CustomQuestionText,
    DateTime? FirstResponseAt,
    DateTime CreatedAt);

public record StaffLoginRequest(string Password);
