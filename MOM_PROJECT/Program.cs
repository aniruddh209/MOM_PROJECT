using Microsoft.EntityFrameworkCore;
using MOM_PROJECT.Data;

var builder = WebApplication.CreateBuilder(args);

// ================= ADD SERVICES =================
builder.Services.AddControllersWithViews();

// ✅ ADD THESE TWO LINES HERE (BEFORE builder.Build)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSession();

var app = builder.Build();

// ================= MIDDLEWARE =================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ ADD SESSION HERE (BEFORE Authorization)
app.UseSession();

app.UseAuthorization();

// ================= ROUTING =================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();