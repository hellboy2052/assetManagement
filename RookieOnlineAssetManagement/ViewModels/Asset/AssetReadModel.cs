using RookieOnlineAssetManagement.ViewModels.History;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.ViewModels.Asset
{
    public class AssetReadModel
    {
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public string Category { get; set; }
        public DateTime InstallDate { get; set; }
        public string State { get; set; }
        public string Location { get; set; }
        public string Specification { get; set; }
        public bool IsAssigned { get; set; }
    }
}
