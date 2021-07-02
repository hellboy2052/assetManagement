using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.ViewModels.Return
{
    public class ReturnReadModel
    {
        public string ReturnId { get; set; }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public string RequestedBy { get; set; }
        public string AcceptedBy { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public string State { get; set; }

    }
}
