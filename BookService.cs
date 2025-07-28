// BookService.cs
public class BookService : IBookService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public async Task<Book> UploadBookAsync(IFormFile file, string title, string author, string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        
        // Сохраняем файл
        var uploadsDir = Path.Combine(_env.WebRootPath, "books");
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadsDir, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var book = new Book { 
            Title = title, 
            Author = author,
            FilePath = $"/books/{fileName}",
            UserId = user.Id
        };

        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();

        return book;
    }

    public async Task DeleteBookAsync(int id, string username)
    {
        var book = await _context.Books
            .Include(b => b.User)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null) throw new Exception("Книга не найдена");
        if (book.User.Username != username && !User.IsInRole("Admin")) 
            throw new Exception("Нет прав на удаление");

        // Удаляем файл
        var filePath = Path.Combine(_env.WebRootPath, book.FilePath.TrimStart('/'));
        if (System.IO.File.Exists(filePath))
            System.IO.File.Delete(filePath);

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }
}