using ClubMembershipApplication.FieldValidators;
using ClubMembershipApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubMembershipApplication.Views
{
    

    public class WelcomeUserView:IView
    {
        User _user = null;

        public WelcomeUserView(User user)
        {
            _user = user;
        }

        public IFieldValidator FieldValidator => null;

        public void RunView()
        {
            Console.Clear();
            CommonOutputText.WriteMainHeading();

           CommonOutputFormat.ChangeFontColor(FontTheme.Success);
           Console.WriteLine($"Hi {_user.FirstName}!!{Environment.NewLine}Welcome to the Cycling Club!!");
           CommonOutputFormat.ChangeFontColor(FontTheme.Default);
           Console.ReadKey();
        }
    }
}
