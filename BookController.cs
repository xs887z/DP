[ApiController]
[Authorize]
[Route("api/books")]
[Route("api/[controller]")]
[Route("api/books")] 
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }
 

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string genre)
    {
        var books = await _bookService.GetBooksByGenreAsync(genre);
        return Ok(books);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadBook(IFormFile file, [FromForm] string title, [FromForm] string author)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var book = await _bookService.UploadBookAsync(file, title, author, userId);
        return Ok(book);
    }

    [HttpPost("admin/upload")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UploadBookAdmin(IFormFile file)
    {
        var result = await _bookService.UploadBookAsync(file);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _bookService.DeleteBookAsync(id, userId);
        return NoContent();
    }
}