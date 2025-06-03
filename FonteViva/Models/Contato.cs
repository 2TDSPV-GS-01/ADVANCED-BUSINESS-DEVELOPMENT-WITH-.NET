using FonteViva.Models;

public class Contato
{
    public int Id { get; set; }

    public string Telefone { get; set; }

    public string Email { get; set; }

    public string? CPF { get; set; }

    public string? CNPJ { get; set; }

    public Responsavel? Responsavel { get; set; }
    public Fornecedor? Fornecedor { get; set; }

}

