
using JSMusic.Interfaces;
using JSMusic.Repositories;
using JSMusic.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Banco de dados
builder.Services.AddDbContext<AppDbContext>();

// Repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IArtistaRepository, ArtistaRepository>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<IMusicaRepository, MusicaRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();

// Repositories de associação
builder.Services.AddScoped<IAlbumMusicaRepository, AlbumMusicaRepository>();
builder.Services.AddScoped<IArtistaAlbumRepository, ArtistaAlbumRepository>();
builder.Services.AddScoped<IArtistaMusicaRepository, ArtistaMusicaRepository>();
builder.Services.AddScoped<IMusicaGeneroRepository, MusicaGeneroRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
