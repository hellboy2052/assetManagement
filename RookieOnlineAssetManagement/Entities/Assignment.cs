using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Entities
{
    public class Assignment
    {
        public string AssignmentId { get; set; }
        public string AssetCode { get; set; }
        public Asset Asset { get; set; }
        public string AdminId { get; set; }
        public ApplicationUser Admin { get; set; }
        public string StaffId { get; set; }
        public ApplicationUser Staff { get; set; }
        public DateTime AssignedDate { get; set; }
        public string State { get; set; }
        public string Note { get; set; }

    }
}
