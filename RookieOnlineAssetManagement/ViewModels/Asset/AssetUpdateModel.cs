using RookieOnlineAssetManagement.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.ViewModels.Asset
{
    public class AssetUpdateModel
    {
        public string AssetCode { get; set; }
        public string AssestName { get; set; }
        public DateTime InstallDate { get; set; }
        public string Specification { get; set; }
        public EnumsObject.State State { get; set; }
    }
}
