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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (existingUser != null)
            {
                return BadRequest("البريد الإلكتروني مستخدم مسبقًا");
            }

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password // يُفضل تشفيرها لاحقًا
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "تم إنشاء الحساب بنجاح", user });
        }
    }
}