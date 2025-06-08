// This file contains a service for fetching user data from the Random User Generator API.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UserManagement.Core.Models;

namespace UserManagement.Infrastructure.Services
{
    public class RandomUserApiService
    {
        private readonly HttpClient _httpClient;

        // Constructor that initializes the HttpClient
        public RandomUserApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Method to fetch a list of users from the Random User Generator API
        public async Task<User[]> GetUsersAsync(int page = 1, int resultsPerPage = 10)
        {
            // Construct the API URL with pagination parameters
            var url = $"https://randomuser.me/api/?page={page}&results={resultsPerPage}";

            // Send a GET request to the API
            var response = await _httpClient.GetAsync(url);

            // Ensure the response is successful
            response.EnsureSuccessStatusCode();

            // Deserialize the response content to get the user data
            var userResponse = await response.Content.ReadFromJsonAsync<RandomUserApiResponse>();

            // Map the API response to the User model
            return userResponse?.Results?.Select(user => new User
            {
                Id = Guid.NewGuid(), // Generate a new GUID for the user
                FirstName = user?.Name?.First ?? "Unknown",
                LastName = user?.Name?.Last ?? "Unknown",
                Email = user?.Email ?? "unknown@example.com",
                DateOfBirth = user?.Dob?.Date ?? DateTime.MinValue,
                Phone = user?.Phone ?? "N/A",
                Address = $"{user?.Location?.Street?.Number?.ToString() ?? "0"} {user?.Location?.Street?.Name ?? "Unknown St"}",
                ProfilePicture = user?.Picture?.Thumbnail ?? ""
            }).ToArray() ?? Array.Empty<User>();
        }
    }

    // Class to represent the API response structure
    public class RandomUserApiResponse
    {
        public UserResult[]? Results { get; set; }
    }

    // Class to represent individual user results from the API
    public class UserResult
    {
        public UserName? Name { get; set; }
        public string? Email { get; set; }
        public UserDob? Dob { get; set; }
        public string? Phone { get; set; }
        public UserLocation? Location { get; set; }
        public UserPicture? Picture { get; set; }
    }

    // Class to represent the user's name
    public class UserName
    {
        public string? First { get; set; }
        public string? Last { get; set; }
    }

    // Class to represent the user's date of birth
    public class UserDob
    {
        public DateTime Date { get; set; }
    }

    // Class to represent the user's location
    public class UserLocation
    {
        public UserStreet? Street { get; set; }
    }

    // Class to represent the user's street information
    public class UserStreet
    {
        public object? Number { get; set; }
        public string? Name { get; set; }
    }

    // Class to represent the user's picture
    public class UserPicture
    {
        public string? Thumbnail { get; set; }
    }
}