// User.cs
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; } = "User"; // "Admin" или "User"
}

// Book.cs
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string FilePath { get; set; } // Путь к PDF/EPUB
    public int? UserId { get; set; } // Кто загрузил
    public User User { get; set; }
}