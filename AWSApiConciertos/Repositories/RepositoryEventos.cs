using AWSApiConciertos.Data;
using AWSApiConciertos.Models;
using Microsoft.EntityFrameworkCore;

namespace AWSApiConciertos.Repositories
{
    public class RepositoryEventos
    {
        private EventosContext context;
        public RepositoryEventos(EventosContext context)
        {
            this.context = context;
        }

        public async Task<List<Categoria>> GetCategoriasAsync()
        {
            return await this.context.Categorias.ToListAsync();
        }
        
        public async Task<List<Evento>> GetEventosAsync()
        {
            return await this.context.Eventos.ToListAsync();
        }
        public async Task<List<Evento>> GetEventosCategoriaAsync(int idcategoria)
        {
            return await this.context.Eventos.Where(x => x.IdCategoria == idcategoria).ToListAsync();
        }

        private async Task<int> GetMaxIdEvento()
        {
            return await this.context.Eventos.MaxAsync(x => x.IdEvento) + 1;
        }

        public async Task NewEventoAsync(int idevento, string nombre, string artista, int idcategoria, string imagen)
        {
            Evento evento = new Evento
            {
                IdCategoria = idcategoria,
                IdEvento = await this.GetMaxIdEvento(),
                Artista = artista,
                Imagen = imagen,
                Nombre = nombre,
            };

            this.context.Eventos.Add(evento);
            await this.context.SaveChangesAsync();
        }

    }
}
