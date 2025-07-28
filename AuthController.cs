// AuthController.cs
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = await _authService.RegisterAsync(dto);
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _authService.LoginAsync(dto);
        return Ok(new { Token = token });
    }
}

// BooksController.cs
[Authorize]
[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    [HttpPost("upload")]
    public async Task<IActionResult> UploadBook(IFormFile file, [FromForm] string title, [FromForm] string author)
    {
        var book = await _bookService.UploadBookAsync(file, title, author, User.Identity.Name);
        return Ok(book);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        await _bookService.DeleteBookAsync(id, User.Identity.Name);
        return NoContent();
    }
}