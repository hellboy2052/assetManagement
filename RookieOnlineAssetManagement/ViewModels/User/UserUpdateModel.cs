using RookieOnlineAssetManagement.Enums;
using System;

namespace RookieOnlineAssetManagement.ViewModels.User
{
    public class UserUpdateModel
    {
        public string Id { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public DateTime JoinedDate { get; set; }
        public EnumsObject.Type Type { get; set; }
    }
}
