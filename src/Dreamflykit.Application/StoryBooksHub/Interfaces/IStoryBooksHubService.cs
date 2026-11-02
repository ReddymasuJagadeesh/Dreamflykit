using Dreamflykit.Application.StoryBooksHub.Models;

namespace Dreamflykit.Application.StoryBooksHub.Interfaces;

public interface IStoryBooksHubService
{
    Task<StoryBooksHubDto> GetConfigAsync(CancellationToken cancellationToken = default);
    Task UpdateConfigAsync(StoryBooksHubDto config, Guid? updatedBy, CancellationToken cancellationToken = default);
    Task ResetToDefaultAsync(CancellationToken cancellationToken = default);
}

