namespace Dreamflykit.Application.Logo;

public interface ILogoSettingService
{
    Task<LogoSettingsDto> GetLogoSettingsAsync(CancellationToken cancellationToken = default);

    Task<(bool Success, string? ErrorMessage)> UpdateLogoSettingsAsync(
        LogoSettingsDto dto,
        Guid updatedBy,
        CancellationToken cancellationToken = default);

    Task<(bool Success, string? FileUrl, string? ErrorMessage)> UploadLogoFileAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        bool isScrolledIcon,
        Guid updatedBy,
        CancellationToken cancellationToken = default);

    Task ResetToDefaultAsync(
        string logoType,
        Guid updatedBy,
        CancellationToken cancellationToken = default);
}
