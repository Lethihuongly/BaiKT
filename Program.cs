using GoodsManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ Controllers với cấu hình JSON không thay đổi tên thuộc tính
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Trả về JSON với tên thuộc tính gốc
    });

// Kết nối CSDL từ chuỗi kết nối trong appsettings.json
builder.Services.AddDbContext<GoodsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Hỗ trợ Swagger UI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Goods Management API",
        Version = "v1"
    });
});

// Cấu hình CORS (Cho phép tất cả nguồn gốc - có thể tùy chỉnh)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Kích hoạt CORS trước khi xử lý request
app.UseCors("AllowAll");

// Bật phục vụ file tĩnh từ thư mục wwwroot
app.UseStaticFiles();

// Bật Swagger UI luôn (không cần kiểm tra môi trường)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Goods Management API V1");
    c.RoutePrefix = "swagger"; // Để Swagger UI có thể truy cập tại /swagger
});

// Bật chuyển hướng HTTPS
app.UseHttpsRedirection();

// Bật xác thực nếu cần
app.UseAuthorization();

// Map API Controllers
app.MapControllers();

// Chạy ứng dụng
app.Run();