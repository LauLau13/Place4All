using WebApi.Modelos;
using MongoDB.Driver;

namespace WebApi.Servicios
{
    public class DireccionServicio
    {
        private readonly IMongoCollection<Direccion> _direcciones;
        
        public DireccionServicio(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DataBaseName);

            _direcciones = database.GetCollection<Direccion>(settings.DireccionesCollectionName);
        }

        public List<Direccion> Get() => _direcciones.Find(direccion => true).ToList();

        public Direccion Get(string id) => _direcciones.Find<Direccion>(direccion => direccion.Id == id).FirstOrDefault();

        public Direccion Create(Direccion direccion)
        {
            _direcciones.InsertOne(direccion);
            return direccion;
        }

        public void Update(string id, Direccion direccionIn) => _direcciones.ReplaceOne(direccion => direccion.Id == id, direccionIn);

        public void Remove(Direccion direccionIn) => _direcciones.DeleteOne(direccion => direccion.Id == direccionIn.ID);


    }
}
