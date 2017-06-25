using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beast.Objects
{
    public interface IObjectRepository : IDisposable
    {
        Task<TObject> GetObject<TObject>(Guid id) where TObject : IObject;
        Task<IEnumerable<TObject>> GetObjects<TObject>(Func<TObject, bool> predicate) where TObject : IObject;

        Task<bool> SaveObject<TObject>(TObject obj) where TObject : IObject;

        Task<bool> DeleteObject<TObject>(TObject obj) where TObject : IObject;
    }
}
