using Common.Domain.Multi;

namespace Common.Infra.Multi
{
    public class PhoneNumber : IPhoneNumber
    {
        public bool IsValid(string number)
        {
            try
            {
                var phoneUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
                var phoneNumber = phoneUtil.Parse(number, null);

                return phoneUtil.IsValidNumber(phoneNumber);
            }
            catch
            {
                return false;
            }
        }
    }
}
