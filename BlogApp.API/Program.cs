using BlogApp.API.Data;
using BlogApp.API.Repositories;
using BlogApp.API.Repositories.Interface;
using BlogApp.API.Services;
using BlogApp.API.Services.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database connection
var connectionString = builder.Configuration.GetConnectionString("DBConeection");
builder.Services.AddDbContext<BlogDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IBlogCommentRepository, BlogCommentRepository>();

// Services
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IBlogService, BlogService>();
builder.Services.AddTransient<IBlogCommentService, BlogCommentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
