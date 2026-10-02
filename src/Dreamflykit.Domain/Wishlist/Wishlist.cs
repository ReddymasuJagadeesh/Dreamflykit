using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Wishlist;

public sealed class Wishlist : AuditableEntity
{
    private readonly List<WishlistItem> _items = new();

    private Wishlist()
    {
    }

    public Wishlist(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new DomainException("User ID is required to create a wishlist.");

        UserId = userId;
    }

    public Guid UserId { get; private set; }

    public IReadOnlyList<WishlistItem> Items => _items.AsReadOnly();

    public int TotalItems => _items.Count;

    public bool ContainsItem(Guid productVariantId) =>
        _items.Any(i => i.ProductVariantId == productVariantId);

    public void AddItem(
        Guid productVariantId,
        string productName,
        string variantName,
        string sku,
        string? imageUrl,
        decimal unitPrice,
        decimal mrp)
    {
        if (ContainsItem(productVariantId))
            return; // Idempotent: already in wishlist

        if (_items.Count >= 100)
            throw new DomainException("Wishlist cannot hold more than 100 items.");

        _items.Add(new WishlistItem(
            Id,
            productVariantId,
            productName,
            variantName,
            sku,
            imageUrl,
            unitPrice,
            mrp));

        MarkUpdated();
    }

    public void RemoveItem(Guid productVariantId)
    {
        var existing = _items.FirstOrDefault(i => i.ProductVariantId == productVariantId);
        if (existing is not null)
        {
            _items.Remove(existing);
            MarkUpdated();
        }
    }

    public void Clear()
    {
        _items.Clear();
        MarkUpdated();
    }
}
