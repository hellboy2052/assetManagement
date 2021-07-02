using System.Collections.Generic;

namespace RookieOnlineAssetManagement.ViewModels.User
{
    public class UserReadViewModel
    {
        public IEnumerable<UserReadModel> UserReadModelList { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
    }
}
