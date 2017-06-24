namespace Beast
{
    public static class ConnectionExtensions
    {
        public static T GetValue<T>(this IConnection connection, string key)
        {
            return connection.GetValue<T>(key, default(T));
        }
        public static T GetValue<T>(this IConnection connection, string key, T defaultValue)
        {
            if (!connection.Properties.ContainsKey(key))
                return defaultValue;
            return (T)connection.Properties[key];
        }

        public static void SetValue(this IConnection connection, string key, object value)
        {
            connection.Properties[key] = value;
        }
    }
}
