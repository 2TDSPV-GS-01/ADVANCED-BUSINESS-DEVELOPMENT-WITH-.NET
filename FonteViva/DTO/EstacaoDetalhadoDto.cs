using FonteViva.Models;

namespace FonteViva.DTO
{
    public class EstacaoDetalhadoDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime DataInstalacao { get; set; }
        public ResponsavelListagemDto? Responsavel { get; set; }
        public ICollection<SensorListagemDto> Sensors { get; set; } = new List<SensorListagemDto>();
    }
}
