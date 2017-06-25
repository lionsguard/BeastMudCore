using Beast.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beast.Sqlite.Objects
{
    public class ObjectRepository : RepositoryBase, IObjectRepository
    {
        public ObjectRepository(SqliteContext context) 
            : base(context)
        {
        }

        public Task<bool> DeleteObject<TObject>(TObject obj) where TObject : IObject
        {
            throw new NotImplementedException();
        }

        public Task<TObject> GetObject<TObject>(Guid id) where TObject : IObject
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TObject>> GetObjects<TObject>(Func<TObject, bool> predicate) where TObject : IObject
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveObject<TObject>(TObject obj) where TObject : IObject
        {
            throw new NotImplementedException();
        }
    }
}
