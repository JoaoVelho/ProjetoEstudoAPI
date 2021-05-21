using System;

namespace ProjetoEstudoAPI.DTOs
{
    public class UserToken
    {
        public bool Authenticated { get; set; }
        public DateTime Expiration { get; set; }
        public string Token { get; set; }
    }
}