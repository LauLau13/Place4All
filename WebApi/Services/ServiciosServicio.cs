using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using WebApi.Modelos;

namespace WebApi.Servicios
{
    public class ServiciosServicio
{
    private readonly IMongoCollection<Servicios> _servicios;
    public ServiciosServicio(IDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _servicios = database.GetCollection<Servicios>("Servicios");
    }

    public List<Servicios> Get() =>
        _servicios.Find(servicio => true).ToList();

    public Servicios Get(string id) =>
        _servicios.Find<Servicios>(servicio => servicio.Id == id).FirstOrDefault();

    public Servicios Create(Servicios servicio)
    {
        //Preguntar donde debería ir esta lógica
        servicio.Id ??= new BsonObjectId(ObjectId.GenerateNewId()).ToString();
        _servicios.InsertOne(servicio);
        return servicio;
    }

    public void Update(string id, Servicios servicioIn)
    {
        _servicios.ReplaceOne(Builders<Servicios>.Filter.Eq(s => s.Id, id), servicioIn);
    }

    public void Remove(Servicios servicioIn) =>
        _servicios.DeleteOne(servicio => servicio.Id == servicioIn.Id);
}
}