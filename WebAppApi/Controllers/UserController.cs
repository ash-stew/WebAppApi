using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppApi.Filters;
using WebAppApi.Models;
using WebAppApi.Services;

namespace WebAppApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private static List<UserDto> listUsers = new List<UserDto>() {
		new UserDto()
		{  Firstname = "Bill", Lastname = "Gates", Email = "bill@microsoft.com", Phone = "+111222333",
		   Address = " New York, USA"


		},
		new UserDto()
		{  Firstname = "Bob", Lastname = "Smith", Email = "bob@gmail.com", Phone = "+123456789",
		   Address = " New York, USA"


		}
		 };

	   


		[HttpGet("info")]
		[DebugFilter]
		public IActionResult GetInfo(int? id, string? name, int? page, [FromServices]IConfiguration configuration, [FromServices] TimeService timeService)
		{
			if(id != null || name != null || page != null)
			{   
				// anonomyous type
				var response = new { Id = id, Name = name, Page = page, ErrorMessage = "The search functionality is not supported yet" };

				return Ok(response);
			}
			List<string> listInfo = new List<string>();
			listInfo.Add("AppName=" + configuration["AppName"]);
			listInfo.Add("Language=" + configuration["Language"]);
			listInfo.Add("Country=" + configuration["Country"]);
			listInfo.Add("Log=" + configuration["Logging:LogLevel:Default"]);
			listInfo.Add("Date=" + timeService.GetDate());
			listInfo.Add("Time=" + timeService.GetTime());
			return Ok(listInfo);
		}

		[HttpGet]
		public IActionResult GetUsers()
		{
			if(listUsers.Count > 0)
			{
				return Ok(listUsers);
			}
			return NoContent();
		}

		[HttpGet("{id:int}")]
		public IActionResult GetUser(int id)
		{
			if(id >=0 &&  id < listUsers.Count)
			{
				return Ok(listUsers[id]);
			}
			return NotFound();
		}

		[HttpGet("{name}")]
		public IActionResult GetUser(string name)
		{
			var user = listUsers.FirstOrDefault(u => u.Firstname.Contains(name) || u.Lastname.Contains(name));
			
			if(user == null)
			{
				return NotFound();
			}
			
			return Ok(user);
		}

		[HttpPost]
		public IActionResult AddUser(UserDto user)
		{
			if(user.Email.Equals("user@example.com"))
			{
				ModelState.AddModelError("Email", "This Email Address is unauthorized!");
				return BadRequest(ModelState);
			}
			listUsers.Add(user);
			return Ok();
		}

		[HttpPut("{id}")]
		public IActionResult UpdateUser(int id, UserDto user)
		{
			if (user.Email.Equals("user@example.com"))
			{
				ModelState.AddModelError("Email", "This Email Address is unauthorized!");
				return BadRequest(ModelState);
			}

			if (id >=0 && id < listUsers.Count)
			{
				listUsers[id] = user;
			}
			return Ok();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteUser(int id)
		{
			if(id >= 0 && id < listUsers.Count)
			{
				listUsers.RemoveAt(id);
			}
			return NoContent();
		}
	}
}
