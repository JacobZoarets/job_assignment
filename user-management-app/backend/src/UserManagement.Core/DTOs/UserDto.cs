// This file defines a Data Transfer Object (DTO) for user data, used for API responses.

using System;

namespace UserManagement.Core.DTOs
{
    public class UserDto
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