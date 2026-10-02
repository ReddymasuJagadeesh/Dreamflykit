using Dreamflykit.Application.Addressing.Interfaces;
using Dreamflykit.Application.Addressing.Models;
using Dreamflykit.Domain.Addressing;

namespace Dreamflykit.Application.Addressing.Services;

public sealed class AddressService : IAddressService
{
    private readonly IAddressRepository _repository;

    public AddressService(IAddressRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<AddressSummary>> GetAddressesAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var addresses = await _repository.GetByUserIdAsync(userId, cancellationToken);
        return addresses.Select(Map).ToList();
    }

    public async Task<AddressSummary?> GetAddressAsync(
        Guid addressId,
        Guid userId,
        CancellationToken cancellationToken)
    {
        var address = await _repository.GetByIdAsync(addressId, userId, cancellationToken);
        return address is null ? null : Map(address);
    }

    public async Task<Guid> AddAddressAsync(
        Guid userId,
        SaveAddressCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var address = new CustomerAddress(
            userId,
            command.FullName,
            command.Phone,
            command.AddressLine1,
            command.AddressLine2,
            command.City,
            command.State,
            command.PostalCode,
            command.Country,
            command.AddressType);

        if (command.SetAsDefault)
        {
            // Clear any existing default first (single-default-per-type invariant)
            await _repository.ClearDefaultAsync(userId, command.AddressType, cancellationToken);
            address.SetAsDefault();
        }

        await _repository.AddAsync(address, cancellationToken);
        return address.Id;
    }

    public async Task UpdateAddressAsync(
        Guid addressId,
        Guid userId,
        SaveAddressCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var address = await GetRequiredAddressAsync(addressId, userId, cancellationToken);

        address.Update(
            command.FullName,
            command.Phone,
            command.AddressLine1,
            command.AddressLine2,
            command.City,
            command.State,
            command.PostalCode,
            command.Country);

        if (command.SetAsDefault && !address.IsDefault)
        {
            await _repository.ClearDefaultAsync(userId, address.AddressType, cancellationToken);
            address.SetAsDefault();
        }

        await _repository.UpdateAsync(address, cancellationToken);
    }

    public async Task DeleteAddressAsync(
        Guid addressId,
        Guid userId,
        CancellationToken cancellationToken)
    {
        var address = await GetRequiredAddressAsync(addressId, userId, cancellationToken);
        await _repository.DeleteAsync(address, cancellationToken);
    }

    public async Task SetDefaultAsync(
        Guid addressId,
        Guid userId,
        CancellationToken cancellationToken)
    {
        var address = await GetRequiredAddressAsync(addressId, userId, cancellationToken);

        await _repository.ClearDefaultAsync(userId, address.AddressType, cancellationToken);
        address.SetAsDefault();
        await _repository.UpdateAsync(address, cancellationToken);
    }

    // ----------------------------------------------------------
    // Private helpers
    // ----------------------------------------------------------

    private async Task<CustomerAddress> GetRequiredAddressAsync(
        Guid addressId,
        Guid userId,
        CancellationToken cancellationToken)
    {
        var address = await _repository.GetByIdAsync(addressId, userId, cancellationToken);

        // Return 404-equivalent — do not reveal whether the address exists
        // for a different user (prevents IDOR enumeration)
        if (address is null)
            throw new InvalidOperationException("Address not found.");

        return address;
    }

    private static AddressSummary Map(CustomerAddress a) => new()
    {
        AddressId = a.Id,
        FullName = a.FullName,
        Phone = a.Phone,
        AddressLine1 = a.AddressLine1,
        AddressLine2 = a.AddressLine2,
        City = a.City,
        State = a.State,
        PostalCode = a.PostalCode,
        Country = a.Country,
        AddressType = a.AddressType,
        IsDefault = a.IsDefault,
        DisplayString = a.ToDisplayString()
    };
}
