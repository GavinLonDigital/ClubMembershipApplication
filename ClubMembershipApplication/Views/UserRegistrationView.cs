using ClubMembershipApplication.Data;
using ClubMembershipApplication.FieldValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubMembershipApplication.Views
{
    public class UserRegistrationView:IView
    {
        IFieldValidator _fieldValidator = null;

        IRegister _register = null;

        public IFieldValidator FieldValidator { get => _fieldValidator;}

        public UserRegistrationView(IRegister register, IFieldValidator fieldValidator)
        {
            _fieldValidator = fieldValidator;
            _register = register;
        }

        public void RunView()
        {
            CommonOutputText.WriteMainHeading();
            CommonOutputText.WriteRegistrationHeading();

            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.EmailAddress] = GetInputFromUser(FieldConstants.UserRegistrationField.EmailAddress,"Please enter your email address: ");
            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.FirstName] = GetInputFromUser(FieldConstants.UserRegistrationField.FirstName, "Please enter your first name: ");
            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.LastName] = GetInputFromUser(FieldConstants.UserRegistrationField.LastName, "Please enter your last name: ");
            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.Password] = GetInputFromUser(FieldConstants.UserRegistrationField.Password, $"Please enter your password.{Environment.NewLine}(Your password must contain at least 1 small-case letter,{Environment.NewLine}1 Capital letter, 1 digit, 1 special character{Environment.NewLine} and the length should be between 6-10 characters): ");
            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.PasswordCompare] = GetInputFromUser(FieldConstants.UserRegistrationField.PasswordCompare, "Please re-enter your password: ");
            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.DateOfBirth] = GetInputFromUser(FieldConstants.UserRegistrationField.DateOfBirth, "Please enter your date of birth: ");
            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.PhoneNumber] = GetInputFromUser(FieldConstants.UserRegistrationField.PhoneNumber, "Please enter your phone number: ");
            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.AddressFirstLine] = GetInputFromUser(FieldConstants.UserRegistrationField.AddressFirstLine, "Please enter the first line of your address: ");
            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.AddressSecondLine] = GetInputFromUser(FieldConstants.UserRegistrationField.AddressSecondLine, "Please enter the second line of your address: ");
            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.AddressCity] = GetInputFromUser(FieldConstants.UserRegistrationField.AddressCity, "Please enter the city where you live: ");
            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.PostCode] = GetInputFromUser(FieldConstants.UserRegistrationField.PostCode, "Please enter your post code: ");

            RegisterUser();
        }

        private void RegisterUser()
        {
            _register.Register(_fieldValidator.FieldArray);

            CommonOutputFormat.ChangeFontColor(FontTheme.Success);
            Console.WriteLine("You have successfully registered. Please press any key to login");
            CommonOutputFormat.ChangeFontColor(FontTheme.Default);
            Console.ReadKey();
        }

        private string GetInputFromUser(FieldConstants.UserRegistrationField field, string promptText)
        {
            string fieldVal = "";

            do
            {
                Console.Write(promptText);
                fieldVal = Console.ReadLine();
            }
            while (!FieldValid(field, fieldVal));

            return fieldVal;
        }

        private bool FieldValid(FieldConstants.UserRegistrationField field, string fieldValue)
        {
            if (!_fieldValidator.ValidatorDel((int)field, fieldValue, _fieldValidator.FieldArray, out string invalidMessage))
            {
                CommonOutputFormat.ChangeFontColor(FontTheme.Danger);

                Console.WriteLine(invalidMessage);

                CommonOutputFormat.ChangeFontColor(FontTheme.Default);
               
                return false;
            }
            return true;
        }

    }
}
