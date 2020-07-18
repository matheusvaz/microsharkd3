using Common.Domain.ValueObject;
using FluentMigrator;

namespace Membership.Infra.Migrations
{
    [Migration(20200301174803)]
    public class CreateUserRoleTable : Migration
    {
        public override void Up()
        {
            Create.Table("userrole")
                .WithColumn("Id").AsString(Id.FieldSize).NotNullable().PrimaryKey()
                .WithColumn("UserId").AsString(Id.FieldSize).NotNullable().ForeignKey()
                .WithColumn("RoleId").AsString(Id.FieldSize).NotNullable()
                .WithColumn("CreatedAt").AsDateTime2().NotNullable()
                .WithColumn("UpdatedAt").AsDateTime2().Nullable()
                .WithColumn("DeletedAt").AsDateTime2().Nullable();

            Create.ForeignKey()
                .FromTable("userrole").ForeignColumn("UserId")
                .ToTable("user").PrimaryColumn("Id");

            Create.ForeignKey()
                .FromTable("userrole").ForeignColumn("RoleId")
                .ToTable("role").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("claim");
        }
    }
}
