namespace FonteViva.Models
{
    public class Responsavel
    {
        public string CPF { get; set; }

        public string Nome { get; set; }

        public ICollection<EstacaoTratamento> EstacaoTratamentos { get; set; } = new List<EstacaoTratamento>();

        public ICollection<Contato> Contatos { get; set; } = new List<Contato>();
    }
}
