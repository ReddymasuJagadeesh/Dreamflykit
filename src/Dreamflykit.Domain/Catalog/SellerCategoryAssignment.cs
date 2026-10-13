namespace Dreamflykit.Domain.Catalog;

public sealed class SellerCategoryAssignment
{
    public Guid Id { get; private set; }
    public Guid SellerId { get; private set; }
    public Guid CategoryId { get; private set; }
    public DateTime AssignedAtUtc { get; private set; }

    private SellerCategoryAssignment() { }

    public SellerCategoryAssignment(Guid sellerId, Guid categoryId)
    {
        Id = Guid.NewGuid();
        SellerId = sellerId;
        CategoryId = categoryId;
        AssignedAtUtc = DateTime.UtcNow;
    }
}
