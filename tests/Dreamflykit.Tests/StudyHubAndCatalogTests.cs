using Dreamflykit.Application.StudyHub.Models;

namespace Dreamflykit.Tests;

public class StudyHubAndCatalogTests
{
    [Fact]
    public void StudyHubDto_CreateDefault_ConfiguresAllSixStudyCategoriesWithSubcategories()
    {
        // Act
        var hub = StudyHubDto.CreateDefault();

        // Assert
        Assert.NotNull(hub);
        Assert.NotNull(hub.CategoriesPage);
        Assert.NotNull(hub.CategoriesPage.Categories);
        Assert.Equal(6, hub.CategoriesPage.Categories.Count);

        var expectedSlugs = new[]
        {
            "stationery-study-supplies",
            "tech-for-study",
            "document-organization",
            "desk-room-study-essential",
            "college-campus-essentials",
            "online-classes-productivity"
        };

        var actualSlugs = hub.CategoriesPage.Categories.Select(c => c.Slug).ToList();
        foreach (var slug in expectedSlugs)
        {
            Assert.Contains(slug, actualSlugs);
        }

        foreach (var cat in hub.CategoriesPage.Categories)
        {
            Assert.False(string.IsNullOrWhiteSpace(cat.Name), "Category name must not be empty.");
            Assert.False(string.IsNullOrWhiteSpace(cat.Slug), "Category slug must not be empty.");
            Assert.False(string.IsNullOrWhiteSpace(cat.ImageUrl), "Category image URL must not be empty.");
            Assert.False(string.IsNullOrWhiteSpace(cat.Description), "Category description must not be empty.");
            Assert.NotNull(cat.Subcategories);
            Assert.NotEmpty(cat.Subcategories);
        }

        // Verify document-organization important note
        var docCat = hub.CategoriesPage.Categories.First(c => c.Slug == "document-organization");
        Assert.False(string.IsNullOrWhiteSpace(docCat.ImportantNote));
    }

    [Fact]
    public void StudyHubDto_Sections_ArePopulatedWithContent()
    {
        // Act
        var hub = StudyHubDto.CreateDefault();

        // Assert
        Assert.NotNull(hub.HeroBanner);
        Assert.False(string.IsNullOrWhiteSpace(hub.HeroBanner.Title));
        Assert.True(hub.HeroBanner.AutoSlideIntervalSeconds >= 2);
        Assert.NotNull(hub.HeroBanner.Slides);
        Assert.NotEmpty(hub.HeroBanner.Slides);
        Assert.All(hub.HeroBanner.Slides, s => Assert.False(string.IsNullOrWhiteSpace(s.ImageUrl)));
        Assert.NotNull(hub.Section1);
        Assert.NotEmpty(hub.Section1.Cards);
        Assert.NotNull(hub.Section2);
        Assert.NotEmpty(hub.Section2.Kits);
        Assert.NotNull(hub.Section4);
        Assert.NotEmpty(hub.Section4.Items);
        Assert.NotNull(hub.Section10);
        Assert.NotEmpty(hub.Section10.Faqs);
    }

    [Fact]
    public void StudyHubDto_Section1Cards_HaveCleanLinksWithoutDuplicateSegments()
    {
        // Act
        var hub = StudyHubDto.CreateDefault();

        // Assert
        Assert.NotNull(hub.Section1);
        Assert.NotEmpty(hub.Section1.Cards);

        foreach (var card in hub.Section1.Cards)
        {
            Assert.False(string.IsNullOrWhiteSpace(card.LinkUrl));
            Assert.DoesNotContain("//", card.LinkUrl);
            Assert.DoesNotContain("/category/study-essentials/category/study-essentials/", card.LinkUrl);
            Assert.StartsWith("/category/study-essentials/", card.LinkUrl);
        }
    }

    [Fact]
    public void StudyHubDto_Subcategories_ContainAllUserSpecifiedItems()
    {
        // Act
        var hub = StudyHubDto.CreateDefault();
        var categories = hub.CategoriesPage.Categories;

        // 1. Stationery & Study Supplies
        var stationery = categories.First(c => c.Slug == "stationery-study-supplies");
        Assert.Contains(stationery.Subcategories, s => s.Contains("Notebooks"));
        Assert.Contains(stationery.Subcategories, s => s.Contains("calculator"));

        // 2. Tech for study
        var tech = categories.First(c => c.Slug == "tech-for-study");
        Assert.Contains(tech.Subcategories, s => s.Contains("Laptop"));
        Assert.Contains(tech.Subcategories, s => s.Contains("travel adapter"));

        // 3. Documents & Organization
        var docs = categories.First(c => c.Slug == "document-organization");
        Assert.Contains(docs.Subcategories, s => s.Contains("Passport holder"));
        Assert.Contains("Keep original passports and certificates with the student; don't pack them in checked luggage.", docs.ImportantNote);

        // 4. Desk & Room Study Essentials
        var desk = categories.First(c => c.Slug == "desk-room-study-essential");
        Assert.Contains(desk.Subcategories, s => s.Contains("desk lamp") || s.Contains("Desk"));

        // 5. College & Campus Essentials
        var campus = categories.First(c => c.Slug == "college-campus-essentials");
        Assert.Contains(campus.Subcategories, s => s.Contains("backpack") || s.Contains("Backpack"));

        // 6. Online Classes & Productivity
        var online = categories.First(c => c.Slug == "online-classes-productivity");
        Assert.Contains(online.Subcategories, s => s.Contains("Webcam"));
        Assert.Contains(online.Subcategories, s => s.Contains("Microphone"));
    }
}
