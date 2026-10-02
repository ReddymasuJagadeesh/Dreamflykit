namespace Dreamflykit.Domain.Media;

/// <summary>
/// Controlled enumeration of all media categories in the system.
/// Never use arbitrary strings — always use this enum for media classification.
/// </summary>
public enum MediaType
{
    Product = 1,
    Category = 2,
    Subcategory = 3,
    Homepage = 4,
    LandingPage = 5,
    Banner = 6,
    Brand = 7,
    Campaign = 8,
    Promotion = 9,
    Other = 10
}
