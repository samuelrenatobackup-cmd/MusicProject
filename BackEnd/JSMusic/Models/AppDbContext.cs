using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JSMusic.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Artistum> Artista { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Musica> Musicas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=JSMusic;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.IdAlbum).HasName("PK__Album__BF9C2A2213C0D809");

            entity.HasMany(d => d.IdArtista).WithMany(p => p.IdAlbums)
                .UsingEntity<Dictionary<string, object>>(
                    "ArtistaAlbum",
                    r => r.HasOne<Artistum>().WithMany()
                        .HasForeignKey("IdArtista")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ArtistaAl__IdArt__59063A47"),
                    l => l.HasOne<Album>().WithMany()
                        .HasForeignKey("IdAlbum")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ArtistaAl__IdAlb__5812160E"),
                    j =>
                    {
                        j.HasKey("IdAlbum", "IdArtista").HasName("PK__ArtistaA__FF3EEBB0A481317F");
                        j.ToTable("ArtistaAlbum");
                    });

            entity.HasMany(d => d.IdMusicas).WithMany(p => p.IdAlbums)
                .UsingEntity<Dictionary<string, object>>(
                    "AlbumMusica",
                    r => r.HasOne<Musica>().WithMany()
                        .HasForeignKey("IdMusica")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__AlbumMusi__IdMus__5CD6CB2B"),
                    l => l.HasOne<Album>().WithMany()
                        .HasForeignKey("IdAlbum")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__AlbumMusi__IdAlb__5BE2A6F2"),
                    j =>
                    {
                        j.HasKey("IdAlbum", "IdMusica").HasName("PK__AlbumMus__D09E40E848E66338");
                        j.ToTable("AlbumMusica");
                    });
        });

        modelBuilder.Entity<Artistum>(entity =>
        {
            entity.HasKey(e => e.IdArtista).HasName("PK__Artista__0A2C1925D84AC417");

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Artistum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Artista__IdUsuar__4F7CD00D");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero).HasName("PK__Genero__0F834988B4062F56");

            entity.HasMany(d => d.IdMusicas).WithMany(p => p.IdGeneros)
                .UsingEntity<Dictionary<string, object>>(
                    "MusicaGenero",
                    r => r.HasOne<Musica>().WithMany()
                        .HasForeignKey("IdMusica")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__MusicaGen__IdMus__6477ECF3"),
                    l => l.HasOne<Genero>().WithMany()
                        .HasForeignKey("IdGenero")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__MusicaGen__IdGen__6383C8BA"),
                    j =>
                    {
                        j.HasKey("IdGenero", "IdMusica").HasName("PK__MusicaGe__6081234285603831");
                        j.ToTable("MusicaGenero");
                    });
        });

        modelBuilder.Entity<Musica>(entity =>
        {
            entity.HasKey(e => e.IdMusica).HasName("PK__Musica__F026ACA7E47E078C");

            entity.HasMany(d => d.IdArtista).WithMany(p => p.IdMusicas)
                .UsingEntity<Dictionary<string, object>>(
                    "ArtistaMusica",
                    r => r.HasOne<Artistum>().WithMany()
                        .HasForeignKey("IdArtista")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ArtistaMu__IdArt__60A75C0F"),
                    l => l.HasOne<Musica>().WithMany()
                        .HasForeignKey("IdMusica")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ArtistaMu__IdMus__5FB337D6"),
                    j =>
                    {
                        j.HasKey("IdMusica", "IdArtista").HasName("PK__ArtistaM__B0846D35C6AE386C");
                        j.ToTable("ArtistaMusica");
                    });
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97DD96F65F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
