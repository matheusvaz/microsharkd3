using System.ComponentModel;

namespace Membership.Domain
{
    public enum UserStatus
    {
        [Description("Active")]
        Active,

        [Description("Inactive")]
        Inactive,

        [Description("Locked")]
        Locked,

        [Description("Suspended")]
        Suspended
    }
}
