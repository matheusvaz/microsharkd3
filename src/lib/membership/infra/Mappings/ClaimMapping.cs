using FluentNHibernate.Mapping;
using Membership.Domain;

namespace Membership.Infra.Mappings
{
    public class ClaimMapping : ClassMap<Claim>
    {
        public ClaimMapping()
        {
            Table("claim");

            CompositeId(map => map.Id).KeyProperty(prop => prop.Identifier, "Id");

            Map(prop => prop.Name).Column("Name").Length(Claim.FieldSize["Name"]);

            Map(prop => prop.Value).Column("Value").Length(Claim.FieldSize["Value"]);

            Map(prop => prop.CreatedAt).Column("CreatedAt");

            Map(prop => prop.UpdatedAt).Column("UpdatedAt").Nullable();

            Map(prop => prop.DeletedAt).Column("DeletedAt").Nullable();

            References(map => map.User).Column("UserId");
        }
    }
}
