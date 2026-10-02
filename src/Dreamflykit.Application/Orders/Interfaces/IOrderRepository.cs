using Dreamflykit.Domain.Orders;

namespace Dreamflykit.Application.Orders.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(
        Guid orderId,
        Guid userId,            // Always scope to user — prevent IDOR
        CancellationToken cancellationToken);

    Task<Order?> GetByOrderNumberAsync(
        string orderNumber,
        Guid userId,
        CancellationToken cancellationToken);

    Task<Order?> GetByOrderNumberAsync(
        string orderNumber,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Order>> GetByUserIdAsync(
        Guid userId,
        int page,
        int pageSize,
        CancellationToken cancellationToken);

    Task<int> CountByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken);

    Task AddAsync(
        Order order,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        Order order,
        CancellationToken cancellationToken);

    /// <summary>
    /// Generates the next sequential order number in the format DFK-YYYYMMDD-NNNN.
    /// Must be atomic — uses a DB sequence or advisory lock.
    /// </summary>
    Task<string> GenerateOrderNumberAsync(
        CancellationToken cancellationToken);
}
