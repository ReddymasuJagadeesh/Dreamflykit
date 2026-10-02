using Dreamflykit.Application.Addressing.Interfaces;
using Dreamflykit.Domain.Addressing;
using Dreamflykit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Dreamflykit.Infrastructure.Addressing;

public sealed class AddressRepository : IAddressRepository
{
    private readonly DreamflykitDbContext _dbContext;

    public AddressRepository(DreamflykitDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<CustomerAddress>> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.CustomerAddresses
            .AsNoTracking()
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.IsDefault)
            .ThenByDescending(a => a.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<CustomerAddress?> GetByIdAsync(
        Guid addressId,
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.CustomerAddresses
            .FirstOrDefaultAsync(a => a.Id == addressId && a.UserId == userId, cancellationToken);
    }

    public async Task<CustomerAddress?> GetDefaultAsync(
        Guid userId,
        AddressType addressType,
        CancellationToken cancellationToken)
    {
        return await _dbContext.CustomerAddresses
            .FirstOrDefaultAsync(a => a.UserId == userId && a.AddressType == addressType && a.IsDefault, cancellationToken);
    }

    public async Task AddAsync(
        CustomerAddress address,
        CancellationToken cancellationToken)
    {
        await _dbContext.CustomerAddresses.AddAsync(address, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        CustomerAddress address,
        CancellationToken cancellationToken)
    {
        _dbContext.CustomerAddresses.Update(address);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        CustomerAddress address,
        CancellationToken cancellationToken)
    {
        _dbContext.CustomerAddresses.Remove(address);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task ClearDefaultAsync(
        Guid userId,
        AddressType addressType,
        CancellationToken cancellationToken)
    {
        var existingDefaults = await _dbContext.CustomerAddresses
            .Where(a => a.UserId == userId && a.AddressType == addressType && a.IsDefault)
            .ToListAsync(cancellationToken);

        foreach (var addr in existingDefaults)
        {
            addr.ClearDefault();
        }

        if (existingDefaults.Count > 0)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
