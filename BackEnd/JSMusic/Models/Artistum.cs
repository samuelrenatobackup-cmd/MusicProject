using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JSMusic.Models;

[Index("IdUsuario", Name = "UQ__Artista__5B65BF96D05B0CB8", IsUnique = true)]
public partial class Artistum
{
    [Key]
    public int IdArtista { get; set; }

    public int IdUsuario { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("Artistum")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    [ForeignKey("IdArtista")]
    [InverseProperty("IdArtista")]
    public virtual ICollection<Album> IdAlbums { get; set; } = new List<Album>();

    [ForeignKey("IdArtista")]
    [InverseProperty("IdArtista")]
    public virtual ICollection<Musica> IdMusicas { get; set; } = new List<Musica>();
}
