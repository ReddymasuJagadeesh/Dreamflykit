namespace Dreamflykit.Application.Theme;

public interface IThemeSettingService
{
    Task<ThemeSettingsDto> GetThemeSettingsAsync(CancellationToken cancellationToken = default);

    Task<(bool Success, string? ErrorMessage)> UpdateThemeSettingsAsync(
        UpdateThemeSettingsCommand command,
        Guid updatedBy,
        CancellationToken cancellationToken = default);

    Task<string> RenderDynamicThemeHtmlAsync(CancellationToken cancellationToken = default);
}
