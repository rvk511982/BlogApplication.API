using BlogApp.API.Models.DTOs.User;
using BlogApp.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
    //--------------------------------------------------------------------------------------------
    /// <summary>
    /// User Controller to handle end points related to user
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly ILogger<UserController> _logger;

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="logger">Logger class object</param>
        /// <param name="userService">IUserService userService</param>
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get User data as list
        /// </summary>
        /// <returns>Returns lists of users</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            if (users != null)
                return Ok(users);
            else
                return NotFound();
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get existing user data by id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Returns user data</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user != null)
                return Ok(user);
            else
                return NotFound("User data not found!");
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Create new user record
        /// </summary>
        /// <param name="user">AddUserDto requestDto</param>
        /// <returns>Returns newly added user data</returns>
        [HttpPost]
        public async Task<IActionResult> Create(AddUserDto requestDto)
        {
            var newUser = await _userService.CreateUserAsync(requestDto);
            if (newUser != null)
                return Ok(newUser);
            else
                return BadRequest("User data not added!");
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Update existing user data
        /// </summary>
        /// <param name="user">UpdateUserDto requestDto</param>
        /// <returns>Returns updated user data</returns>
        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserDto requestDto)
        {
            var updateUser = await _userService.UpdateUserAsync(requestDto);
            if (updateUser != null)
                return Ok(updateUser);
            else
                return BadRequest("User data not updated!");
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Delete the existing data by Id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.DeleteUserAsync(id);
            if (user > 0)
                return Ok(new { message = "User data deleted." });
            else
                return BadRequest("User data not found!");
        }
    }
}
