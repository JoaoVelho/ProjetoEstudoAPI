using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjetoEstudoAPI.Data;
using ProjetoEstudoAPI.DTOs;
using ProjetoEstudoAPI.Models;

namespace ProjetoEstudoAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        private readonly UserManager<Artist> _userManager;
        private readonly SignInManager<Artist> _signInManager;
        private readonly IConfiguration _configuration;

        public ArtistsController(ApplicationDbContext database, UserManager<Artist> userManager, 
            SignInManager<Artist> signInManager, IConfiguration configuration) {
            _database = database;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<ArtistDTO>>> GetAsync() {
            try {
                var artistsTemp = await _database.Artists.AsNoTracking().ToListAsync();

                List<ArtistDTO> artists = new List<ArtistDTO>();
                artistsTemp.ForEach(artistTemp => {
                    ArtistDTO artist = new ArtistDTO();
                    artist.Name = artistTemp.Name;
                    artists.Add(artist);
                });

                return artists;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar os artistas do banco de dados");
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserRegisterDTO model) {
            if (model.Password == model.ConfirmPassword) {
                var artist = new Artist {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(artist, model.Password);

                if (!result.Succeeded) {
                    return BadRequest(result.Errors);
                }

                await _signInManager.SignInAsync(artist, false);
                return Ok(GenerateToken(model));
            } else {
                ModelState.AddModelError("Erro", "Senhas devem ser iguais");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDTO model) {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, 
                isPersistent: false, lockoutOnFailure: false);
            
            if (result.Succeeded) {
                return Ok(GenerateToken(model));
            } else {
                ModelState.AddModelError("Erro", "Login inválido...");
                return BadRequest(ModelState);
            }
        }

        private UserToken GenerateToken(UserLoginDTO model) {
            // define user claims
            var claims = new List<Claim> {
                new Claim("userEmail", model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // generate key using symmetric algorithm
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            // generate token digital signature using private key and Sha256 algorithm
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Expiration time
            var expirationTime = _configuration["TokenConfigurations:ExpireHours"];
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expirationTime));

            // generate JWT token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["TokenConfigurations:Issuer"],
                audience: _configuration["TokenConfigurations:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new UserToken() {
                Authenticated = true,
                Expiration = expiration,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}