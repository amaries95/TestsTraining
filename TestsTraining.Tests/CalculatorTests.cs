using FluentAssertions;
using TestsTraining.Api.Calculator;
using Xunit;

namespace TestsTraining.Tests
{
	public class CalculatorTests
	{
		[Theory]
		[InlineData(1,1,1)]
		[InlineData(10,5,2)]
		public void Divide_TwoValidNumbersAreGiven_TheResultOfOperationIsReturned(int a, int b, int expectedResult)
		{
			// Arrange
			var calculator = new Calculator();

			// Act
			var result = calculator.Divide(a, b);

			// Assert
			result.Should().Be(expectedResult);
		}
	}
}
