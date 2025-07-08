using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoFinal.Models;

public class Local
{
    [Key]
    public int IdLocal { get; set; }

    [Required(ErrorMessage = "Campo nome é obrigatório.")]
    [Display(Name =  "Pacote")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "Campo descrição é obrigatório.")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "Campo endereço é obrigatório.")]
    [Display(Name = "Opções de lazer.")]
    public string? Endereco { get; set; }

    [Required(ErrorMessage = "O preço por noite é obrigatório.")]
    [Display(Name = "Preço por noite.")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal PrecoPorNoite { get; set; }

    public string? ImagemUrl { get; set; }
    public string? ImagemUrl2 { get; set; }
    public string? ImagemUrl3 { get; set; }
    public string? ImagemUrl4 { get; set; }
    public string? ImagemUrl5 { get; set; }
    public string? ImagemUrl6 { get; set; }

}
