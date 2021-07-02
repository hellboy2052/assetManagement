using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RookieOnlineAssetManagement.Entities;

namespace RookieOnlineAssetManagement.ViewModels.User
{
    public class UserAssignmentReadModel
    {
        public string AssignmentId { get; set; }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public string Category { get; set; }
        public DateTime AssignedDate { get; set; }
        public string State { get; set; }
    }
}
