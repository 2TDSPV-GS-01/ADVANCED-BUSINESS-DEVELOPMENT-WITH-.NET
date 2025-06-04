using System.ComponentModel.DataAnnotations.Schema;

namespace FonteViva.Models
{
    public class Sensor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TpSensor { get; set; }
        public string? TpMedida { get; set; }
        public int IdEstacao { get; set; }

        public EstacaoTratamento? EstacaoTratamento { get; set; }

        public ICollection<RegistroMedida>? RegistroMedidas { get; set; } = new List<RegistroMedida>();
    }
}
