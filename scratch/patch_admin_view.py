import os

path = r'src/Dreamflykit.Web/Areas/Admin/Views/ElectronicsHub/Index.cshtml'
with open(path, 'r', encoding='utf-8') as f:
    content = f.read()

# Add initialSubPagesMap near the start of the script tag
script_start = '<script>\n'
if script_start not in content:
    script_start = '<script>'

replacement_script_start = """<script>
    const initialSubPagesMap = @Html.Raw(System.Text.Json.JsonSerializer.Serialize(subPages.ToDictionary(p => p.CategorySlug, p => p)));
"""

if 'const initialSubPagesMap' not in content:
    content = content.replace(script_start, replacement_script_start, 1)

target_payload = """            payload.SubcategoryPages.push({
                Id: pageId,
                CategorySlug: slug,
                CategoryName: pageName,
                Emoji: pageEmoji,
                HeroBanner: {
                    IsEnabled: true,
                    Title: heroTitle,
                    TitleHighlight: heroHighlight,
                    Subtitle: heroSub,
                    ImageUrl: heroImg,
                    IsUploadingSoon: heroSoon,
                    BannerHeight: 380,
                    TitleColor: '#063f91',
                    TitleHighlightColor: '#0284c7',
                    SubtitleColor: '#334155'
                },
                ShopByBrands: {
                    IsEnabled: true,
                    Title: brandsTitle,
                    Subtitle: brandsSub,
                    Brands: brandsList
                },
                CuratedProducts: []
            });"""

replacement_payload = """            const existingPage = (typeof initialSubPagesMap !== 'undefined' && initialSubPagesMap[slug]) || {};
            payload.SubcategoryPages.push({
                Id: pageId,
                CategorySlug: slug,
                CategoryName: pageName,
                Emoji: pageEmoji,
                HeroBanner: {
                    IsEnabled: true,
                    Title: heroTitle,
                    TitleHighlight: heroHighlight,
                    Subtitle: heroSub,
                    ImageUrl: heroImg,
                    IsUploadingSoon: heroSoon,
                    BannerHeight: 400,
                    TitleColor: '#063f91',
                    TitleHighlightColor: '#0284c7',
                    SubtitleColor: '#334155'
                },
                ShopByBrands: {
                    IsEnabled: true,
                    Title: brandsTitle,
                    Subtitle: brandsSub,
                    Brands: brandsList
                },
                EquipmentTypes: existingPage.EquipmentTypes || [],
                CuratedProducts: existingPage.CuratedProducts || []
            });"""

content_norm = content.replace('\r\n', '\n')
target_norm = target_payload.replace('\r\n', '\n')
replacement_norm = replacement_payload.replace('\r\n', '\n')

if target_norm in content_norm:
    content_norm = content_norm.replace(target_norm, replacement_norm, 1)
    with open(path, 'w', encoding='utf-8') as f:
        f.write(content_norm)
    print("Updated Admin ElectronicsHub Index.cshtml successfully!")
else:
    print("Target payload not found in Admin Index.cshtml!")

