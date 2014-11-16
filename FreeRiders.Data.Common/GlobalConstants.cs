namespace FreeRiders.Data.Common
{
    public class GlobalConstants
    {
        public const string AdminRole = "Admin";
        public const string ModeratorRole = "Moderator";
        public const string TrustedUserRole = "TrustedUser";
        public const string RegularUserRole = "RegularUser";
        public const string AllRolesExceptRegularUser = AdminRole + ", " + ModeratorRole + ", " + TrustedUserRole;
        public const string AdministrationRoles = AdminRole + ", " + ModeratorRole;

        public static string AdminEmailUsername = "admin@freeriders.com";
        public static string AdminPassword = "parola";
    }
}
