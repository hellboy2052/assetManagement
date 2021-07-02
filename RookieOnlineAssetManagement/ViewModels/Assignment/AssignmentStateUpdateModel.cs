using RookieOnlineAssetManagement.Enums;
using System;

namespace RookieOnlineAssetManagement.ViewModels.Assignment
{
    public class AssignmentStateUpdateModel
    {
        public string AssignmentId { get; set; }
        public EnumsObject.AssignmentState AssignmentState { get; set; }

    }
}
