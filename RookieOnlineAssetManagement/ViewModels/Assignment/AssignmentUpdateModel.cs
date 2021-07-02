using System;

namespace RookieOnlineAssetManagement.ViewModels.Assignment
{
    public class AssignmentUpdateModel
    {
        public string AssignmentId { get; set; }
        public string UserId { get; set; }
        public string AssestCode { get; set; }
        public DateTime AssignedDate { get; set; } = DateTime.Now;
        public string Note { get; set; }

    }
}
