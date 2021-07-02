using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Entities
{
    public class Category
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
    }
}
