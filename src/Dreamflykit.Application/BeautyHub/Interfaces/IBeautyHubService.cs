using Dreamflykit.Application.BeautyHub.Models;

namespace Dreamflykit.Application.BeautyHub.Interfaces;

public interface IBeautyHubService
{
    Task<BeautyHubDto> GetConfigAsync(CancellationToken cancellationToken = default);

    Task UpdateConfigAsync(
        BeautyHubDto dto,
        string? updatedBy = null,
        CancellationToken cancellationToken = default);
}

