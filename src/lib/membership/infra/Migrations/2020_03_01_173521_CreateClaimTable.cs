using Common.Domain.ValueObject;
using FluentMigrator;
using Membership.Domain;

namespace Membership.Infra.Migrations
{
    [Migration(20200301173521)]
    public class CreateClaimTable : Migration
    {
        public override void Up()
        {
            Create.Table("claim")
                .WithColumn("Id").AsString(Id.FieldSize).NotNullable().PrimaryKey()
                .WithColumn("UserId").AsString(Id.FieldSize).NotNullable().ForeignKey()
                .WithColumn("Name").AsString(Claim.FieldSize["Name"]).NotNullable()
                .WithColumn("Value").AsString(Claim.FieldSize["Value"]).NotNullable()
                .WithColumn("CreatedAt").AsDateTime2().NotNullable()
                .WithColumn("UpdatedAt").AsDateTime2().Nullable()
                .WithColumn("DeletedAt").AsDateTime2().Nullable();

            Create.ForeignKey()
                .FromTable("claim").ForeignColumn("UserId")
                .ToTable("user").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("claim");
        }
    }
}
