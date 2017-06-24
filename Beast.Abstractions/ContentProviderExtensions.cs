namespace Beast
{
    public static class ContentProviderExtensions
    {
        public static string GetText(this IContentProvider provider, ContentKeys key)
        {
            return provider.GetText(key.ToString());
        }
    }
}
