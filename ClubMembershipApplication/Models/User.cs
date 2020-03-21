using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClubMembershipApplication.Models
{
    public class User
    {
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string AddressFirstLine { get; set; }

        public string AddressSecondLine { get; set; }

        public string AddressCity { get; set; }

        public string PostCode { get; set; }

    }
}
