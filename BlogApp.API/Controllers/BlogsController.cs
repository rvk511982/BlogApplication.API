using BlogApp.API.Models.DTOs.BlogPost;
using BlogApp.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
    //--------------------------------------------------------------------------------------------
    /// <summary>
    /// Blogs Controller to handle end points
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private IBlogService _service;
        private readonly ILogger<BlogsController> _logger;

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="logger">Logger class object</param>
        /// <param name="service">IBlogService service</param>
        public BlogsController(ILogger<BlogsController> logger, IBlogService service)
        {
            _logger = logger;
            _service = service;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get blog data as list
        /// </summary>
        /// <returns>Returns lists of blog</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllBlogAsync();
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get existing blog data by id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Returns blog data</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetBlogByIdAsync(id);
            if (result != null)
                return Ok(result);
            else
                return NotFound("Blog data not found!");
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Create new blog record
        /// </summary>
        /// <param name="requestDto">AddBlogDto requestDto</param>
        /// <returns>Returns newly added blog data</returns>
        [HttpPost]
        public async Task<IActionResult> Create(AddBlogDto requestDto)
        {
            var result = await _service.CreateBlogAsync(requestDto);
            if (result != null)
                return Ok(result);
            else
                return BadRequest("Blog data not added!");
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Update existing blog data
        /// </summary>
        /// <param name="requestDto">UpdateBlogDto requestDto</param>
        /// <returns>Returns updated blog data</returns>
        [HttpPut]
        public async Task<IActionResult> Update(UpdateBlogDto requestDto)
        {
            var result = await _service.UpdateBlogAsync(requestDto);
            if (result != null)
                return Ok(result);
            else
                return BadRequest("Blog data not updated!");
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
            var result = await _service.DeleteBlogAsync(id);
            if (result > 0)
                return Ok(new { message = "Blog data deleted." });
            else
                return BadRequest("Blog data not found!");
        }
    }
}
