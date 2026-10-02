using Dreamflykit.Application.Addressing.Models;
using Dreamflykit.Domain.Addressing;

namespace Dreamflykit.Application.Addressing.Interfaces;

public interface IAddressService
{
    Task<IReadOnlyList<AddressSummary>> GetAddressesAsync(
        Guid userId,
        CancellationToken cancellationToken);

    Task<AddressSummary?> GetAddressAsync(
        Guid addressId,
        Guid userId,
        CancellationToken cancellationToken);

    Task<Guid> AddAddressAsync(
        Guid userId,
        SaveAddressCommand command,
        CancellationToken cancellationToken);

    Task UpdateAddressAsync(
        Guid addressId,
        Guid userId,
        SaveAddressCommand command,
        CancellationToken cancellationToken);

    Task DeleteAddressAsync(
        Guid addressId,
        Guid userId,
        CancellationToken cancellationToken);

    Task SetDefaultAsync(
        Guid addressId,
        Guid userId,
        CancellationToken cancellationToken);
}
