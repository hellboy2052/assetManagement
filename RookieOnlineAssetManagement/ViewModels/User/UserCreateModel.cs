using RookieOnlineAssetManagement.Enums;
using System;

namespace RookieOnlineAssetManagement.ViewModels.User
{
    public class UserCreateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EnumsObject.Location? Location { get; set; }
        public DateTime DoB { get; set; }
        public bool Gender { get; set; }
        public DateTime JoinedDate { get; set; }
        public EnumsObject.Type Type { get; set; }
    }
}
