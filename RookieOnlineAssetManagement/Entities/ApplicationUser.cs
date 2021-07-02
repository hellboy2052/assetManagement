using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RookieOnlineAssetManagement.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public int IncrementId { get; set; }
        public string StaffCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime JoinedDate { get; set; }
        public bool Gender { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsAssigned { get; set; }
        public bool? IsDefaultPassword { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
    }
}
