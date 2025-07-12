public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
    public ICollection<Bookmark> Bookmarks { get; set; }
    public ICollection<Review> Reviews { get; set; }
}

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public string FilePath { get; set; } 
    public ICollection<Review> Reviews { get; set; }
}

public class Review
{
    public int Id { get; set; }
    public string Text { get; set; }
    public int Rating { get; set; } 
    public int UserId { get; set; }
    public User User { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
}

public class Bookmark
{
    public int Id { get; set; }
    public int PageNumber { get; set; }
    public string Notes { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
}