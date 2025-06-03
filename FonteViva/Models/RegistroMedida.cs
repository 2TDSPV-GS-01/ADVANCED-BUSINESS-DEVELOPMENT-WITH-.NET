namespace FonteViva.Models
{
    public class RegistroMedida
    {
        public string Id { get; private set; } = null!;
        public DateTime DtRegistro { get; private set; }
        public float Resultado { get; private set; }
        public int IdSensor { get; private set; }

        public Sensor Sensor { get; private set; } = null!;

    }
}
