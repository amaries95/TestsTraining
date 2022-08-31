using Microsoft.AspNetCore.Mvc;
using TestsTraining.Domain.Entities;
using TestsTraining.Domain.Interfaces;

namespace TestsTraining.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var response = await _userRepository.GetUserById(id);

            return Ok(response);
        }
        
        [HttpGet("")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userRepository.GetAllUsers();

            return Ok(response);
        }

        [HttpPost("")]
        public async Task<IActionResult> Add(User createUserRequest)
        {
            await _userRepository.AddNewUser(createUserRequest);

            return Ok();
        }
    }

   

}
