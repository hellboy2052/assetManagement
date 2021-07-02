using System;
using RookieOnlineAssetManagement.Enums;

namespace RookieOnlineAssetManagement.ViewModels.Return
{
    public class ReturnFilteredModel
    {
        public string QueryString { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public EnumsObject.Returning[] States { get; set; }
    }
}