using Common.Domain.Multi;

namespace Membership.Domain
{
    public class Username
    {
        public string Value { get; private set; }

        public Username(string username)
        {
            Value = username;
            Validate();
        }

        protected Username() { }

        public override string ToString()
        {
            return Value;
        }

        public static int FieldSize => 16;

        private void Validate()
        {
            Assert.NotEmpty(Value, Translation.Key("MEMBERSHIP_USER_NAME.USERNAME_NOT_EMPTY"));            
            Assert.MaxLength(Value, FieldSize, Translation.Key("MEMBERSHIP_USER_NAME.USERNAME_MAXIMUM_LENGTH", false, FieldSize));
            Assert.IsMatch("[A-Za-z0-9_]", Value, Translation.Key("MEMBERSHIP_USER_NAME.USERNAME"));
        }
    }
}
