using Microsoft.EntityFrameworkCore;
using SlojPodataka.TehnoloskeKlase;
using SlojPoslovneLogike.Ogranicenja;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<TurnirDbContext>(options =>
    options.UseSqlServer(builder.Configuration
        .GetConnectionString("PodrazumevanaKonekcija")));

Konekcija.NizKonekcije = builder.Configuration.GetConnectionString("PodrazumevanaKonekcija")!;

builder.Services.AddScoped<ZapisnikRepozitorijum>();
builder.Services.AddScoped<KorisnikRepozitorijum>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<CitacPravila>();
builder.Services.AddScoped<SlojPoslovneLogike.Validacija.PoslovnoPraviloValidator>();
builder.Services.AddScoped<KlubRepoDBUtils>();

var app = builder.Build();

using (var opseg = app.Services.CreateScope())
{
    var kontekst = opseg.ServiceProvider.GetRequiredService<TurnirDbContext>();
    var putanja = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
        "..", "..", "..", "..", "SlojPodataka", "XML", "sifrarnik_podaci.xml");
    PocetniPodaci.PopuniSve(kontekst, putanja);
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();