using WebApi.Modelos;
using MongoDB.Driver;

namespace WebApi.Servicios
{
    public class DireccionServicio
    {
        private readonly IMongoCollection<Direccion> _direcciones;
        
        public DireccionServicio(IDireccionesDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DataBaseName);

            _direcciones = database.GetCollection<Direccion>(settings.DireccionesCollectionName);
        }

        public List<Direccion> Get() => _direcciones.Find(direccion => true).ToList();

        public Direccion Get(int id) => _direcciones.Find(direccion => direccion.ID == id);
    }
}
