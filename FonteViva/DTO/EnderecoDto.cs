using Microsoft.AspNetCore.Mvc;

namespace FonteViva.DTO
{
    public class EnderecoDto
    {
        public int? Id { get; set; }
        public string? Pais { get; set; }
        public string? Estado { get; set; }
        public string? Cidade { get; set; }
        public string? Rua { get; set; }
        public string? CEP { get; set; }

    }
}
