namespace TestsTraining.Data.Helpers
{
	internal static class RepositoryHelper
	{
		internal static string? ConvertEmailToUsername(string email)
		{
			var emailSplit = email.Split('@');
			return emailSplit.FirstOrDefault( x=>!string.IsNullOrEmpty(x) && !string.IsNullOrWhiteSpace(x));
		}
	}
}
