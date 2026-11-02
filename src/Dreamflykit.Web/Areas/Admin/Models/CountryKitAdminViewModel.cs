using Dreamflykit.Application.CountryKits.Models;

namespace Dreamflykit.Web.Areas.Admin.Models;

public sealed class CountryKitAdminViewModel
{
    public CountryKitsHubDto HubData { get; set; } = new();
    public CountryKitProfileDto SelectedProfile { get; set; } = new();
    public string SelectedSlug { get; set; } = "usa";
    public List<CountryKitProfileDto> Countries { get; set; } = new();
}

