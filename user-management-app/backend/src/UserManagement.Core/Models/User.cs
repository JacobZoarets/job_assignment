// This file defines the User model, representing user data in the application.

using System;

namespace UserManagement.Core.Models
{
    public class User
    {
        // Unique identifier for the user
        public Guid Id { get; set; }

        // User's first name
        public string FirstName { get; set; }

        // User's last name
        public string LastName { get; set; }

        // User's email address
        public string Email { get; set; }

        // User's date of birth
        public DateTime DateOfBirth { get; set; }

        // User's phone number
        public string Phone { get; set; }

        // User's address
        public string Address { get; set; }

        // URL of the user's profile picture
        public string ProfilePicture { get; set; }
    }
}