using System.ComponentModel.DataAnnotations.Schema;

namespace FonteViva.Models
{
    public class Endereco
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Pais { get; set; } = null!;
        public string? Estado { get; set; } = null!;
        public string? Cidade { get; set; } = null!;
        public string? Rua { get; set; } 
        public string? CEP { get; set; }

        public Fornecedor? Fornecedor { get; set; }
    }
}
