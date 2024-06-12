namespace QuizApp.WebAPI;

public class AppSetting
{
    public required ConnectionStrings ConnectionStrings { get; set; }

    public required Jwt Jwt { get; set; }
}

public class Jwt
{
    public required string ValidAudience { get; set; }
    public required string ValidIssuer { get; set; }
    public required string Secret { get; set; }
    public required string ExpirationInMinutes { get; set; }
}

public class ConnectionStrings
{
    public required string QuizAppConnection { get; set; }
}