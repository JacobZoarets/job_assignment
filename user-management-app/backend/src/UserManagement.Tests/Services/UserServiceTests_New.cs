// UserServiceTests.cs
// Unit tests for the UserService class to ensure business logic is working correctly.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using UserManagement.Core.DTOs;
using UserManagement.Core.Interfaces;
using UserManagement.Core.Models;
using UserManagement.Infrastructure.Services;
using Xunit;

namespace UserManagement.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<ILogger<UserService>> _mockLogger;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockLogger = new Mock<ILogger<UserService>>();
            
            // Create a mock HttpClient for RandomUserApiService
            var mockHttpClient = new System.Net.Http.HttpClient();
            var randomUserApiService = new RandomUserApiService(mockHttpClient);
            
            _userService = new UserService(
                _mockUserRepository.Object,
                randomUserApiService,
                _mockLogger.Object);
        }

        [Fact]
        public async Task GetUsersAsync_WhenCacheHasUsers_ShouldReturnPaginatedResult()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "john@example.com" },
                new User { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith", Email = "jane@example.com" }
            };

            _mockUserRepository.Setup(r => r.GetTotalUsersCountAsync()).ReturnsAsync(10);
            _mockUserRepository.Setup(r => r.GetUsersAsync(1, 2)).ReturnsAsync(users);

            // Act
            var result = await _userService.GetUsersAsync(1, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Items.Count);
            Assert.Equal(10, result.TotalCount);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(2, result.PageSize);
        }

        [Fact]
        public async Task GetUserByIdAsync_WhenUserExists_ShouldReturnUserDto()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User 
            { 
                Id = userId, 
                FirstName = "John", 
                LastName = "Doe", 
                Email = "john@example.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Phone = "123-456-7890",
                Address = "123 Main St",
                ProfilePicture = "http://example.com/pic.jpg"
            };

            _mockUserRepository.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            Assert.Equal("john@example.com", result.Email);
        }

        [Fact]
        public async Task GetUserByIdAsync_WhenUserDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockUserRepository.Setup(r => r.GetUserByIdAsync(userId)).ReturnsAsync((User)null);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task SearchUsersAsync_WithValidSearchTerm_ShouldReturnMatchingUsers()
        {
            // Arrange
            var searchTerm = "John";
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "john@example.com" },
                new User { Id = Guid.NewGuid(), FirstName = "Johnny", LastName = "Smith", Email = "johnny@example.com" }
            };

            _mockUserRepository.Setup(r => r.SearchUsersAsync(searchTerm)).ReturnsAsync(users);

            // Act
            var result = await _userService.SearchUsersAsync(searchTerm);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task SearchUsersAsync_WithEmptySearchTerm_ShouldReturnEmptyList()
        {
            // Act
            var result = await _userService.SearchUsersAsync("");

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task SearchUsersAsync_WithNullSearchTerm_ShouldReturnEmptyList()
        {
            // Act
            var result = await _userService.SearchUsersAsync(null);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
