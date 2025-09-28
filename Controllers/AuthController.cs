using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthAPI.Data;
using AuthAPI.Models;
using AuthAPI.Dtos;
using AuthAPI.Helpers;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("البريد الإلكتروني مستخدم مسبقًا");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = PasswordHasher.Hash(dto.Password!)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return StatusCode(201, "تم إنشاء الحساب بنجاح");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !PasswordHasher.Verify(dto.Password!, user.PasswordHash!))
                return Unauthorized("بيانات الدخول غير صحيحة");

            return Ok(new { user.Id, user.Name, user.Email });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users
                .Select(u => new { u.Id, u.Name, u.Email })
                .ToListAsync();
            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound("المستخدم غير موجود");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok("تم حذف المستخدم");
        }
    }
}