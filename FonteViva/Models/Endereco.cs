namespace FonteViva.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string? Pais { get; set; } = null!;
        public string? Estado { get; set; } = null!;
        public string? Cidade { get; set; } = null!;
        public string? Rua { get; set; } 
        public string? CEP { get; set; }

        public Fornecedor? Fornecedor { get; set; }
    }
}
