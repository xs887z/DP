public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<User> _hasher;
    private readonly IJwtService _jwtService;

    public AuthService(AppDbContext context, IPasswordHasher<User> hasher, IJwtService jwtService)
    {
        _context = context;
        _hasher = hasher;
        _jwtService = jwtService;
    }

    public async Task<User> RegisterAsync(RegisterDto dto)
    {
        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email
        };
        user.PasswordHash = _hasher.HashPassword(user, dto.Password);

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null || _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password) == PasswordVerificationResult.Failed)
            throw new Exception("Invalid credentials");

        return _jwtService.GenerateToken(user);
    }
}