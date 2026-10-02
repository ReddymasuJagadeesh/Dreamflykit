using Dreamflykit.Domain.Addressing;

namespace Dreamflykit.Application.Addressing.Interfaces;

public interface IAddressRepository
{
    Task<IReadOnlyList<CustomerAddress>> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken);

    Task<CustomerAddress?> GetByIdAsync(
        Guid addressId,
        Guid userId,            // Always scope to user — prevent IDOR
        CancellationToken cancellationToken);

    Task<CustomerAddress?> GetDefaultAsync(
        Guid userId,
        AddressType addressType,
        CancellationToken cancellationToken);

    Task AddAsync(
        CustomerAddress address,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        CustomerAddress address,
        CancellationToken cancellationToken);

    Task DeleteAsync(
        CustomerAddress address,
        CancellationToken cancellationToken);

    /// <summary>
    /// Clears IsDefault=true on all addresses of the given type for this user.
    /// Called before setting a new default to enforce single-default constraint.
    /// </summary>
    Task ClearDefaultAsync(
        Guid userId,
        AddressType addressType,
        CancellationToken cancellationToken);
}
