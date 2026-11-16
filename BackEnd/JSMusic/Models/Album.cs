using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JSMusic.Models;

[Table("Album")]
public partial class Album
{
    [Key]
    public int IdAlbum { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    public DateOnly? DataLancamento { get; set; }

    [ForeignKey("IdAlbum")]
    [InverseProperty("IdAlbums")]
    public virtual ICollection<Artistum> IdArtista { get; set; } = new List<Artistum>();

    [ForeignKey("IdAlbum")]
    [InverseProperty("IdAlbums")]
    public virtual ICollection<Musica> IdMusicas { get; set; } = new List<Musica>();
}
