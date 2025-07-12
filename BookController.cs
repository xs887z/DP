[ApiController]
[Route("api/[controller]")]
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UploadBook(IFormFile file)
    {
        var result = await _bookService.UploadBookAsync(file);
        return Ok(result);
    }
}