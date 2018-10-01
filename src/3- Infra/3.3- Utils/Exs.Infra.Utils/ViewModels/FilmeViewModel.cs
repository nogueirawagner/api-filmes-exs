using System;
using System.ComponentModel.DataAnnotations;

namespace Exs.Infra.Utils
{
    public class FilmeViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
        public int GeneroId { get; set; }
    }
}
