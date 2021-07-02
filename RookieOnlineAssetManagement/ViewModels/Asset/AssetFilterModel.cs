using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.ViewModels.User;
using System.ComponentModel;

namespace RookieOnlineAssetManagement.ViewModels.Asset
{
    public class AssetFilterModel : PaginationModel
    {
        public string QueryString { get; set; }
        [DefaultValue(new string[] { "Personal Computer", "Laptop" })]
        public string[] Category { get; set; }
        public EnumsObject.State[] States { get; set; }
    }
}
