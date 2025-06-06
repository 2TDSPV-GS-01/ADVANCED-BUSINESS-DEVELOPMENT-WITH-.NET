namespace FonteViva.Models
{
    public class RegistroMedida
    {
        public string Id { get; set; } = null!;
        public DateTime DtRegistro { get; set; }
        public float Resultado { get; set; }
        public int IdSensor { get; set; }

        public Sensor Sensor { get; set; } = null!;

    }
}
