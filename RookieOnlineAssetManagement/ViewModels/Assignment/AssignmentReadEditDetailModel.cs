using System;

namespace RookieOnlineAssetManagement.ViewModels.Assignment
{
    public class AssignmentReadEditDetailModel
    {
        public string AssignmentId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public DateTime AssignedDate { get; set; }
        public string Note { get; set; }
    }
}
