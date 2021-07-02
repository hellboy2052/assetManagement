using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.ViewModels.User;
using System;
using System.ComponentModel;

namespace RookieOnlineAssetManagement.ViewModels.Asset
{
    public class AssignmentFilterModel : PaginationModel
    {
        public string QueryString { get; set; }
        public DateTime? AssignedDate { get; set; }
        public EnumsObject.AssignmentState[] States { get; set; }
    }
}
