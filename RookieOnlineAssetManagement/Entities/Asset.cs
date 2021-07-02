using RookieOnlineAssetManagement.Enums;
using System;
using System.Collections.Generic;

namespace RookieOnlineAssetManagement.Entities
{
    public class Asset
    {
        public string AssestCode { get; set; }
        public string AssestName { get; set; }
        public DateTime InstallDate { get; set; }
        public string Specification { get; set; }
        public string State { get; set; }
        public string Location { get; set; }
        public bool IsAssigned { get; set; }
        public string CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}
