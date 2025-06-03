namespace FonteViva.Models
{
    public class Fornecedor
    {
        public string CNPJ { get; set; }

        public string Nome { get; set; }

        public int IdEndereco { get; set; }

        public Endereco Endereco { get; set; } = null!;

        public ICollection<Contato> Contatos { get; set; } = new List<Contato>();


        public ICollection<Material> Materials { get; set; } = new List<Material>();
    }
}
