using FluentNHibernate.Mapping;
using Membership.Domain;

namespace Membership.Infra.Mappings
{
    public class UserRoleMapping : ClassMap<UserRole>
    {
        public UserRoleMapping()
        {
            Table("userrole");

            CompositeId(map => map.Id).KeyProperty(prop => prop.Identifier, "Id");

            Map(prop => prop.CreatedAt).Column("CreatedAt");

            Map(prop => prop.UpdatedAt).Column("UpdatedAt").Nullable();

            Map(prop => prop.DeletedAt).Column("DeletedAt").Nullable();

            HasMany(prop => prop.Users)
                .Access.CamelCaseField(Prefix.Underscore)
                .Inverse()
                .AsBag()
                .LazyLoad();

            HasMany(prop => prop.Roles)
                .Access.CamelCaseField(Prefix.Underscore)
                .Inverse()
                .AsBag()
                .LazyLoad();
        }
    }
}
