// IUserService.cs
// This interface defines the contract for user-related services in the application.
// It outlines the methods that must be implemented for managing user data.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Core.DTOs;

namespace UserManagement.Core.Interfaces
{
    public interface IUserService
    {
        // Fetches a paginated list of users.
        Task<PaginatedResult<UserDto>> GetUsersAsync(int pageNumber, int pageSize);

        // Fetches a user by their unique identifier.
        Task<UserDto?> GetUserByIdAsync(Guid userId);

        // Searches for users based on a search term.
        Task<IEnumerable<UserDto>> SearchUsersAsync(string searchTerm);

        // Additional methods for user management can be added here.
    }
}