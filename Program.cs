using API_Neeew.Data;
using API_Neeew.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// 🟢 إعداد سلسلة الاتصال بقاعدة بيانات PostgreSQL على Railway
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🟢 إضافة الكنترولرات
builder.Services.AddControllers();

// 🟢 إعداد Swagger لتوثيق الـ API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API_Neeew",
        Version = "v1",
        Description = "User Management API for Flutter Integration"
    });
});

// 🟢 إعداد سياسة CORS للسماح بالاتصال من أي جهة (مفيد لـ Flutter)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// 🟢 تفعيل التحقق من النموذج
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = false;
});

var app = builder.Build();

// 🟢 تفعيل Swagger في جميع البيئات (اختياري للنشر)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API_Neeew v1");
    c.RoutePrefix = "swagger";
});

// 🟢 ترتيب الـ Middleware
app.UseCors("AllowAll");


app.UseHttpsRedirection();



app.UseAuthorization();


app.MapControllers();


app.Run();