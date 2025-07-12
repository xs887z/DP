public class BookService : IBookService
{
    private readonly AppDbContext _context;

    public BookService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAllBooksAsync()
    {
        return await _context.Books.Include(b => b.Reviews).ToListAsync();
    }

    public async Task<Book> UploadBookAsync(IFormFile file)
    {
        var book = new Book
        {
            Title = Path.GetFileNameWithoutExtension(file.FileName),
            FilePath = $"uploads/{Guid.NewGuid()}{Path.GetExtension(file.FileName)}"
        };

        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", book.FilePath);
        using (var stream = new FileStream(uploadPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();

        return book;
    }
}