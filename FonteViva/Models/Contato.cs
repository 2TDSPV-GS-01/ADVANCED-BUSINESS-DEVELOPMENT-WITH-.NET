using FonteViva.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class Contato
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Telefone { get; set; }

    public string Email { get; set; }

    public string? CPF { get; set; }

    public string? CNPJ { get; set; }

    public Responsavel? Responsavel { get; set; }
    public Fornecedor? Fornecedor { get; set; }

}

