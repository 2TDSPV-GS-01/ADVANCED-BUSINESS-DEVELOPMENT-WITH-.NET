namespace FonteViva.DTO
{
    public class SensorDetalhadoDto
    {
        public int Id { get; set; }
        public string TpSensor { get; set; }
        public string TpMedida { get; set; }

        public EstacaoListagemDto Estacao { get; set; }
        public List<RegistroMedidaDto> Registros { get; set; }
    }
}
