using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoEstudoAPI.Data;
using ProjetoEstudoAPI.DTOs;
using ProjetoEstudoAPI.Models;

namespace ProjetoEstudoAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly ApplicationDbContext _database;

        public AlbumsController(ApplicationDbContext database) {
            _database = database;
        }

        [HttpGet]
        public async Task<ActionResult<List<AlbumOutDTO>>> GetAsync() {
            try {
                var albums = await _database.Albums.Include(album => album.Artist)
                    .AsNoTracking().ToListAsync();

                List<AlbumOutDTO> albumsOut = new List<AlbumOutDTO>();
                albums.ForEach(album => {
                    AlbumOutDTO albumOut = new AlbumOutDTO();
                    albumOut.Id = album.Id;
                    albumOut.Name = album.Name;
                    albumOut.Artist = album.Artist.Name;

                    albumsOut.Add(albumOut);
                });
                return albumsOut;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar os álbuns do banco de dados");
            }
        }

        [HttpGet("{id}", Name = "GetAlbum")]
        public async Task<ActionResult<AlbumOutDTO>> GetByIdAsync(int id) {
            try {
                var album = await _database.Albums.Include(album => album.Artist)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(album => album.Id == id);

                if (album == null) return NotFound();

                AlbumOutDTO albumOut = new AlbumOutDTO();
                albumOut.Id = album.Id;
                albumOut.Name = album.Name;
                albumOut.Artist = album.Artist.Name;

                return albumOut;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar o álbum do banco de dados");
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public ActionResult Create([FromBody] AlbumDTO albumDTO) {
            // Get user email from Claims
            string email = HttpContext.User.Claims
                .First(claim => claim.Type.ToString().Equals("userEmail", StringComparison.InvariantCultureIgnoreCase)).Value;
            Artist artistTemp = _database.Artists
                .AsNoTracking()
                .FirstOrDefault(art => art.Email == email);

            if (artistTemp != null) {
                Album album = new Album();
                album.Name = albumDTO.Name;
                album.ArtistId = artistTemp.Id;

                _database.Albums.Add(album);
                _database.SaveChanges();

                AlbumOutDTO albumOut = new AlbumOutDTO();
                albumOut.Id = album.Id;
                albumOut.Name = album.Name;
                albumOut.Artist = artistTemp.Name;
                return new CreatedAtRouteResult("GetAlbum", new { id = albumOut.Id }, albumOut);
            } else {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public ActionResult<AlbumOutDTO> Delete(int id) {
            try {
                // Get user email from Claims
                string email = HttpContext.User.Claims
                    .First(claim => claim.Type.ToString().Equals("userEmail", StringComparison.InvariantCultureIgnoreCase)).Value;

                var album = _database.Albums.Include(album => album.Artist)
                    .FirstOrDefault(album => album.Id == id);

                if (album == null) return NotFound();

                if (album.Artist.Email == email) {
                    try {
                        _database.Albums.Remove(album);
                        _database.SaveChanges();
                    } catch (Exception) {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            "Não foi possível deletar pois provavelmente há alguma relação entre esse item e outro");
                    }

                    AlbumOutDTO albumOut = new AlbumOutDTO();
                    albumOut.Id = album.Id;
                    albumOut.Name = album.Name;
                    albumOut.Artist = album.Artist.Name;
                    return albumOut;
                } else {
                    return StatusCode(StatusCodes.Status403Forbidden,
                        "Este álbum não pertence a você");
                }
            } catch(Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar deletar o álbum do banco de dados");
            }
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public ActionResult Put(int id, [FromBody] AlbumPutDTO albumPutDTO) {
            if (id != albumPutDTO.Id) {
                return BadRequest();
            }

            // Get user email from Claims
            string email = HttpContext.User.Claims
                .First(claim => claim.Type.ToString().Equals("userEmail", StringComparison.InvariantCultureIgnoreCase)).Value;

            var albumTemp = _database.Albums.Include(alb => alb.Artist)
                .FirstOrDefault(alb => alb.Id == id);

            if (albumTemp == null) return NotFound();
            
            if (albumTemp.Artist.Email == email) {
                albumTemp.Name = albumPutDTO.Name;

                _database.SaveChanges();
                return Ok();
            } else {
                return StatusCode(StatusCodes.Status403Forbidden,
                    "Este álbum não pertence a você");
            }
        }
    }
}