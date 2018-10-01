using System.ComponentModel.DataAnnotations;

namespace Exs.Infra.Identity.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O CPF deve ser informado.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "A senha deve ser informada.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}