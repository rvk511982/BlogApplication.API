using BlogApp.API.Models.DTOs.BlogComments;
using BlogApp.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
    //--------------------------------------------------------------------------------------------
    /// <summary>
    /// BlogComments Controller to handle end points
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCommentsController : ControllerBase
    {
        private IBlogCommentService _service;
        private readonly ILogger<BlogCommentsController> _logger;

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="logger">Logger class object</param>
        /// <param name="service">IBlogCommentService service</param>
        public BlogCommentsController(ILogger<BlogCommentsController> logger, IBlogCommentService service)
        {
            _logger = logger;
            _service = service;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get blog comment data as list
        /// </summary>
        /// <returns>Returns lists of blog comment</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllBlogCommentAsync();
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Get existing blog comment data by id
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>Returns blog comment data</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetBlogCommentByIdAsync(id);
            if (result != null)
                return Ok(result);
            else
                return NotFound("Blog comment data not found!");
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Create new blog comment record
        /// </summary>
        /// <param name="requestDto">AddBlogCommentDto requestDto</param>
        /// <returns>Returns newly added blog comment data</returns>
        [HttpPost]
        public async Task<IActionResult> Create(AddBlogCommentDto requestDto)
        {
            var result = await _service.CreateBlogCommentAsync(requestDto);
            if (result != null)
                return Ok(result);
            else
                return BadRequest("Blog comment data not added!");
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Update existing blog comment data
        /// </summary>
        /// <param name="requestDto">UpdateBlogCommentDto requestDto</param>
        /// <returns>Returns updated blog comment data</returns>
        [HttpPut]
        public async Task<IActionResult> Update(UpdateBlogCommentDto requestDto)
        {
            var result = await _service.UpdateBlogCommentAsync(requestDto);
            if (result != null)
                return Ok(result);
            else
                return BadRequest("Blog comment data not updated!");
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
            var result = await _service.DeleteBlogCommentAsync(id);
            if (result > 0)
                return Ok(new { message = "Blog comment data deleted." });
            else
                return BadRequest("Blog comment data not found!");
        }
    }
}
