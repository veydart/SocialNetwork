namespace SocialNetwork.Extensions.Errors
{
    public class ErrorModel
    {
        public string Title { get; set; }

        public int Status { get; set; }

        public string Raw { get; set; }

        public ErrorModel() { }

        public ErrorModel(string message, string raw, int status)
        {
            Title = message;
            Raw = raw;
            Status = status;
        }
    }
}