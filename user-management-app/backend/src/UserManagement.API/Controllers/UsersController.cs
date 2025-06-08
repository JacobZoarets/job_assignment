// UsersController.cs
// This controller handles HTTP requests related to user management.
// It provides endpoints for getting users with pagination, fetching individual users, and searching users.

using Microsoft.AspNetCore.Mvc;
using UserManagement.Core.Interfaces;
using UserManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace UserManagement.API.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        /// <summary>
        /// Constructor to inject dependencies
        /// </summary>
        /// <param name="userService">The user service for business logic</param>
        /// <param name="logger">Logger for logging operations</param>
        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Gets a paginated list of users
        /// </summary>
        /// <param name="pageNumber">The page number (default: 1)</param>
        /// <param name="pageSize">The number of users per page (default: 10)</param>
        /// <returns>A paginated result containing users</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<UserDto>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PaginatedResult<UserDto>>> GetUsers(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                _logger.LogInformation($"Fetching users - Page: {pageNumber}, PageSize: {pageSize}");
                
                // Validate pagination parameters
                if (pageNumber < 1) pageNumber = 1;
                if (pageSize < 1 || pageSize > 100) pageSize = 10; // Limit max page size to 100

                // Fetch users with pagination
                var result = await _userService.GetUsersAsync(pageNumber, pageSize);
                
                _logger.LogInformation($"Successfully fetched {result.Items.Count} users");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching users");
                return StatusCode(500, "An error occurred while fetching users");
            }
        }

        /// <summary>
        /// Gets a specific user by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the user</param>
        /// <returns>The user details if found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            try
            {
                _logger.LogInformation($"Fetching user with ID: {id}");
                
                // Fetch a user by ID
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    _logger.LogWarning($"User with ID {id} not found");
                    return NotFound($"User with ID {id} not found");
                }
                
                _logger.LogInformation($"Successfully fetched user: {user.FirstName} {user.LastName}");
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching user with ID: {id}");
                return StatusCode(500, "An error occurred while fetching the user");
            }
        }

        /// <summary>
        /// Searches for users based on a search term
        /// </summary>
        /// <param name="query">The search term to match against user names and emails</param>
        /// <returns>A list of users matching the search criteria</returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<UserDto>>> SearchUsers([FromQuery] string query)
        {
            try
            {
                _logger.LogInformation($"Searching users with query: {query}");
                
                // Validate search query
                if (string.IsNullOrWhiteSpace(query))
                {
                    _logger.LogWarning("Search query is empty or null");
                    return BadRequest("Search query cannot be empty");
                }

                if (query.Length < 2)
                {
                    _logger.LogWarning("Search query is too short");
                    return BadRequest("Search query must be at least 2 characters long");
                }

                // Search for users based on the query
                var result = await _userService.SearchUsersAsync(query);
                
                _logger.LogInformation($"Search completed. Found {result.Count()} users matching '{query}'");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while searching users with query: {query}");
                return StatusCode(500, "An error occurred while searching users");
            }
        }
    }
}