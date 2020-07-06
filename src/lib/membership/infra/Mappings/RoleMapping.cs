using FluentNHibernate.Mapping;
using Membership.Domain;

namespace Membership.Infra.Mappings
{
    public class RoleMapping : ClassMap<Role>
    {
        public RoleMapping()
        {
            Table("role");

            CompositeId(map => map.Id).KeyProperty(prop => prop.Identifier, "Id");

            Map(prop => prop.Name).Column("Name").Length(Role.FieldSize["Name"]);

            Map(prop => prop.CreatedAt).Column("CreatedAt");

            Map(prop => prop.UpdatedAt).Column("UpdatedAt").Nullable();

            Map(prop => prop.DeletedAt).Column("DeletedAt").Nullable();

            HasManyToMany(prop => prop.UserRoles)
                .Access.CamelCaseField(Prefix.Underscore)
                .Inverse()
                .AsBag()
                .LazyLoad();
        }
    }
}
