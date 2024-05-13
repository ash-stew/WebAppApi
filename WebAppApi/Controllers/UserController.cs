using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAppApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private static List<string> listUsers = new List<string>() { "Bob", "Bill", "Will", "Dom" };

		[HttpGet]
		public List<string> GetUsers()
		{
			return listUsers;
		}

		[HttpGet("{id}")]
		public string GetUser(int id)
		{
			if(id >=0 &&  id < listUsers.Count)
			{
				return listUsers[id];
			}
			return "";
		}

		[HttpPost]
		public void AddUser(string username)
		{
			listUsers.Add(username);
		}

		[HttpPut("{id}")]
		public void UpdateUser(int id, string username)
		{
			if(id >=0 && id < listUsers.Count)
			{
				listUsers[id] = username;
			}
			
		}

		[HttpDelete("{id}")]
		public void DeleteUser(int id)
		{
			listUsers.RemoveAt(id);
		}
	}
}
