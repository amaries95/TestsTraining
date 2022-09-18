

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using TestsTraining.Domain.Entities;

namespace TestsTraining.Benchmark
{
	[SimpleJob(RuntimeMoniker.Net60)]
	[SimpleJob(RuntimeMoniker.Net70)]
	[MemoryDiagnoser(false)]
	public class Benchmarks
	{
		private HttpClient _httpClient;
		private List<User> users;
		private UserIdComparer _userIdComparer;

		[GlobalSetup]
		public void Setup()
		{
			_httpClient = new HttpClient();
			_userIdComparer = new UserIdComparer();

			users = new List<User>
			{
				new User {Id = 1, Name = "Dummy", UserName = "dsa", Email = "dsa@gmial.com"},
				new User {Id = 2, Name = "Dummy", UserName = "dsa", Email = "dsa@gmial.com"},
				new User {Id = 3, Name = "Dummy", UserName = "dsa", Email = "dsa@gmial.com"},
			};
		}

		[Benchmark]
		public List<User> OrderBy()
		{
			users.OrderBy(x => x.Id);

			return users;
		}
		
		[Benchmark]
		public User[] SortWithToArray()
		{
			var arrayUsers = users.ToArray();
			Array.Sort(arrayUsers);

			return arrayUsers;
		}

		// [Benchmark]
		// public async Task<HttpResponseMessage> GetAllUsers()
		// {
		// 	return await _httpClient.GetAsync("https://localhost:7098/User/");
		// }
		
		// [Benchmark]
		// public async Task<HttpResponseMessage> GetUserById()
		// {
		// 	return await _httpClient.GetAsync("https://localhost:7098/User/7");
		// }
	}

	public class UserIdComparer : IComparer<User>
	{
		public int Compare(User? x, User? y)
		{
			if (x?.Id >= y?.Id)
				return 1;
			
			if (x?.Id <= y?.Id)
				return -1;
			
			return 0;

		}
	}
}
