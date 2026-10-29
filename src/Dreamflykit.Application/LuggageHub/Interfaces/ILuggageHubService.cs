using Dreamflykit.Application.LuggageHub.Models;

namespace Dreamflykit.Application.LuggageHub.Interfaces;

public interface ILuggageHubService
{
    Task<LuggageHubDto> GetConfigAsync(CancellationToken cancellationToken = default);
    Task UpdateConfigAsync(LuggageHubDto dto, string? updatedBy = null, CancellationToken cancellationToken = default);
}

