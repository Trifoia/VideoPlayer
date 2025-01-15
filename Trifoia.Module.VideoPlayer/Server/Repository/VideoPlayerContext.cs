using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace Trifoia.Module.VideoPlayer.Repository
{
    public class VideoPlayerContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.VideoPlayer> VideoPlayer { get; set; }

        public VideoPlayerContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.VideoPlayer>().ToTable(ActiveDatabase.RewriteName("TrifoiaVideoPlayer"));
        }
    }
}
