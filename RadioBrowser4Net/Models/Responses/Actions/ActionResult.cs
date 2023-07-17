using System.Text.Json.Serialization;

namespace RadioBrowser4Net.Models.Responses.Actions
{
    public class ActionResult
    {
		[JsonPropertyName("ok")]
        public bool Ok { get; set; }

		[JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
