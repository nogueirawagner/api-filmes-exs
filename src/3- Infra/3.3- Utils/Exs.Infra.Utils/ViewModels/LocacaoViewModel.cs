using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exs.Infra.Utils
{
    public class LocacaoViewModel
    {
        [Key]
        public int Id { get; set; }
        public string CPF { get; set; }
        public DateTime DataLocacao { get; set; }
        public List<int> FilmesId { get; set; }
        public List<LocacaoFilmeViewModel> Locacoes { get; set; }
    }
}
