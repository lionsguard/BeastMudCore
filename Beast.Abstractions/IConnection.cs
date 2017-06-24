using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beast
{
    public interface IConnection
    {
        Guid Id { get; set; }
        IConnectionContext Context { get; set; }
        IDictionary<string, object> Properties { get; }

        Task SendAsync(string message);
        Task SendErrorAsync(string error);

        Task CloseAsync();
    }
}
