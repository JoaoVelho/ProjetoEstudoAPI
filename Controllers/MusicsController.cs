using System;
using System.Collections.Generic;
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
    public class MusicsController : ControllerBase
    {
        private readonly ApplicationDbContext _database;

        public MusicsController(ApplicationDbContext database) {
            _database = database;
        }

        [HttpGet]
        public async Task<ActionResult<List<MusicOutDTO>>> GetAsync() {
            try {
                var musics = await _database.Musics.Include(music => music.Album)
                    .AsNoTracking().ToListAsync();

                List<MusicOutDTO> musicsOut = new List<MusicOutDTO>();
                musics.ForEach(music => {
                    MusicOutDTO musicOut = new MusicOutDTO();
                    musicOut.Id = music.Id;
                    musicOut.Name = music.Name;
                    musicOut.Album = music.Album.Name;

                    musicsOut.Add(musicOut);
                });
                return musicsOut;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar as músicas do banco de dados");
            }
        }

        [HttpGet("{id}", Name = "GetMusic")]
        public async Task<ActionResult<MusicOutDTO>> GetByIdAsync(int id) {
            try {
                var music = await _database.Musics.Include(music => music.Album)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(music => music.Id == id);

                if (music == null) return NotFound();

                MusicOutDTO musicOut = new MusicOutDTO();
                musicOut.Id = music.Id;
                musicOut.Name = music.Name;
                musicOut.Album = music.Album.Name;

                return musicOut;
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar buscar a música do banco de dados");
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public ActionResult Create([FromBody] MusicDTO musicDTO) {
            // Get user email from Claims
            string email = HttpContext.User.Claims
                .First(claim => claim.Type.ToString().Equals("userEmail", StringComparison.InvariantCultureIgnoreCase)).Value;

            Album album = _database.Albums.Include(album => album.Artist)
                .AsNoTracking()
                .FirstOrDefault(album => album.Id == musicDTO.AlbumId);

            if (album != null && album.Artist.Email == email) {
                Music music = new Music();
                music.Name = musicDTO.Name;
                music.AlbumId = musicDTO.AlbumId;

                _database.Musics.Add(music);
                _database.SaveChanges();

                MusicOutDTO musicOut = new MusicOutDTO();
                musicOut.Id = music.Id;
                musicOut.Name = music.Name;
                musicOut.Album = album.Name;
                return new CreatedAtRouteResult("GetMusic", new { id = musicOut.Id }, musicOut);
            } else if (album == null) {
                return NotFound();
            } else {
                return StatusCode(StatusCodes.Status403Forbidden,
                    "O álbum informado não pertence a você");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public ActionResult<MusicOutDTO> Delete(int id) {
            try {
                // Get user email from Claims
                string email = HttpContext.User.Claims
                    .First(claim => claim.Type.ToString().Equals("userEmail", StringComparison.InvariantCultureIgnoreCase)).Value;

                var music = _database.Musics.Include(music => music.Album.Artist)
                    .FirstOrDefault(music => music.Id == id);

                if (music == null) return NotFound();

                if (music.Album.Artist.Email == email) {
                    try {
                        _database.Musics.Remove(music);
                        _database.SaveChanges();
                    } catch (Exception) {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            "Não foi possível deletar pois provavelmente há alguma relação entre esse item e outro");
                    }

                    MusicOutDTO musicOut = new MusicOutDTO();
                    musicOut.Id = music.Id;
                    musicOut.Name = music.Name;
                    musicOut.Album = music.Album.Name;
                    return musicOut;
                } else {
                    return StatusCode(StatusCodes.Status403Forbidden,
                        "Esta música não pertence a você");
                }
            } catch(Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar deletar a música do banco de dados");
            }
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public ActionResult Put(int id, [FromBody] MusicPutDTO musicPutDTO) {
            if (id != musicPutDTO.Id) {
                return BadRequest();
            }

            // Get user email from Claims
            string email = HttpContext.User.Claims
                .First(claim => claim.Type.ToString().Equals("userEmail", StringComparison.InvariantCultureIgnoreCase)).Value;

            var musicTemp = _database.Musics.Include(music => music.Album.Artist)
                .FirstOrDefault(music => music.Id == id);

            if (musicTemp == null) return NotFound();
            
            if (musicTemp.Album.Artist.Email == email) {
                musicTemp.Name = musicPutDTO.Name;
                Album albumTemp = _database.Albums.Include(album => album.Artist)
                    .AsNoTracking()
                    .FirstOrDefault(album => album.Id == musicPutDTO.AlbumId);

                if (albumTemp != null && albumTemp.Artist.Email == email) {
                    musicTemp.AlbumId = albumTemp.Id;
                } else if (albumTemp == null) {
                    return NotFound();
                } else {
                    return StatusCode(StatusCodes.Status403Forbidden,
                        "O álbum informado não pertence a você");
                }

                _database.SaveChanges();
                return Ok();
            } else {
                return StatusCode(StatusCodes.Status403Forbidden,
                    "Esta música não pertence a você");
            }
        }
    }
}