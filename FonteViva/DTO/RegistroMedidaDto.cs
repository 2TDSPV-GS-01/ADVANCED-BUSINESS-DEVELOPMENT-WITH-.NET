namespace FonteViva.DTO
{
    public class RegistroMedidaDto
    {
        public string Id { get; set; } = null!;
        public DateTime DtRegistro { get; set; }
        public float Resultado { get; set; }
        public int IdSensor { get; set; }
    }
}