using Dreamflykit.Application.StudyHub.Models;

namespace Dreamflykit.Tests;

public class StudyHubAndCatalogTests
{
    [Fact]
    public void StudyHubDto_CreateDefault_ConfiguresAllEightStudySubcategories()
    {
        // Act
        var hub = StudyHubDto.CreateDefault();

        // Assert
        Assert.NotNull(hub);
        Assert.NotNull(hub.CategoriesPage);
        Assert.NotNull(hub.CategoriesPage.Categories);
        Assert.Equal(8, hub.CategoriesPage.Categories.Count);

        var expectedSlugs = new[]
        {
            "stationery-study-supplies",
            "tech-for-study",
            "document-organization",
            "desk-room-study-essential",
            "college-campus-essentials",
            "online-classes-productivity",
            "exam-preparation",
            "books-references"
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
        }
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
}
