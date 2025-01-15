using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace Trifoia.Module.VideoPlayer.Migrations.EntityBuilders
{
    public class VideoPlayerEntityBuilder : AuditableBaseEntityBuilder<VideoPlayerEntityBuilder>
    {
        private const string _entityTableName = "TrifoiaVideoPlayer";
        private readonly PrimaryKey<VideoPlayerEntityBuilder> _primaryKey = new("PK_TrifoiaVideoPlayer", x => x.VideoPlayerId);
        private readonly ForeignKey<VideoPlayerEntityBuilder> _moduleForeignKey = new("FK_TrifoiaVideoPlayer_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public VideoPlayerEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override VideoPlayerEntityBuilder BuildTable(ColumnsBuilder table)
        {
            VideoPlayerId = AddAutoIncrementColumn(table,"VideoPlayerId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Name = AddMaxStringColumn(table,"Name");
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> VideoPlayerId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
    }
}
