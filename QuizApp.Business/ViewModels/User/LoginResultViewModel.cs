using Newtonsoft.Json;

namespace QuizApp.Business;

public class LoginResultViewModel
{
    [JsonProperty("access_token")]
    public required string AccessToken { get; set; }

    [JsonProperty("userId")]
    public required string UserId { get; set; }

    [JsonProperty("userInformation")]
    public required string UserInformation { get; set; }

    [JsonProperty(".expires")]
    public required DateTime ExpiresAt { get; set; }

    [JsonProperty(".issued")]
    public required DateTime IssuedAt { get; set; }
}
