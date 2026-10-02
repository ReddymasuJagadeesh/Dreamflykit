using System.ComponentModel.DataAnnotations;

namespace Dreamflykit.Web.Models.Cart;

/// <summary>
/// Full cart page view model.
/// </summary>
public sealed class CartViewModel
{
    public bool IsEmpty { get; init; }

    public int TotalItems { get; init; }

    public decimal Subtotal { get; init; }

    public decimal TotalMrp { get; init; }

    public decimal TotalDiscount { get; init; }

    public IReadOnlyList<CartItemViewModel> Items { get; init; }
        = Array.Empty<CartItemViewModel>();
}

/// <summary>
/// Single cart line view model.
/// </summary>
public sealed class CartItemViewModel
{
    public Guid ProductVariantId { get; init; }

    public string ProductName { get; init; } = string.Empty;

    public string VariantName { get; init; } = string.Empty;

    public string Sku { get; init; } = string.Empty;

    public string? ImageUrl { get; init; }

    public decimal UnitPrice { get; init; }

    public decimal Mrp { get; init; }

    public int Quantity { get; init; }

    public decimal LineTotal { get; init; }

    public decimal LineMrp { get; init; }

    public decimal LineDiscount { get; init; }

    public decimal DiscountPercentage { get; init; }
}

/// <summary>
/// Form model for the Add to Cart POST.
/// </summary>
public sealed class AddToCartFormModel
{
    [Required]
    public Guid ProductVariantId { get; set; }

    [Range(1, 99)]
    public int Quantity { get; set; } = 1;

    /// <summary>Safe return URL — validated with Url.IsLocalUrl before use.</summary>
    public string? ReturnUrl { get; set; }
}

/// <summary>
/// Form model for Update Quantity POST.
/// </summary>
public sealed class UpdateCartItemFormModel
{
    [Required]
    public Guid ProductVariantId { get; set; }

    [Range(0, 99)]
    public int Quantity { get; set; }
}

/// <summary>
/// Form model for Remove Item POST.
/// </summary>
public sealed class RemoveCartItemFormModel
{
    [Required]
    public Guid ProductVariantId { get; set; }
}
