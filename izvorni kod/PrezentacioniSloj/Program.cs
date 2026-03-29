using Microsoft.EntityFrameworkCore;
using SlojPodataka.TehnoloskeKlase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TurnirDbContext>(options =>
    options.UseSqlServer(builder.Configuration
        .GetConnectionString("PodrazumevanaKonekcija")));

builder.Services.AddHttpClient("FudbalskiApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7193/");
});

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Nalog}/{action=Prijava}/{id?}");

app.Run();