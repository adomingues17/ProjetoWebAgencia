using System.ComponentModel.DataAnnotations;

namespace ProjetoFinal.Models;

public class Foto
{
    [Key]
    public int IdFoto { get; set; }
    public string? Imagem { get; set; }
    public string? Descricao { get; set; }
}
