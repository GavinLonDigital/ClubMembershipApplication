using ClubMembershipApplication.FieldValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubMembershipApplication.Views
{
    public interface IView
    {
        void RunView();
        IFieldValidator FieldValidator { get; }
    }
}
