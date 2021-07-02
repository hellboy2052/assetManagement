using RookieOnlineAssetManagement.ViewModels.History;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RookieOnlineAssetManagement.ViewModels.Asset
{
    public class AssetDetailReadModel : AssetReadModel
    {
        public IEnumerable<HistoryReadModel> Histories { get; set; } = Enumerable.Empty<HistoryReadModel>();
    }
}
