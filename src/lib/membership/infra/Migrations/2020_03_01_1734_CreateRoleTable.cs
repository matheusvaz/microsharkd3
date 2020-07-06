using Common.Domain.ValueObject;
using FluentMigrator;
using Membership.Domain;

namespace Membership.Infra.Migrations
{
    [Migration(202003011734)]
    public class CreateRoleTable : Migration
    {
        public override void Up()
        {
            Create.Table("role")
                .WithColumn("Id").AsString(Id.FieldSize).NotNullable().PrimaryKey()
                .WithColumn("Name").AsString(Role.FieldSize["Name"]).NotNullable()
                .WithColumn("CreatedAt").AsDateTime2().NotNullable()
                .WithColumn("UpdatedAt").AsDateTime2().Nullable()
                .WithColumn("DeletedAt").AsDateTime2().Nullable();
        }

        public override void Down()
        {
            Delete.Table("role");
        }
    }
}
