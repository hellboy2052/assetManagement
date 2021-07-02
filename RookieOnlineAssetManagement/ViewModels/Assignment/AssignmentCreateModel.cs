using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.ViewModels.Assignment
{
    public class AssignmentCreateModel
    {
        public string UserId { get; set; }
        public string AssestCode { get; set; }
        public DateTime AssignedDate { get; set; } = DateTime.Now;
        public string Note { get; set; }

    }
}
