using Common.Domain.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace FlagSense.FlagService.Domain.Data
{
    public class FsContext : FxSqlServerDbContext
    {
        public FsContext(DbContextOptions options) : base(options)
        {
        }
    }
}
