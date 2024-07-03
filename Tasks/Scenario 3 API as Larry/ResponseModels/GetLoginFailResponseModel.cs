using System.Text.Json.Serialization;

namespace Tasks.Scenario_3_API_as_Larry.ResponseModels
{
    public class GetLoginFailResponseModel
    {
        [JsonPropertyName("results")]
        public List<Result>? Results { get; set; }

        public class Result
        {
            [JsonPropertyName("user_name")]
            public string? UserName { get; set; }

            [JsonPropertyName("fail_count")]
            public int? FailCount { get; set; }
        }
    }
}
