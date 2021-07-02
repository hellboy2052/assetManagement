using System.Collections.Generic;

namespace RookieOnlineAssetManagement.ViewModels.Asset
{
    public class AssetReadViewModel
    {
        public IEnumerable<AssetReadModel> AssetReadModelList { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
    }
}
