using Common.Domain.ValueObject;
using FluentMigrator;
using Membership.Domain;

namespace Membership.Infra.Migrations
{
    [Migration(202003011732)]
    public class CreateUserTable : Migration
    {
        public override void Up()
        {
            Create.Table("user")
                .WithColumn("Id").AsString(Id.FieldSize).NotNullable().PrimaryKey()
                .WithColumn("FirstName").AsString(User.FieldSize["FirstName"]).NotNullable()
                .WithColumn("LastName").AsString(User.FieldSize["LastName"]).NotNullable()
                .WithColumn("Username").AsString(Username.FieldSize).NotNullable().Unique()
                .WithColumn("Email").AsString(Email.FieldSize).NotNullable().Unique()
                .WithColumn("Password").AsString().NotNullable()
                .WithColumn("Status").AsString(16).NotNullable()
                .WithColumn("PhoneNumber").AsString(PhoneNumber.FieldSize).Nullable().Unique()
                .WithColumn("TwoFactorEnabled").AsBoolean()
                .WithColumn("CreatedAt").AsDateTime2().NotNullable()
                .WithColumn("UpdatedAt").AsDateTime2().Nullable()
                .WithColumn("DeletedAt").AsDateTime2().Nullable();
        }

        public override void Down()
        {
            Delete.Table("user");
        }
    }
}
