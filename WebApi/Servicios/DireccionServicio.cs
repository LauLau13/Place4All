using MongoDB.Bson;
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
            var database = client.GetDatabase(settings.DatabaseName);

            _direcciones = database.GetCollection<Direccion>("Direccion");
        }

        public List<Direccion> Get() => _direcciones.Find(direccion => true).ToList();

        public Direccion Get(string id) => _direcciones.Find<Direccion>(direccion => direccion.Id == id).FirstOrDefault();

        public Direccion Create(Direccion direccion)
        {
            direccion.Id ??= new BsonObjectId(ObjectId.GenerateNewId()).ToString();
            _direcciones.InsertOne(direccion);
            return direccion;
        }

        public void Update(string id, Direccion direccionIn) => _direcciones.ReplaceOne(Builders<Direccion>.Filter.Eq(s => s.Id, id), direccionIn);

        public void Remove(Direccion direccionIn) => _direcciones.DeleteOne(direccion => direccion.Id == direccionIn.Id);


    }
}
