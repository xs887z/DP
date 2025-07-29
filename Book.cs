public interface IBook
{
    global::System.Int32 Id { get; set; }
    global::System.String Title { get; set; }
    global::System.String Author { get; set; }
    global::System.String FilePath { get; set; }
    global::System.String UserId { get; set; }
}

public class Book : IBook
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string FilePath { get; set; }
    public string UserId { get; set; }
}

