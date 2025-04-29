namespace Security.Core.MessageBus
{
    public static class RabbitQueue
    {
        // ITEM LOCATION
        public static string AccountNewRequestQueue { get; } = "AccountNewRequestQueue";
        public static string AccountFindRequestQueue { get; } = "AccountFindRequestQueue";
        public static string AccountFindResponseQueue { get; } = "AccountFindResponseQueue";
    }
}
