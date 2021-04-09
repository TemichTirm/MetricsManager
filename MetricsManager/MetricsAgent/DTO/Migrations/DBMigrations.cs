using FluentMigrator;

namespace MetricsAgent.DTO.Migrations
{
    
    [Migration(1)]
    public class FirstMigration : Migration
    {
        public override void Up()
        {
            // CPU metrics            
            Create.Table("cpumetrics")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Value").AsInt32()
            .WithColumn("Time").AsInt64();

            // .Net metrics
            Create.Table("dotnetmetrics")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Value").AsInt32()
            .WithColumn("Time").AsInt64();

            // HDD metrics
            Create.Table("hddmetrics")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Value").AsInt32()
            .WithColumn("Time").AsInt64();

            // Networt metrics
            Create.Table("networkmetrics")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Value").AsInt32()
            .WithColumn("Time").AsInt64();

            // RAM metrics
            Create.Table("rammetrics")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Value").AsInt32()
            .WithColumn("Time").AsInt64();
        }
        public override void Down()
        {
            Delete.Table("cpumetrics");
            Delete.Table("dotnetmetrics");
            Delete.Table("hddmetrics");
            Delete.Table("networkmetrics");
            Delete.Table("rammetrics");
        }
    }
}
