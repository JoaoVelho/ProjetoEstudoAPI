namespace ProjetoEstudoAPI.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Artist Artist { get; set; }
        public string ArtistId { get; set; }
    }
}