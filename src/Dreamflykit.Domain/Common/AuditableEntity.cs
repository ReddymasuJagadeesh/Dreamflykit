namespace Dreamflykit.Domain.Common;

public abstract class AuditableEntity : Entity
{
    public DateTime CreatedAtUtc { get; private set; }

    public DateTime? UpdatedAtUtc { get; private set; }

    protected AuditableEntity()
        : base()
    {
        CreatedAtUtc = DateTime.UtcNow;
    }

    protected AuditableEntity(Guid id)
        : base(id)
    {
        CreatedAtUtc = DateTime.UtcNow;
    }

    public void MarkUpdated()
    {
        UpdatedAtUtc = DateTime.UtcNow;
    }
}