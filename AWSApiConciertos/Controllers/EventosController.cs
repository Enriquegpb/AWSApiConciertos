using AWSApiConciertos.Models;
using AWSApiConciertos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWSApiConciertos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private RepositoryEventos repo;

        public EventosController(RepositoryEventos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Categoria>>> GetCategorias()
        {
            return await this.repo.GetCategoriasAsync();
        }
        [HttpGet]
        public async Task<ActionResult<List<Evento>>> GetEventos()
        {
            return await this.repo.GetEventosAsync();
        } 
        
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<List<Evento>>> GetEventosCategoria(int id)
        {
            return await this.repo.GetEventosCategoriaAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> NewEvento(Evento evento)
        {
            await this.repo.NewEventoAsync(evento.IdEvento, evento.Nombre, evento.Artista, evento.IdCategoria, evento.Imagen);
            return Ok();
        }
    }
}
