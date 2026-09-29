using Dreamflykit.Application.Kits.Models;

namespace Dreamflykit.Application.Kits.Interfaces;

public interface IFindMyKitService
{
    Task<FindMyKitResult> FindAsync(
        FindMyKitRequest request,
        CancellationToken cancellationToken);
}