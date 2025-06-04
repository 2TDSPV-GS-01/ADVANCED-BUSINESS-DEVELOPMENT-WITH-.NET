namespace FonteViva.Models
{
    public class EstacaoTratamento
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime DataInstalacao { get; set; }
        public string? CPF { get; set; }

        public Responsavel? Responsavel { get; set; }

        public ICollection<Sensor> Sensors { get; set; } = new List<Sensor>();

    }
}
