namespace ECommerce.API.Common.Responses
{
    public class ErrorResponses
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public int StatusCode { get; set; }

        public List<string> Errors { get; set; } = new();
    }
}
