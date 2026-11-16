using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JSMusic.Models;

[Table("Musica")]
public partial class Musica
{
    [Key]
    public int IdMusica { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    public int Duracao { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string UrlAudio { get; set; } = null!;

    [ForeignKey("IdMusica")]
    [InverseProperty("IdMusicas")]
    public virtual ICollection<Album> IdAlbums { get; set; } = new List<Album>();

    [ForeignKey("IdMusica")]
    [InverseProperty("IdMusicas")]
    public virtual ICollection<Artistum> IdArtista { get; set; } = new List<Artistum>();

    [ForeignKey("IdMusica")]
    [InverseProperty("IdMusicas")]
    public virtual ICollection<Genero> IdGeneros { get; set; } = new List<Genero>();
}
