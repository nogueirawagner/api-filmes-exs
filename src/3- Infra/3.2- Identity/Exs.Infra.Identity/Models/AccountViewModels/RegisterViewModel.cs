using System.ComponentModel.DataAnnotations;

namespace Exs.Infra.Identity.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O CPF deve ser informado.")]
        [MaxLength(14, ErrorMessage = "O CPF deve conter no máximo {1} dígitos.")]
        [Display(Name = "Cpf")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "A senha deve ser informada.")]
        [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "A confirmação de senha deve ser informada.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmaSenha { get; set; }
    }
}
