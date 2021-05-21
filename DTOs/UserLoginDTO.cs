using System.ComponentModel.DataAnnotations;

namespace ProjetoEstudoAPI.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Email obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha obrigatória")]
        public string Password { get; set; }
    }
}