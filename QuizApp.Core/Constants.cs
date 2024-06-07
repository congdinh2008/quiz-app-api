namespace QuizApp.Core;

public struct Constants
{
    public static readonly Guid SystemAdministratorId = new Guid("00000000-0000-0000-0000-000000000000");

    public struct UserName
    {
        public const string System = "SYSTEM";
        public const string SystemAdministrator = "SystemAdministrator";
    }

    public struct Roles
    {
        public const string SystemAdministrator = "System Administrator";

        public const string Administrator = "Administrator";

        public const string Manager = "Manager";

        public const string User = "User";
    }

    public struct ErrorMessages
    {
        public const string ErrorFromServer = "ErrorFromServer: ";
    }

    public struct AppSetting
    {
        public struct ConnectionStrings
        {
            public const string DbConnection = "ConnectionStrings:DbConnection";
        }

        public struct Jwt
        {
            public const string ValidIssuer = "Jwt:ValidIssuer";
            
            public const string ValidAudience = "Jwt:ValidAudience";

            public const string Secret = "Jwt:Secret";

            public const string ExpirationInMinutes = "Jwt:ExpirationInMinutes";
        }

    }
}
