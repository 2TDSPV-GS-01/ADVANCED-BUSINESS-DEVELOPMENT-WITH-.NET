namespace FonteViva.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public int Quantidade { get; set; }
        public float Preco { get; set; }
        public string CNPJ { get; set; }
        public Fornecedor Fornecedor { get; set; }

    }
}
