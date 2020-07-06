using FluentNHibernate.Mapping;
using Membership.Domain;

namespace Membership.Infra.Mappings
{
    public class UserMapping : ClassMap<User>
    {
        public UserMapping()
        {
            Table("user");

            CompositeId(map => map.Id).KeyProperty(prop => prop.Identifier, "Id");

            Map(prop => prop.FirstName).Column("FirstName").Length(User.FieldSize["FirstName"]);

            Map(prop => prop.LastName).Column("LastName").Length(User.FieldSize["LastName"]);

            Component(map => map.Username, columns =>
            {
                columns.Map(field => field.Value).Column("Username").Length(Username.FieldSize).Unique();
            });

            Component(map => map.Email, columns =>
            {
                columns.Map(field => field.Value).Column("Email").Length(Email.FieldSize).Unique();
            });

            Component(map => map.Password, columns =>
            {
                columns.Map(field => field.Value).Column("Password");
            });

            Map(prop => prop.Status).Column("Status");

            Component(map => map.PhoneNumber, columns =>
            {
                columns.Map(field => field.Value).Column("PhoneNumber").Length(PhoneNumber.FieldSize).Nullable().Unique();
            });

            Map(prop => prop.TwoFactorEnabled).Column("TwoFactorEnabled");

            Map(prop => prop.CreatedAt).Column("CreatedAt");

            Map(prop => prop.UpdatedAt).Column("UpdatedAt").Nullable();

            Map(prop => prop.DeletedAt).Column("DeletedAt").Nullable();

            HasMany(prop => prop.Claims)
                .KeyColumn("UserId")
                .Access.CamelCaseField(Prefix.Underscore)
                .AsBag()
                .LazyLoad();

            HasMany(prop => prop.UserRoles)
                .Access.CamelCaseField(Prefix.Underscore)
                .AsBag()
                .LazyLoad();
        }
    }
}
