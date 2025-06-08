// IUserRepository.cs
// This interface defines the contract for user data access operations.
// It outlines methods for fetching users, searching users, and handling pagination.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Core.Models;

namespace UserManagement.Core.Interfaces
{
    public interface IUserRepository
    {
        // Fetches a paginated list of users.
        Task<IEnumerable<User>> GetUsersAsync(int pageNumber, int pageSize);

        // Fetches a user by their unique identifier.
        Task<User?> GetUserByIdAsync(Guid id);

        // Searches for users based on a search term.
        Task<IEnumerable<User>> SearchUsersAsync(string searchTerm);

        // Gets the total count of users in the database
        Task<int> GetTotalUsersCountAsync();

        // Adds or updates users in the database (for caching)
        Task AddOrUpdateUsersAsync(IEnumerable<User> users);
    }
}