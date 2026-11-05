import os

path = r'src/Dreamflykit.Web/Views/Shared/_ElectronicsCategoryHub.cshtml'
with open(path, 'r', encoding='utf-8') as f:
    content = f.read()

target = """    string GetCategoryEmoji(string? slug) => slug switch
    {
        "laptops-macbooks" or "laptops" or "macbooks" => "💻",
        "headphones-audio" or "headphones" or "audio" => "🎧",
        "travel-adapters" or "adapters" => "🔌",
        "power-banks-chargers" or "chargers" or "powerbanks" => "🔋",
        "student-sims" or "sims" or "esim" => "📶",
        "storage-ssds" or "ssds" or "storage" => "💾",
        "smartwatches-wearables" or "smartwatches" or "watches" => "⌚",
        "cables-hubs-docks" or "cables" or "hubs" => "🔗",
        "monitors-displays" or "monitors" => "🖥️",
        "calculators-tech-tools" or "calculators" => "🔢",
        _ => "⚡"
    };"""

replacement = """    string GetCategoryEmoji(string? slug) => slug switch
    {
        "laptops-macbooks" or "laptops" or "macbooks" => "💻",
        "headphones-audio" or "headphones" or "earphones" or "audio" => "🎧",
        "grooming" or "shavers" or "trimmers" => "🪒",
        "mobile-cases" or "cases" or "phone-cases" => "📱",
        "storage-devices" or "storage-ssds" or "ssds" or "storage" => "💾",
        "chargers-cables" or "cables-hubs-docks" or "chargers" or "cables" or "travel-adapters" or "adapters" => "🔌",
        "power-banks" or "power-banks-chargers" or "powerbanks" => "🔋",
        "watches" or "smartwatches-wearables" or "smartwatches" => "⌚",
        "computer-accessories" or "accessories" or "mouse" or "keyboard" => "🖱️",
        "speakers" or "bluetooth-speakers" => "🔊",
        "student-sims" or "sims" or "esim" => "📶",
        "monitors-displays" or "monitors" => "🖥️",
        "calculators-tech-tools" or "calculators" => "🔢",
        _ => "⚡"
    };"""

if target in content:
    content = content.replace(target, replacement, 1)
    with open(path, 'w', encoding='utf-8') as f:
        f.write(content)
    print("Updated _ElectronicsCategoryHub.cshtml successfully!")
else:
    # Try normalizing line endings
    content_norm = content.replace('\r\n', '\n')
    target_norm = target.replace('\r\n', '\n')
    replacement_norm = replacement.replace('\r\n', '\n')
    if target_norm in content_norm:
        content_norm = content_norm.replace(target_norm, replacement_norm, 1)
        with open(path, 'w', encoding='utf-8') as f:
            f.write(content_norm)
        print("Updated _ElectronicsCategoryHub.cshtml with normalized LF successfully!")
    else:
        print("Target still not found!")

