using Common.Domain.Multi;

namespace Membership.Domain
{
    public class PhoneNumber
    {
        private readonly IPhoneNumber phoneNumber;
        public string Value { get; private set; }

        public PhoneNumber(string number, IPhoneNumber phoneNumber)
        {
            this.phoneNumber = phoneNumber;
            Value = number;
            Validate();
        }

        protected PhoneNumber() { }

        public override string ToString()
        {
            return Value;
        }

        public static int FieldSize => 17;

        private void Validate()
        {
            Assert.NotEmpty(Value, Translation.Key("MEMBERSHIP_PHONE_NUMBER.PHONE_NUMBER_NOT_EMPTY"));

            if (!phoneNumber.IsValid(Value))
            {
                throw new DomainException(Translation.Key("MEMBERSHIP_PHONE_NUMBER.PHONE_NUMBER"));
            }
        }
    }
}
