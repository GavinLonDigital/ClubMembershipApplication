using ClubMembershipApplication.Data;
using FieldValidatorAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubMembershipApplication.FieldValidators
{
    public class UserRegistrationValidator:IFieldValidator
    {
        const int FirstName_Min_Length = 2;
        const int FirstName_Max_Length = 100;
        const int LastName_Min_Length = 2;
        const int LastName_Max_Length = 100;

        delegate bool EmailExistsDel(string emailAddress);

        FieldValidatorDel _fieldValidatorDel = null;

        RequiredValidDel _requiredValidDel = null;
        StringLengthValidDel _stringLenthValidDel = null;
        DateValidDel _dateValidDel = null;
        PatternMatchValidDel _patternMatchValidDel = null;
        CompareFieldsValidDel _compareFieldsValidDel = null;

        EmailExistsDel _emailExistsDel = null;

        string[] _fieldArray = null;
        IRegister _register = null;

        public string[] FieldArray
        {
            get
            {
                if (_fieldArray == null)
                    _fieldArray = new string[Enum.GetValues(typeof(FieldConstants.UserRegistrationField)).Length];
                return _fieldArray;
            }
        }

        public FieldValidatorDel ValidatorDel => _fieldValidatorDel;

        public UserRegistrationValidator(IRegister register)
        {
            _register = register;
        }

        public void InitialiseValidatorDelegates()
        {
            _fieldValidatorDel = new FieldValidatorDel(ValidField);
            _emailExistsDel = new EmailExistsDel(_register.EmailExists);

            _requiredValidDel = CommonFieldValidatorFunctions.RequiredFieldValidDel;
            _stringLenthValidDel = CommonFieldValidatorFunctions.StringLengthFieldValidDel;
            _dateValidDel = CommonFieldValidatorFunctions.DateFieldValidDel;
            _patternMatchValidDel = CommonFieldValidatorFunctions.PatternMatchValidDel;
            _compareFieldsValidDel = CommonFieldValidatorFunctions.FieldsCompareValidDel;
        }

        private bool ValidField(int fieldIndex, string fieldValue, string[] fieldArray, out string fieldInvalidMessage)
        {

            fieldInvalidMessage = "";

            FieldConstants.UserRegistrationField userRegistrationField = (FieldConstants.UserRegistrationField)fieldIndex;

            switch (userRegistrationField)
            {
                case FieldConstants.UserRegistrationField.EmailAddress:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_patternMatchValidDel(fieldValue,CommonRegularExpressionValidationPatterns.Email_Address_RegEx_Pattern)) ? $"You must enter a valid email address{Environment.NewLine}" : fieldInvalidMessage;
                    fieldInvalidMessage = (fieldInvalidMessage == "" && _emailExistsDel(fieldValue)) ? $"This email address already exists. Please try again{Environment.NewLine}" : fieldInvalidMessage;

                    break;
                case FieldConstants.UserRegistrationField.FirstName:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_stringLenthValidDel(fieldValue, FirstName_Min_Length,FirstName_Max_Length)) ? $"The length for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)} must be between {FirstName_Min_Length} and {FirstName_Max_Length}{Environment.NewLine}" : fieldInvalidMessage;
                    break;
                case FieldConstants.UserRegistrationField.LastName:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_stringLenthValidDel(fieldValue, LastName_Min_Length, LastName_Max_Length)) ? $"The length for field: {Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)} must be between {LastName_Min_Length} and {LastName_Max_Length}{Environment.NewLine}" : fieldInvalidMessage;
                    break;
                case FieldConstants.UserRegistrationField.Password:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_patternMatchValidDel(fieldValue, CommonRegularExpressionValidationPatterns.Strong_Password_RegEx_Pattern)) ? $"Your password must contain at least 1 small-case letter, 1 capital letter, 1 special character and the length should be between 6 - 10 characters{Environment.NewLine}" : fieldInvalidMessage;
                    break;
                case FieldConstants.UserRegistrationField.PasswordCompare:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_compareFieldsValidDel(fieldValue,fieldArray[(int)FieldConstants.UserRegistrationField.Password])) ? $"Your entry did not match your password{Environment.NewLine}" : fieldInvalidMessage;
                    break;
                case FieldConstants.UserRegistrationField.DateOfBirth:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_dateValidDel(fieldValue, out DateTime validDateTime)) ? $"You did not enter a valid date" : fieldInvalidMessage;
                    break;
                case FieldConstants.UserRegistrationField.PhoneNumber:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_patternMatchValidDel(fieldValue,CommonRegularExpressionValidationPatterns.Uk_PhoneNumber_RegEx_Pattern)) ? $"You did not enter a valid UK phone number{Environment.NewLine}" : fieldInvalidMessage;
                    break;
                case FieldConstants.UserRegistrationField.AddressFirstLine:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    break;
                case FieldConstants.UserRegistrationField.AddressSecondLine:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    break;
                case FieldConstants.UserRegistrationField.AddressCity:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    break;
                case FieldConstants.UserRegistrationField.PostCode:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : "";
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_patternMatchValidDel(fieldValue, CommonRegularExpressionValidationPatterns.Uk_Post_Code_RegEx_Pattern)) ? $"You did not enter a valid UK post code{Environment.NewLine}" : fieldInvalidMessage;
                    break;
                default:
                    throw new ArgumentException("This field does not exists");

            }

            return (fieldInvalidMessage == "");
 
        }
    }
}
