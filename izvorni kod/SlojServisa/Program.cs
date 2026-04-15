using Microsoft.EntityFrameworkCore;
using SlojPodataka.TehnoloskeKlase;
using SlojPoslovneLogike.Ogranicenja;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<TurnirDbContext>(options =>
    options.UseSqlServer(builder.Configuration
        .GetConnectionString("PodrazumevanaKonekcija")));

builder.Services.AddScoped<ZapisnikRepozitorijum>();
builder.Services.AddHttpClient();

builder.Services.AddScoped<SlojPoslovneLogike.Ogranicenja.CitacPravila>();

builder.Services.AddScoped<SlojPoslovneLogike.Validacija.PoslovnoPraviloValidator>();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();