using Dreamflykit.Application.AppliancesHub.Models;

namespace Dreamflykit.Application.AppliancesHub.Interfaces;

public interface IAppliancesHubService
{
    Task<AppliancesHubDto> GetConfigAsync(CancellationToken cancellationToken = default);
    Task UpdateConfigAsync(AppliancesHubDto dto, Guid? updatedBy, CancellationToken cancellationToken = default);
}
