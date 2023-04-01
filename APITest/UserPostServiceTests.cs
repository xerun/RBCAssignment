using Shouldly;
using System.Text.Json;
using API.Models;
using System.Net;
using System.Text;
using Moq;
using API.Services;
using API.Helper;
using API.Utilities;

namespace APITest
{
    public class UserPostServiceTests
    {
        private readonly Mock<IHttpClientWrapper> _httpClientWrapperMock;
        private readonly UserPostService _userPostService;

        public UserPostServiceTests()
        {
            _httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            _userPostService = new UserPostService(_httpClientWrapperMock.Object);
        }

        // Test that GetAllUserPost returns all user posts when they exist in the API
        [Fact]
        public async Task GetAllUserPost_ReturnsListOfUserPosts()
        {
            // Arrange
            var expectedPosts = new List<UserPost>
            {
                new UserPost { Id = 1, UserId = 1, Title = "Test Post 1", Body = "This is a test post." },
                new UserPost { Id = 2, UserId = 1, Title = "Test Post 2", Body = "This is another test post." }
            };

            var serialized = JsonSerializer.Serialize(expectedPosts);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(serialized, Encoding.UTF8, "application/json")
            };
            _httpClientWrapperMock.Setup(x => x.GetAsync(It.IsAny<string>()))
                                  .ReturnsAsync(response);

            // Act
            var actualPosts = await _userPostService.GetAllUserPost();

            // Assert
            actualPosts.ShouldNotBeNull();
            actualPosts.Count.ShouldBe(expectedPosts.Count);            
        }


        // Test that GetAllUserPost returns an empty list when no user posts exist in the API
        [Fact]
        public async Task GetAllUserPost_ReturnsEmptyList_WhenResponseIsNull()
        {
            // Arrange
            string? nullJson = string.Empty;
            var serialized = JsonSerializer.Serialize(nullJson);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(serialized, Encoding.UTF8, "application/json")
            };

            _httpClientWrapperMock.Setup(x => x.GetAsync(It.IsAny<string>()))
                                  .ReturnsAsync(response);

            // Act
            var actualPosts = await _userPostService.GetAllUserPost();

            // Assert
            actualPosts.ShouldNotBeNull();
            actualPosts.ShouldBeEmpty();
        }

        // Test that GetAllUserPost returns data in the correct format (JSON)
        [Fact]
        public async Task GetAllUserPost_ReturnsEmptyList_WhenDeserializationFails()
        {
            // Arrange
            var invalidJson = "{ 'id': 1 }";
            var serialized = JsonSerializer.Serialize(invalidJson);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(serialized, Encoding.UTF8, "application/json")
            };
            _httpClientWrapperMock.Setup(x => x.GetAsync(It.IsAny<string>()))
                                  .ReturnsAsync(response);

            // Act
            var actualPosts = await _userPostService.GetAllUserPost();

            // Assert
            actualPosts.ShouldNotBeNull();
            actualPosts.ShouldBeEmpty();
        }

        // Test that GetAllUserPost returns correct data types for all properties
        [Fact]
        public async Task GetAllUserPost_Returns_Correct_Data_Types_For_All_Properties()
        {
            // Arrange
            var userPosts = new List<UserPost>
            {
                new UserPost { Id = 1, UserId = 1, Title = "title 1", Body = "body 1" },
                new UserPost { Id = 2, UserId = 1, Title = "title 2", Body = "body 2" },
                new UserPost { Id = 3, UserId = 2, Title = "title 3", Body = "body 3" },
            };
            var serialized = JsonSerializer.Serialize(userPosts);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(serialized, Encoding.UTF8, "application/json")
            };
            _httpClientWrapperMock.Setup(x => x.GetAsync(It.IsAny<string>()))
                                  .ReturnsAsync(response);

            // Act
            var result = await _userPostService.GetAllUserPost();

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<List<UserPost>>();
            result.Count.ShouldBe(userPosts.Count);
            foreach (var userPost in result)
            {
                userPost.Id.ShouldBeInRange(1, int.MaxValue);
                userPost.UserId.ShouldBeInRange(1, int.MaxValue);
                userPost.Title.ShouldNotBeNullOrEmpty();
                userPost.Body.ShouldNotBeNullOrEmpty();
            }
        }

        // Test that GetAllUserPost returns an empty list when the API is unavailable
        [Fact]
        public async Task GetAllUserPost_Returns_Empty_List_When_API_Is_Unavailable()
        {
            // Arrange
            _httpClientWrapperMock.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new Exception());

            // Act
            var result = await _userPostService.GetAllUserPost();

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeEmpty();
        }

        // Test that GetAllUserPost returns an empty list when the API returns an error status code (e.g. 500 Internal Server Error)
        [Fact]
        public async Task GetAllUserPost_Returns_Empty_List_When_API_Returns_Error_Status_Code()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            };
            _httpClientWrapperMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(httpResponseMessage);

            // Act
            var result = await _userPostService.GetAllUserPost();

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeEmpty();
        }

    }

}