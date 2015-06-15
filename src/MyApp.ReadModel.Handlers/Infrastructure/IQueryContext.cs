using System;
using System.Data.Entity;

namespace MyApp.ReadModel.Handlers
{
    public interface IQueryContext : IDisposable
    {
        IDbSet<T> Set<T>() where T : class;
    }

    public class QueryContext : IQueryContext
    {
        private DbContext _context;

        public QueryContext(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        public IDbSet<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
    }
}