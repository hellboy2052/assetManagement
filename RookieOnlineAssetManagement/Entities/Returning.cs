using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Entities
{
    public class Returning
    {
        public string ReturnId { get; set; }
        public string AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }
        public string RequestById { get; set; }
        public virtual ApplicationUser RequestBy { get; set; }
        public string AssignedById { get; set; }
        public virtual ApplicationUser AssignedBy { get; set; }
        public DateTime ReturnedDate { get; set; }
        public string State { get; set; }

    }
}
