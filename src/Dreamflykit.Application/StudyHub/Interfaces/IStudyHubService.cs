using Dreamflykit.Application.StudyHub.Models;

namespace Dreamflykit.Application.StudyHub.Interfaces;

public interface IStudyHubService
{
    Task<StudyHubDto> GetConfigAsync(CancellationToken cancellationToken = default);
    Task UpdateConfigAsync(StudyHubDto dto, Guid updatedBy, CancellationToken cancellationToken = default);
}
