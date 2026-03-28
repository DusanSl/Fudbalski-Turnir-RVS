using Microsoft.EntityFrameworkCore;
using SlojPodataka.TehnoloskeKlase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<TurnirDbContext>(options =>
    options.UseSqlServer(builder.Configuration
        .GetConnectionString("PodrazumevanaKonekcija")));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();