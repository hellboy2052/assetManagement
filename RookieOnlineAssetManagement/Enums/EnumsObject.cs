using System.ComponentModel;

namespace RookieOnlineAssetManagement.Enums
{
    public static class EnumsObject
    {
        public enum State
        {
            Available = 1,
            NotAvailable = 2,
            Assigned = 3,
            WaitingForRecycling = 4,
            Recycled = 5,
        }
        public enum AssignmentState
        {
            Accepted = 1,
            Declined = 2,
            WaitingForAcceptance = 3,
            WaitingForReturning = 4,
            ReturningDeclined = 5,
            Completed = 6,
        }
        public enum Location
        {
            HCM = 1,
            HN = 2,
        }
        public enum Type
        {
            Admin = 1,
            Staff = 2,
        }
        public enum Returning
        {
            WaitingForReturning = 1,
            Completed = 2,
        }
    }
}
