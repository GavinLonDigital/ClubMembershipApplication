namespace ClubMembershipApplication.FieldValidators
{
    public class FieldConstants
    {
        public enum UserRegistrationField  //readable fields rather than ints in field array
        {
            EmailAddress,
            FirstName,
            LastName,
            Password,
            PasswordCompare,
            DateOfBirth,
            PhoneNumber,
            AddressFirstLine,
            AddressSecondLine,
            AddressCity,
            PostCode
        }
    }
}
