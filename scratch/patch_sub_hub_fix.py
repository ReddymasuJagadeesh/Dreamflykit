import os

path = r'src/Dreamflykit.Web/Views/Shared/_ElectronicsSubcategoryHub.cshtml'
with open(path, 'r', encoding='utf-8') as f:
    content = f.read()

target1 = "var brandName = !string.IsNullOrWhiteSpace(prod.BrandName) ? prod.BrandName : activePage.CategoryName;"
replacement1 = "var brandName = activePage.CategoryName;"

target2 = "onclick=\"elecAddToCart('@prod.Id', '@prod.Name.Replace(\"'\", \"\\\\'\")', @prod.SellingPrice, this)\">"
replacement2 = "onclick=\"elecAddToCart('@prod.ProductId', '@prod.Name.Replace(\"'\", \"\\\\'\")', @prod.SellingPrice, this)\">"

if target1 in content:
    content = content.replace(target1, replacement1, 1)
    print("Replaced target1")
else:
    print("target1 not found")

if target2 in content:
    content = content.replace(target2, replacement2, 1)
    print("Replaced target2")
else:
    print("target2 not found")

with open(path, 'w', encoding='utf-8') as f:
    f.write(content)

