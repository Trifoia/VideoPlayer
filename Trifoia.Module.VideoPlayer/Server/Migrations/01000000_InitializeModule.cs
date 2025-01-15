using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Trifoia.Module.VideoPlayer.Migrations.EntityBuilders;
using Trifoia.Module.VideoPlayer.Repository;

namespace Trifoia.Module.VideoPlayer.Migrations
{
    [DbContext(typeof(VideoPlayerContext))]
    [Migration("Trifoia.Module.VideoPlayer.01.00.00.00")]
    public class InitializeModule : MultiDatabaseMigration
    {
        public InitializeModule(IDatabase database) : base(database)
        {
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var entityBuilder = new VideoPlayerEntityBuilder(migrationBuilder, ActiveDatabase);
            entityBuilder.Create();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var entityBuilder = new VideoPlayerEntityBuilder(migrationBuilder, ActiveDatabase);
            entityBuilder.Drop();
        }
    }
}
