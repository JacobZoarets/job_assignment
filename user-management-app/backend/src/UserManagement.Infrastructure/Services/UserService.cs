// UserService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UserManagement.Core.DTOs;
using UserManagement.Core.Interfaces;
using UserManagement.Core.Models;

namespace UserManagement.Infrastructure.Services
{
    // UserService class implements IUserService interface and handles business logic
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly RandomUserApiService _randomUserApiService;
        private readonly ILogger<UserService> _logger;

        // Constructor to inject dependencies
        public UserService(IUserRepository userRepository, RandomUserApiService randomUserApiService, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _randomUserApiService = randomUserApiService;
            _logger = logger;
        }

        // Method to get users with pagination
        public async Task<PaginatedResult<UserDto>> GetUsersAsync(int pageNumber, int pageSize)
        {
            try
            {
                // Check if we have cached users in the database
                var totalCount = await _userRepository.GetTotalUsersCountAsync();
                
                // If no users in cache, fetch from external API and cache them
                if (totalCount == 0)
                {
                    _logger.LogInformation("No users found in cache, fetching from external API");
                    await FetchAndCacheUsersFromExternalApi();
                    totalCount = await _userRepository.GetTotalUsersCountAsync();
                }
                
                // Fetch users from repository with pagination
                var users = await _userRepository.GetUsersAsync(pageNumber, pageSize);

                // Map users to UserDto
                var userDtos = users.Select(user => new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DateOfBirth = user.DateOfBirth,
                    Phone = user.Phone,
                    Address = user.Address,
                    ProfilePicture = user.ProfilePicture
                }).ToList();

                // Return paginated result
                return new PaginatedResult<UserDto>(userDtos, totalCount, pageSize, pageNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching users");
                throw;
            }
        }

        // Method to get a user by ID
        public async Task<UserDto?> GetUserByIdAsync(Guid userId)
        {
            try
            {
                // Fetch user from the repository
                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    _logger.LogWarning($"User with ID {userId} not found");
                    return null;
                }

                // Map user to UserDto and return
                return new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DateOfBirth = user.DateOfBirth,
                    Phone = user.Phone,
                    Address = user.Address,
                    ProfilePicture = user.ProfilePicture
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching user with ID {userId}");
                throw;
            }
        }

        // Method to search users by a query
        public async Task<IEnumerable<UserDto>> SearchUsersAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return new List<UserDto>();
                }

                // Fetch users matching the search query
                var users = await _userRepository.SearchUsersAsync(searchTerm);

                // Map users to UserDto and return
                return users.Select(user => new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DateOfBirth = user.DateOfBirth,
                    Phone = user.Phone,
                    Address = user.Address,
                    ProfilePicture = user.ProfilePicture
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while searching users with term: {searchTerm}");
                throw;
            }
        }

        // Private method to fetch users from external API and cache them
        private async Task FetchAndCacheUsersFromExternalApi()
        {
            try
            {
                _logger.LogInformation("Fetching users from Random User API");
                
                // Fetch multiple pages to get a good amount of data
                var allUsers = new List<User>();
                for (int page = 1; page <= 5; page++) // Fetch 5 pages (50 users total)
                {
                    var users = await _randomUserApiService.GetUsersAsync(page, 10);
                    allUsers.AddRange(users);
                }

                // Cache the users in the database
                await _userRepository.AddOrUpdateUsersAsync(allUsers);
                
                _logger.LogInformation($"Successfully cached {allUsers.Count} users from external API");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching and caching users from external API");
                throw;
            }
        }
    }
}