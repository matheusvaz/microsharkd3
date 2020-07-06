using Common.Domain.Multi;

namespace Membership.Domain
{
    public class Password
    {
        private readonly ICryptography cryptography;
        public string Value { get; private set; }

        public Password(string password, ICryptography cryptography)
        {
            Value = password;
            this.cryptography = cryptography;
            Validate();

            Value = cryptography.HashPassword(Value);
        }

        protected Password() { }

        public override string ToString()
        {
            return Value;
        }

        private void Validate()
        {
            Assert.NotEmpty(Value, Translation.Key("MEMBERSHIP_PASSWORD.PASSWORD_NOT_EMPTY"));
        }
    }
}
