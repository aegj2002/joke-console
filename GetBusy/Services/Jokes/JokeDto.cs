using System.Text.Json.Serialization;

namespace GetBusy.Services.Jokes
{
    public class JokeDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }


        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }


        [JsonPropertyName("updated_at")]
        public DateTime UpdateAt { get; set; }


        [JsonPropertyName("icon_url")]
        public string IconUrl { get; set; }


        [JsonPropertyName("url")]
        public string URL { get; set; }


        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
