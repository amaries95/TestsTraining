using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TestsTraining.Data;
using TestsTraining.Data.Repositories;
using TestsTraining.Domain.Entities;
using TestsTraining.Domain.Interfaces;
using Xunit;

namespace TestsTraining.Tests
{
	public class UserControllerIntegrationTests
	{
		private readonly WebApplicationFactory<Program> appFactory;
		
		public UserControllerIntegrationTests()
		{
			appFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
			{
				builder.ConfigureServices(configureServices =>
				{
					configureServices.AddScoped<IUserRepository, UserRepository>();
					configureServices.RemoveAll(typeof(ProjectProjectDbContext));
					configureServices.AddDbContext<ProjectProjectDbContext>();
				});
			});
		}

		[Fact]
		public async Task GetAllUsers_UsersListNotEmpty_ReturnedListOfUsersAndOkResponse()
		{
			// Arrange
			var client = appFactory.CreateClient();

			// Act
			var response = await client.GetAsync("User/");

			// Assert
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			var content = await response.Content.ReadFromJsonAsync<List<User>>();
			content.Should().BeOfType<List<User>>();
		}
	}
}
