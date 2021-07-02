using RookieOnlineAssetManagement.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.ViewModels.Asset
{
    public class AssetCreateModel
    {
        public string AssestName { get; set; }
        public string CategoryId { get; set; }
        public DateTime InstallDate { get; set; }
        public string Specification { get; set; }
        public EnumsObject.State State { get; set; }
    }
}
