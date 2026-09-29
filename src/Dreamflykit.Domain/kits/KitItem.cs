using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Kits;

public sealed class KitItem : AuditableEntity
{
    private KitItem()
    {
    }

    public KitItem(
        Guid kitId,
        Guid productVariantId,
        int quantity,
        int displayOrder = 0)
    {
        if (kitId == Guid.Empty)
            throw new DomainException(
                "Kit ID is required.");

        if (productVariantId == Guid.Empty)
            throw new DomainException(
                "Product variant ID is required.");

        if (quantity <= 0)
            throw new DomainException(
                "Kit item quantity must be greater than zero.");

        if (displayOrder < 0)
            throw new DomainException(
                "Kit item display order cannot be negative.");

        KitId = kitId;
        ProductVariantId = productVariantId;
        Quantity = quantity;
        DisplayOrder = displayOrder;
    }

    public Guid KitId { get; private set; }

    public Guid ProductVariantId { get; private set; }

    public int Quantity { get; private set; }

    public int DisplayOrder { get; private set; }

    public void Update(
        int quantity,
        int displayOrder)
    {
        if (quantity <= 0)
            throw new DomainException(
                "Kit item quantity must be greater than zero.");

        if (displayOrder < 0)
            throw new DomainException(
                "Kit item display order cannot be negative.");

        Quantity = quantity;
        DisplayOrder = displayOrder;

        MarkUpdated();
    }
}