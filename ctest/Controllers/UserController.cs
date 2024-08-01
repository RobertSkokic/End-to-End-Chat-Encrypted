using Microsoft.AspNetCore.Mvc;
using ctest.Models.dboSchema;
using Microsoft.EntityFrameworkCore;
using ctest.Models;

public class UserController : Controller
{
    private readonly Test2Context _context;

    public UserController(Test2Context context)
    {
        _context = context;
    }

    [HttpGet("api/users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Chatuser
            .Where(u => u.Valid == 1)
            .Select(u => new { u.Chatuserid, u.Username })
            .ToListAsync();

        return Ok(users);
    }
}
