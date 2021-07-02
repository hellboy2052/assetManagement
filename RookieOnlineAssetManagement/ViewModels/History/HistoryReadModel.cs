using System;

namespace RookieOnlineAssetManagement.ViewModels.History
{
    public class HistoryReadModel
    {
        public DateTime AssignedDate { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedBy { get; set; }
        public DateTime ReturnedDate { get; set; }
    }
}
