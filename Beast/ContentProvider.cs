using Microsoft.Extensions.Options;
using System.IO;

namespace Beast
{
    public class ContentProvider : IContentProvider
    {
        public const string TextFolder = "text";

        readonly string _root;

        public ContentProvider(IOptions<ContentOptions> options)
        {
            _root = options.Value.RootPath;

            Directory.CreateDirectory(_root);
            Directory.CreateDirectory(Path.Combine(_root, TextFolder));
        }

        public string GetText(string key)
        {
            var path = Path.Combine(_root, TextFolder, string.Concat(key, ".txt"));
            if (!File.Exists(path))
                return string.Empty;
            return File.ReadAllText(path);
        }
    }
}
