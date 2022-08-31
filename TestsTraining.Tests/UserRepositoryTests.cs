using System.Reflection;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using TestsTraining.Data;
using TestsTraining.Data.Repositories;
using TestsTraining.Domain.Entities;
using Xunit;

namespace TestsTraining.Tests
{
	public class UserRepositoryTest
	{
		private readonly Mock<ProjectProjectDbContext> mockProjectDbContext;
		private readonly Mock<DbSet<User>> mockDbSet;
		private readonly DbContextOptions<ProjectProjectDbContext> _contextOptions;

		public UserRepositoryTest()
		{
			mockDbSet = new List<User>
			{
				new() {Id = 0, Name = "DummyName1", Email = "DummyMail@mail.com"},
				new() {Id = 1, Name = "DummyName2", Email = "bla@mail.com"}
			}.AsQueryable().BuildMockDbSet();

			_contextOptions = new DbContextOptionsBuilder<ProjectProjectDbContext>().
				UseInMemoryDatabase("Products Test").
				Options;
			mockProjectDbContext = new Mock<ProjectProjectDbContext>(_contextOptions);

			mockProjectDbContext.Setup(c => c.Users).Returns(mockDbSet.Object!);
		}

		#region Public method

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		public async Task GetUserById_AValidIdIsGiven_ReturnedUserObjectHavingSpecifiedId(int id)
		{
			// Arrange
			var repo = new UserRepository(mockProjectDbContext.Object);

			// Act
			var result = await repo.GetUserById(id);

			// Assert
			result.Should().Be(mockDbSet.Object.ElementAt(id));
		}

		#endregion

		#region Private method

		[Fact]
		public async Task GetNumberOfEntries_UserListIsNotNull_ReturnedNumberOfElements()
		{
			// Arrange
			var repo = new UserRepository(mockProjectDbContext.Object);

			MethodInfo? method = repo.GetType()
				.GetMethod("GetNumberOfEntries", BindingFlags.Instance | BindingFlags.NonPublic);

			// Act
			dynamic task = method!.Invoke(repo, null)!;

			// Assert
			await task;
			var result = (int)task.GetAwaiter().GetResult();
			result.Should().Be(mockDbSet.Object.Count());
		}

		#endregion

		#region Void insert method

		[Fact]
		public async Task AddNewUser_AValidUserObjectIsGiven_AddMethodSuccessfullyCalledOnce()
		{
			// Arrange
			var user = new User { Name = "DummyName2", Email = "DummyMail2@mail.com" };
			var repo = new UserRepository(mockProjectDbContext.Object);

			// Act
			await repo.AddNewUser(user);

			// Assert
			mockProjectDbContext.Verify(ctx => ctx.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
			mockProjectDbContext.Verify(ctx => ctx.Users.Add(It.IsAny<User>()), Times.Once);
		}
		
		[Fact]
		public async Task AddNewUser_AValidUserObjectIsGiven_NewUserIsSuccessfullyAdded()
		{
			// Arrange
			var user = new User { Name = "DummyName2", Email = "DummyMail2@mail.com" };
			var dbContext = new ProjectProjectDbContext(_contextOptions);

			var repo = new UserRepository(dbContext);

			// Assume
			dbContext.Users.Should().NotContain(user);

			// Act
			await repo.AddNewUser(user);

			// Assert
			dbContext.Users.Should().Contain(user);
		}

		#endregion
	}
}