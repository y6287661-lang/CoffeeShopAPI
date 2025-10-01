using API_Neeew.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// ✅ تسجيل الخدمات الأساسية
builder.Services.AddControllers();

// ✅ تفعيل Swagger لتوثيق الـ API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ تسجيل DbContext مع SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    ));

// ✅ تفعيل CORS للسماح بالوصول من تطبيقات خارجية
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// ✅ تحسين التحقق من المدخلات
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = false;
});

var app = builder.Build();

// ✅ تفعيل Swagger فقط في بيئة التطوير
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ تفعيل CORS
app.UseCors("AllowAll");

// ✅ تفعيل HTTPS
app.UseHttpsRedirection();

// ✅ تفعيل التفويض (Authorization)
app.UseAuthorization();

// ✅ ربط الـ Controllers
app.MapControllers();

// ✅ تشغيل التطبيق
app.Run();