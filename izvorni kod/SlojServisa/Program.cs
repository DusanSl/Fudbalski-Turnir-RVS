using Microsoft.EntityFrameworkCore;
using SlojPodataka.TehnoloskeKlase;
using SlojPoslovneLogike.Ogranicenja;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<TurnirDbContext>(options =>
    options.UseSqlServer(builder.Configuration
        .GetConnectionString("PodrazumevanaKonekcija")));

builder.Services.AddScoped<ZapisnikRepozitorijum>();
builder.Services.AddScoped<CitacPravila>(provider =>
    new CitacPravila(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
        "Ogranicenja", "pravila_hronologije.xml")));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();