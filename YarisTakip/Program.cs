using Microsoft.EntityFrameworkCore;
using YarisTakip.Data;
using YarisTakip.Helpers;
using YarisTakip.Interfaces;
using YarisTakip.Repository;
using YarisTakip.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IYarisRepository, YarisRepository>();
builder.Services.AddScoped<IResimService, ResimService>();
builder.Services.Configure<CloudinaryAyarlari>(builder.Configuration.GetSection("CloudinaryAyarlari"));
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Yaris}/{action=Index}/{id?}");

app.Run();
