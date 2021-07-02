using System;

namespace RookieOnlineAssetManagement.ViewModels.User
{
    public class UserReadModel
    {
        public string Id { get; set; }
        public string StaffCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => $"{FirstName} {LastName}"; }
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public DateTime JoinedDate { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public bool IsAssigned { get; set; }
    }
}
