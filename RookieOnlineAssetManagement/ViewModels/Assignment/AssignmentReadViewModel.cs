using System.Collections.Generic;

namespace RookieOnlineAssetManagement.ViewModels.Assignment
{
    public class AssignmentReadViewModel
    {
        public IEnumerable<AssignmentReadModel> AssignmentReadModelList { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
    }
}
