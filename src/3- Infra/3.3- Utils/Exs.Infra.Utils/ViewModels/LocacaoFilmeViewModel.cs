using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exs.Infra.Utils
{
    public class LocacaoFilmeViewModel
    {
        [Key]
        public int Id { get; set; }
        public int LocacaoId { get; set; }
        public List<int> FilmesId { get; set; }
        public string CPF { get; set; }

        // Testando retorno
        public int FilmeId { get; set; }
        public FilmeViewModel Filme { get; set; }
    }
}
