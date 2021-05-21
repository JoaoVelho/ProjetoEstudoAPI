using Microsoft.AspNetCore.Identity;

namespace ProjetoEstudoAPI.Models
{
    public class Artist : IdentityUser
    {
        public string Name { get; set; }
    }
}