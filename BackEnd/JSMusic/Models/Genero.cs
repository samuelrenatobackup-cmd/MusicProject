using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JSMusic.Models;

[Table("Genero")]
public partial class Genero
{
    [Key]
    public int IdGenero { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [ForeignKey("IdGenero")]
    [InverseProperty("IdGeneros")]
    public virtual ICollection<Musica> IdMusicas { get; set; } = new List<Musica>();
}
