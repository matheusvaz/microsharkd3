using Common.Domain.Multi;

namespace Membership.Domain
{
    public class Email
    {
        public string Value { get; private set; }

        public Email(string email)
        {
            Value = email;
            Validate();
        }

        protected Email() { }

        public override string ToString()
        {
            return Value;
        }

        public static int FieldSize => 255;

        private void Validate()
        {
            Assert.NotEmpty(Value, Translation.Key("MEMBERSHIP_EMAIL.EMAIL_NOT_EMPTY"));
            Assert.MaxLength(Value, FieldSize, Translation.Key("MEMBERSHIP_EMAIL.EMAIL_MAXIMUM_LENGTH", false, FieldSize));
            Assert.IsEmail(Value, Translation.Key("MEMBERSHIP_EMAIL.EMAIL"));
        }
    }
}
