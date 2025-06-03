using FonteViva.Models;

namespace FonteViva.DTO
{
    public class ContatoDetalhadoDto
    {
        public int Id { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public ResponsavelListagemDto? Responsavel { get; set; }
        public FornecedorListagemDto? Fornecedor { get; set; }
    }
}
