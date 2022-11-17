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

        public Direccion Get(int id) => _direcciones.Find(direccion => direccion.ID == ID).FirstOrDefault();

        public Direccion Create(direccion)
        {
            _direcciones.InsertOne(direccion);
            return direccion;
        }

        public void Update(string id, Direccion updatedDireccion) => _direcciones.ReplaceOne(direccion => direccion.ID == ID, updatedDireccion);

        public void Delete(Direccion direccionForDeletion) => _direcciones.DeleteOne(direccion => direccion.ID == direccionForDeletion.ID);

        public void Delete(string id) => _direcciones.DeleteOne(direccion => direccion.ID == ID);


    }
}
