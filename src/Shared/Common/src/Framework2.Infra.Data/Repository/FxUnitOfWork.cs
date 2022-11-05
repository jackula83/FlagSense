using Framework2.Core.Extensions;
using Framework2.Infra.Data.Context;
using Microsoft.Extensions.Logging;

namespace Framework2.Infra.Data.Repository
{
    public abstract class FxUnitOfWork<TContext> : IDisposable
        where TContext : FxDbContext
    {
        protected readonly TContext _context;
        protected readonly string _correlationId;
        private readonly object _scope = new();
        private bool _disposed = false;
        private ILogger<FxUnitOfWork<TContext>> _logger;

        public FxUnitOfWork(ILogger<FxUnitOfWork<TContext>> logger, TContext context, string correlationId)
        {
            _logger = logger;
            _context = context;
            _correlationId = correlationId;
        }

        public virtual async Task Save()
            => await _context.SaveChangesAsync();

        public virtual void Dispose()
        {
            lock (_scope)
            {
                if (!_disposed)
                    _context.Dispose();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }

        protected virtual async Task<TResult?> Run<TResult>(Func<Task<TResult>> func)
        {
            try
            {
                return await func();
            } 
            catch (Exception e)
            {
                _logger.LogError(e.MakeMessage(_correlationId));
            }

            return default;
        }
    }
}
