using FluentAssertions;
using TestsTraining.Data.Helpers;
using Xunit;

namespace TestsTraining.Tests
{
	public class RepositoryHelperTests
	{
		#region Internal method

		[Theory]
		[InlineData("dummyName@gmail.com", "dummyName")]
		[InlineData("", null)]
		[InlineData(" ", null)]
		public void ConvertEmailToUserName__ReturnedUserName(string email, string expectedResult)
		{
			// Arrange

			// Act
			var result = RepositoryHelper.ConvertEmailToUsername(email);

			// Assert
			((string) result).Should().Be(expectedResult);
		}


		[Theory]
		[InlineData(null)]
		public void ConvertEmailToUserNames__ReturnedUserName(string? email)
		{
			// Arrange

			// Act
			Action act = () => RepositoryHelper.ConvertEmailToUsername(email);

			// Assert
			act.Should().Throw<NullReferenceException>();
		}

		#endregion
	}
}
