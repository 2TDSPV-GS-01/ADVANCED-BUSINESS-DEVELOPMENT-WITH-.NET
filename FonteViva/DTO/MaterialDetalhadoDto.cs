namespace FonteViva.DTO
{
    public class MaterialDetalhadoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public int Quantidade { get; set; }
        public float Preco { get; set; }

        public FornecedorListagemDto Fornecedor{ get; set; }
    }
}
