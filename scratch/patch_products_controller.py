import os

path = r'src/Dreamflykit.Web/Controllers/ProductsController.cs'
with open(path, 'r', encoding='utf-8') as f:
    content = f.read()

target = """            var subPage = elecHubConfig.SubcategoryPages?.FirstOrDefault(p =>

                string.Equals(p.CategorySlug, currentSubcategorySlug, StringComparison.OrdinalIgnoreCase) ||

                (currentSubcategorySlug.Contains("laptop") && p.CategorySlug.Contains("laptop")) ||

                (currentSubcategorySlug.Contains("headphone") && p.CategorySlug.Contains("headphone")) ||

                (currentSubcategorySlug.Contains("adapter") && p.CategorySlug.Contains("adapter")) ||

                (currentSubcategorySlug.Contains("power") && p.CategorySlug.Contains("power")) ||

                (currentSubcategorySlug.Contains("sim") && p.CategorySlug.Contains("sim")) ||

                (currentSubcategorySlug.Contains("storage") && p.CategorySlug.Contains("storage")) ||

                (currentSubcategorySlug.Contains("watch") && p.CategorySlug.Contains("watch")) ||

                (currentSubcategorySlug.Contains("cable") && p.CategorySlug.Contains("cable")) ||

                (currentSubcategorySlug.Contains("monitor") && p.CategorySlug.Contains("monitor")) ||

                (currentSubcategorySlug.Contains("calculator") && p.CategorySlug.Contains("calculator")));"""

replacement = """            var subPage = elecHubConfig.SubcategoryPages?.FirstOrDefault(p =>
                string.Equals(p.CategorySlug, currentSubcategorySlug, StringComparison.OrdinalIgnoreCase) ||
                (currentSubcategorySlug.Contains("laptop") && p.CategorySlug.Contains("laptop")) ||
                ((currentSubcategorySlug.Contains("earphone") || currentSubcategorySlug.Contains("headphone") || currentSubcategorySlug.Contains("audio")) && (p.CategorySlug.Contains("earphone") || p.CategorySlug.Contains("headphone"))) ||
                (currentSubcategorySlug.Contains("groom") && p.CategorySlug.Contains("groom")) ||
                ((currentSubcategorySlug.Contains("case") || currentSubcategorySlug.Contains("cover")) && p.CategorySlug.Contains("case")) ||
                ((currentSubcategorySlug.Contains("storage") || currentSubcategorySlug.Contains("ssd")) && p.CategorySlug.Contains("storage")) ||
                ((currentSubcategorySlug.Contains("charger") || currentSubcategorySlug.Contains("cable") || currentSubcategorySlug.Contains("adapter")) && (p.CategorySlug.Contains("charger") || p.CategorySlug.Contains("cable"))) ||
                (currentSubcategorySlug.Contains("power") && p.CategorySlug.Contains("power")) ||
                (currentSubcategorySlug.Contains("watch") && p.CategorySlug.Contains("watch")) ||
                ((currentSubcategorySlug.Contains("accessory") || currentSubcategorySlug.Contains("accessories") || currentSubcategorySlug.Contains("mouse") || currentSubcategorySlug.Contains("keyboard")) && p.CategorySlug.Contains("access")) ||
                (currentSubcategorySlug.Contains("speaker") && p.CategorySlug.Contains("speaker")));"""

if target in content:
    content = content.replace(target, replacement, 1)
    with open(path, 'w', encoding='utf-8') as f:
        f.write(content)
    print("Updated ProductsController.cs successfully!")
else:
    # Try normalizing line breaks
    content_norm = content.replace('\r\n', '\n')
    target_norm = target.replace('\r\n', '\n')
    replacement_norm = replacement.replace('\r\n', '\n')
    if target_norm in content_norm:
        content_norm = content_norm.replace(target_norm, replacement_norm, 1)
        with open(path, 'w', encoding='utf-8') as f:
            f.write(content_norm)
        print("Updated ProductsController.cs with normalized LF successfully!")
    else:
        print("Target not found in ProductsController.cs!")

