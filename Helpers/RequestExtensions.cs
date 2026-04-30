namespace learn_asp.net_mvc.Helpers
{
    public static class RequestExtensions
    {
        public static string GetDebugInfo(this HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host}";
        }
    }
}
