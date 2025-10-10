using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Neeew.Data;
using API_Neeew.Models;
using API_Neeew.DTOs;

namespace API_Neeew.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == dto.Password);

            if (user == null)
                return Unauthorized(new { message = "بيانات الدخول غير صحيحة" });

            return Ok(new
            {
                message = "تم تسجيل الدخول بنجاح",
                user = new { user.Id, user.Name, user.Email }
            });
        }
    }
}