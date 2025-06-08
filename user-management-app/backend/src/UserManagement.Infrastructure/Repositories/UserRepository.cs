// UserRepository.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Interfaces;
using UserManagement.Core.Models;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Repositories
{
    // UserRepository implements the IUserRepository interface for data access related to users
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        // Constructor that accepts the ApplicationDbContext for dependency injection
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Fetches users with pagination
        public async Task<IEnumerable<User>> GetUsersAsync(int pageNumber, int pageSize)
        {
            // Returns a paginated list of users
            return await _context.Users
                .Skip((pageNumber - 1) * pageSize) // Skip users based on the current page
                .Take(pageSize) // Take only the number of users specified by pageSize
                .ToListAsync(); // Asynchronously convert the result to a list
        }

        // Fetches a user by their unique identifier
        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            // Finds a user by their ID and returns it
            return await _context.Users.FindAsync(id);
        }

        // Searches for users based on a search term
        public async Task<IEnumerable<User>> SearchUsersAsync(string searchTerm)
        {
            // Filters users based on the search term in their first or last name or email
            return await _context.Users
                .Where(u => u.FirstName.Contains(searchTerm) || 
                           u.LastName.Contains(searchTerm) ||
                           u.Email.Contains(searchTerm))
                .ToListAsync(); // Asynchronously convert the result to a list
        }

        // Gets the total count of users in the database
        public async Task<int> GetTotalUsersCountAsync()
        {
            return await _context.Users.CountAsync();
        }

        // Adds or updates users in the database (for caching external API data)
        public async Task AddOrUpdateUsersAsync(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                var existingUser = await _context.Users.FindAsync(user.Id);
                if (existingUser == null)
                {
                    await _context.Users.AddAsync(user);
                }
                else
                {
                    // Update existing user properties
                    existingUser.FirstName = user.FirstName;
                    existingUser.LastName = user.LastName;
                    existingUser.Email = user.Email;
                    existingUser.DateOfBirth = user.DateOfBirth;
                    existingUser.Phone = user.Phone;
                    existingUser.Address = user.Address;
                    existingUser.ProfilePicture = user.ProfilePicture;
                }
            }
            await _context.SaveChangesAsync(); // Saves changes to the database
        }
    }
}