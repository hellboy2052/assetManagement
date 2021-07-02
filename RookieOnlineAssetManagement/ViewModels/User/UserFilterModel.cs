using System;
using System.ComponentModel;

namespace RookieOnlineAssetManagement.ViewModels.User
{
    public class UserFilterModel : PaginationModel
    {
        public string QueryString { get; set; }
        [DefaultValue(new string[] { "Admin", "Staff" })]
        public string[] Type { get; set; }
    }
}
