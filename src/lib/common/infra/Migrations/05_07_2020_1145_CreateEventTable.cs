using Common.Domain.ValueObject;
using FluentMigrator;

namespace Common.Infra.Migrations
{
    [Migration(050720201145)]
    public class CreateEventsTable : Migration
    {
        public override void Up()
        {
            Create.Table("event")
                .WithColumn("Id").AsString(Id.FieldSize).NotNullable().PrimaryKey()
                .WithColumn("Type").AsString().NotNullable()
                .WithColumn("DateOccurred").AsDateTime2().NotNullable()
                .WithColumn("Data").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Table("event");
        }
    }
}
