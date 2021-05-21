using System.ComponentModel.DataAnnotations;

namespace ProjetoEstudoAPI.Models
{
    public class Music
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Album Album { get; set; }
        public int? AlbumId { get; set; }
    }
}