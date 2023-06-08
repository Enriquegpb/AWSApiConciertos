using AWSApiConciertos.Models;
using Microsoft.EntityFrameworkCore;

namespace AWSApiConciertos.Data
{
    public class EventosContext: DbContext
    {
        public EventosContext(DbContextOptions<EventosContext> options) : base(options) { }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Evento> Eventos { get; set; }
    }
}
