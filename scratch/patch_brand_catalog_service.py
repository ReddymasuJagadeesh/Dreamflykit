import os

path = r'src/Dreamflykit.Application/ElectronicsHub/Services/ElectronicsBrandCatalogService.cs'
with open(path, 'r', encoding='utf-8') as f:
    content = f.read()

target = """        var subPage = allPages.FirstOrDefault(p =>
            string.Equals(p.CategorySlug, cleanSub, StringComparison.OrdinalIgnoreCase) ||
            (cleanSub.Contains("laptop") && p.CategorySlug.Contains("laptop")) ||
            (cleanSub.Contains("headphone") && p.CategorySlug.Contains("headphone")) ||
            (cleanSub.Contains("adapter") && p.CategorySlug.Contains("adapter")) ||
            (cleanSub.Contains("power") && p.CategorySlug.Contains("power")) ||
            (cleanSub.Contains("sim") && p.CategorySlug.Contains("sim")) ||
            (cleanSub.Contains("storage") && p.CategorySlug.Contains("storage")) ||
            (cleanSub.Contains("watch") && p.CategorySlug.Contains("watch")) ||
            (cleanSub.Contains("cable") && p.CategorySlug.Contains("cable")) ||
            (cleanSub.Contains("monitor") && p.CategorySlug.Contains("monitor")) ||
            (cleanSub.Contains("calculator") && p.CategorySlug.Contains("calculator")))
            ?? allPages.First();"""

replacement = """        var subPage = allPages.FirstOrDefault(p =>
            string.Equals(p.CategorySlug, cleanSub, StringComparison.OrdinalIgnoreCase) ||
            (cleanSub.Contains("laptop") && p.CategorySlug.Contains("laptop")) ||
            ((cleanSub.Contains("headphone") || cleanSub.Contains("earphone") || cleanSub.Contains("audio")) && (p.CategorySlug.Contains("earphone") || p.CategorySlug.Contains("headphone"))) ||
            (cleanSub.Contains("groom") && p.CategorySlug.Contains("groom")) ||
            ((cleanSub.Contains("case") || cleanSub.Contains("cover")) && p.CategorySlug.Contains("case")) ||
            ((cleanSub.Contains("storage") || cleanSub.Contains("ssd")) && p.CategorySlug.Contains("storage")) ||
            ((cleanSub.Contains("charger") || cleanSub.Contains("cable") || cleanSub.Contains("adapter")) && (p.CategorySlug.Contains("charger") || p.CategorySlug.Contains("cable"))) ||
            (cleanSub.Contains("power") && p.CategorySlug.Contains("power")) ||
            (cleanSub.Contains("watch") && p.CategorySlug.Contains("watch")) ||
            ((cleanSub.Contains("accessory") || cleanSub.Contains("accessories") || cleanSub.Contains("mouse") || cleanSub.Contains("keyboard")) && p.CategorySlug.Contains("access")) ||
            (cleanSub.Contains("speaker") && p.CategorySlug.Contains("speaker")))
            ?? allPages.First();"""

content_norm = content.replace('\r\n', '\n')
target_norm = target.replace('\r\n', '\n')
replacement_norm = replacement.replace('\r\n', '\n')

if target_norm in content_norm:
    content_norm = content_norm.replace(target_norm, replacement_norm, 1)
    with open(path, 'w', encoding='utf-8') as f:
        f.write(content_norm)
    print("Updated ElectronicsBrandCatalogService.cs successfully!")
else:
    print("Target not found in ElectronicsBrandCatalogService.cs!")

