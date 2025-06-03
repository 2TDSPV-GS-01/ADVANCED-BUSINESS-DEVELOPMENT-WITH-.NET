using FonteViva.Models;

namespace FonteViva.DTO
{
    public class ResponsavelDetalhadoDto
    {
        public string CPF { get; set; }

        public string Nome { get; set; }

        public ICollection<EstacaoTratamentoDto> EstacaoTratamentos { get; set; } = new List<EstacaoTratamentoDto>();

        public ICollection<ContatoDto> Contatos { get; set; } = new List<ContatoDto>();
    }
}
