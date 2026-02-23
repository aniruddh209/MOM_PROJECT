using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =============================
// ADD SERVICES
// =============================

// MVC (Controllers + Views)
builder.Services.AddControllersWithViews();

// OPTIONAL: If you use Session anywhere
builder.Services.AddSession();

// OPTIONAL: If you use Authentication later
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// OPTIONAL: If you use DbContext (keep if already using EF somewhere)
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// =============================
// MIDDLEWARE PIPELINE
// =============================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// HTTPS
app.UseHttpsRedirection();

// Static files (CSS, JS, Images)
app.UseStaticFiles();

// Routing
app.UseRouting();

// Session (must be BEFORE Authorization)
app.UseSession();

// Auth
app.UseAuthentication();
app.UseAuthorization();

// =============================
// ROUTES
// =============================

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// =============================
// RUN APP
// =============================
app.Run();