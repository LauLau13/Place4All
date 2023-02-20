using MongoDB.Bson;
using WebApi.Modelos;
using MongoDB.Driver;

namespace WebApi.Servicios
{
    public class DireccionesServicio
    {
        private readonly IMongoCollection<Direcciones> _direcciones;
        
        public DireccionesServicio(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _direcciones = database.GetCollection<Direcciones>("Direcciones");
        }

        public List<Direcciones> Get() => _direcciones.Find(direccion => true).ToList();

        public Direcciones Get(string id) => _direcciones.Find<Direcciones>(direccion => direccion.Id == id).FirstOrDefault();

        public Direcciones Create(Direcciones direccion)
        {
            direccion.Id ??= new BsonObjectId(ObjectId.GenerateNewId()).ToString();
            _direcciones.InsertOne(direccion);
            return direccion;
        }

        public void Update(string id, Direcciones direccionIn) => _direcciones.ReplaceOne(Builders<Direcciones>.Filter.Eq(s => s.Id, id), direccionIn);

        public void Remove(Direcciones direccionIn) => _direcciones.DeleteOne(direccion => direccion.Id == direccionIn.Id);


    }
}
