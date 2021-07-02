using System.Collections.Generic;

namespace RookieOnlineAssetManagement.Models
{
    public class Account
    {
        public string Username { get; set; }

        public string FullName { get; set; }

        public string Role { get; set; }

        public bool IsDisabled { get; set; }
        public bool IsDefaultPassword { get; set; }
    }
}