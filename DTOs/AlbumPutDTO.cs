using System.ComponentModel.DataAnnotations;

namespace ProjetoEstudoAPI.DTOs
{
    public class AlbumPutDTO
    {
        [Required(ErrorMessage = "Id obrigatório")]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nome obrigatório")]
        [MaxLength(100, ErrorMessage = "Nome muito grande, deve ter menos que {1} caracteres")]
        [MinLength(1, ErrorMessage = "Nome muito pequeno, deve ter mais que {1} caracteres")]
        public string Name { get; set; }
    }
}