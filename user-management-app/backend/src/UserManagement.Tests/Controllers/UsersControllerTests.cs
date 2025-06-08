using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.API.Controllers;
using UserManagement.Core.DTOs;
using UserManagement.Core.Interfaces;
using Xunit;

namespace UserManagement.Tests.Controllers
{
    public class UsersControllerTests
    {
        private readonly UsersController _controller;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<ILogger<UsersController>> _loggerMock;

        public UsersControllerTests()
        {
            // Initialize the mocks for the user service and logger
            _userServiceMock = new Mock<IUserService>();
            _loggerMock = new Mock<ILogger<UsersController>>();

            // Create an instance of the UsersController with the mocked dependencies
            _controller = new UsersController(_userServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetUsers_ReturnsOkResult_WithPaginatedUsers()
        {
            // Arrange: Set up a paginated result of mock users to be returned by the service
            var users = new List<UserDto>
            {
                new UserDto { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "john@example.com", DateOfBirth = DateTime.Now.AddYears(-25), Phone = "123-456-7890", Address = "123 Main St", ProfilePicture = "pic1.jpg" },
                new UserDto { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", Email = "jane@example.com", DateOfBirth = DateTime.Now.AddYears(-30), Phone = "098-765-4321", Address = "456 Oak Ave", ProfilePicture = "pic2.jpg" }
            };
            var paginatedResult = new PaginatedResult<UserDto>(
                users, // items
                2,     // totalCount  
                10,    // pageSize
                1      // currentPage
            );
            _userServiceMock.Setup(service => service.GetUsersAsync(1, 10)).ReturnsAsync(paginatedResult);

            // Act: Call the GetUsers method
            var result = await _controller.GetUsers();

            // Assert: Verify the result is an OkObjectResult containing the paginated result
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<PaginatedResult<UserDto>>(okResult.Value);
            Assert.Equal(users.Count, returnValue.Items.Count);
        }

        [Fact]
        public async Task GetUserById_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange: Set up the user service to return null for a non-existent user
            var userId = Guid.NewGuid();
            _userServiceMock.Setup(service => service.GetUserByIdAsync(userId)).ReturnsAsync((UserDto?)null);

            // Act: Call the GetUserById method
            var result = await _controller.GetUserById(userId);

            // Assert: Verify the result is a NotFoundResult
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetUserById_ReturnsOkResult_WithUser()
        {
            // Arrange: Set up the expected user result
            var userId = Guid.NewGuid();
            var user = new UserDto 
            { 
                Id = userId, 
                FirstName = "John", 
                LastName = "Doe", 
                Email = "john@example.com", 
                DateOfBirth = DateTime.Now.AddYears(-25), 
                Phone = "123-456-7890", 
                Address = "123 Main St", 
                ProfilePicture = "pic1.jpg" 
            };
            _userServiceMock.Setup(service => service.GetUserByIdAsync(userId)).ReturnsAsync(user);

            // Act: Call the GetUserById method
            var result = await _controller.GetUserById(userId);

            // Assert: Verify the result is an OkObjectResult containing the user
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<UserDto>(okResult.Value);
            Assert.Equal(userId, returnValue.Id);
        }

        [Fact]
        public async Task SearchUsers_ReturnsOkResult_WithFilteredUsers()
        {
            // Arrange: Set up the expected result for a search
            var searchTerm = "John";
            var users = new List<UserDto>
            {
                new UserDto { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "john@example.com", DateOfBirth = DateTime.Now.AddYears(-25), Phone = "123-456-7890", Address = "123 Main St", ProfilePicture = "pic1.jpg" },
                new UserDto { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", Email = "jane@example.com", DateOfBirth = DateTime.Now.AddYears(-30), Phone = "098-765-4321", Address = "456 Oak Ave", ProfilePicture = "pic2.jpg" }
            };
            _userServiceMock.Setup(service => service.SearchUsersAsync(searchTerm)).ReturnsAsync(users);

            // Act: Call the SearchUsers method
            var result = await _controller.SearchUsers(searchTerm);

            // Assert: Verify the result is an OkObjectResult containing the filtered users
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<UserDto>>(okResult.Value);
            Assert.Equal(users.Count, returnValue.Count());
        }
    }
}