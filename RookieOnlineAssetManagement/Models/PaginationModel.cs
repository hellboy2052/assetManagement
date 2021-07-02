using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.ViewModels.User
{
    public class PaginationModel
    {
        [DefaultValue(1)]
        public int? PageIndex { get; set; } = 1;
        [DefaultValue(10)]
        public int? PageSize { get; set; } = 10;
    }
}
