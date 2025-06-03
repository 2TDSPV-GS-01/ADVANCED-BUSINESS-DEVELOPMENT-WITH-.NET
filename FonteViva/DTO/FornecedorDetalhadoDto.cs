using FonteViva.Models;

namespace FonteViva.DTO
{
    public class FornecedorDetalhadoDto
    {
        public string CNPJ { get; set; }

        public string Nome { get; set; }

        public EnderecoDto Endereco { get; set; } = null!;

        public ICollection<ContatoDto> Contatos { get; set; } = new List<ContatoDto>();

        public ICollection<MaterialDto> Materials { get; set; } = new List<MaterialDto>();
    }
}
