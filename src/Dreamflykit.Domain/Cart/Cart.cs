using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Cart;

/// <summary>
/// Shopping cart aggregate root. One cart per authenticated user.
/// Business rules are enforced here — no controller may bypass them.
/// </summary>
public sealed class Cart : AuditableEntity
{
    private readonly List<CartItem> _items = new();

    private Cart()
    {
    }

    public Cart(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            throw new DomainException("User ID is required to create a cart.");
        }

        UserId = userId;
    }

    // --------------------------------------------------------
    // Properties
    // --------------------------------------------------------

    public Guid UserId { get; private set; }

    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

    public int TotalItems => _items.Sum(item => item.Quantity);

    public decimal Subtotal => _items.Sum(item => item.LineTotal);

    public decimal TotalMrp => _items.Sum(item => item.LineMrp);

    public decimal TotalDiscount => TotalMrp - Subtotal;

    // --------------------------------------------------------
    // Domain operations
    // --------------------------------------------------------

    /// <summary>
    /// Adds a product variant to the cart. If the variant is already
    /// present, increases the quantity by the given amount.
    /// </summary>
    public void AddItem(
        Guid productVariantId,
        string productName,
        string variantName,
        string sku,
        string? imageUrl,
        decimal unitPrice,
        decimal mrp,
        int quantity = 1)
    {
        ValidateVariantId(productVariantId);
        ValidatePrices(unitPrice, mrp);
        ValidateQuantityValue(quantity);

        if (_items.Count >= 50)
        {
            throw new DomainException(
                "Cart cannot have more than 50 distinct items.");
        }

        var existing = _items
            .FirstOrDefault(item =>
                item.ProductVariantId == productVariantId);

        if (existing is not null)
        {
            existing.IncreaseQuantity(quantity);
        }
        else
        {
            _items.Add(
                new CartItem(
                    Id,
                    productVariantId,
                    productName,
                    variantName,
                    sku,
                    imageUrl,
                    unitPrice,
                    mrp,
                    quantity));
        }

        MarkUpdated();
    }

    /// <summary>
    /// Sets the quantity of a specific cart line.
    /// Passing quantity = 0 removes the item.
    /// </summary>
    public void UpdateItemQuantity(
        Guid productVariantId,
        int quantity)
    {
        ValidateVariantId(productVariantId);

        if (quantity < 0)
        {
            throw new DomainException(
                "Quantity cannot be negative.");
        }

        var item = FindItem(productVariantId);

        if (quantity == 0)
        {
            _items.Remove(item);
        }
        else
        {
            item.SetQuantity(quantity);
        }

        MarkUpdated();
    }

    /// <summary>
    /// Removes a specific variant from the cart entirely.
    /// </summary>
    public void RemoveItem(Guid productVariantId)
    {
        ValidateVariantId(productVariantId);

        var item = FindItem(productVariantId);

        _items.Remove(item);

        MarkUpdated();
    }

    /// <summary>
    /// Empties the cart completely.
    /// </summary>
    public void Clear()
    {
        _items.Clear();
        MarkUpdated();
    }

    // --------------------------------------------------------
    // Private helpers
    // --------------------------------------------------------

    private CartItem FindItem(Guid productVariantId)
    {
        var item = _items
            .FirstOrDefault(i =>
                i.ProductVariantId == productVariantId);

        if (item is null)
        {
            throw new DomainException(
                "The specified item is not in the cart.");
        }

        return item;
    }

    private static void ValidateVariantId(Guid productVariantId)
    {
        if (productVariantId == Guid.Empty)
        {
            throw new DomainException(
                "Product variant ID is required.");
        }
    }

    private static void ValidatePrices(
        decimal unitPrice,
        decimal mrp)
    {
        if (unitPrice <= 0)
        {
            throw new DomainException(
                "Unit price must be greater than zero.");
        }

        if (mrp <= 0)
        {
            throw new DomainException(
                "MRP must be greater than zero.");
        }

        if (unitPrice > mrp)
        {
            throw new DomainException(
                "Unit price cannot be greater than MRP.");
        }
    }

    private static void ValidateQuantityValue(int quantity)
    {
        if (quantity is < 1 or > 99)
        {
            throw new DomainException(
                "Quantity must be between 1 and 99.");
        }
    }
}
