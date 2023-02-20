using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApi.Modelos;

namespace WebApi.Servicios;

public class ReservasServicio
{
    private readonly IMongoCollection<Reservas> _reservas;

    public ReservasServicio(IDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);

        _reservas = database.GetCollection<Reservas>("Reservas");
    }

    public async Task<List<Reservas>> Get() => await _reservas.Find(reserva => true).ToListAsync();

    public async Task<List<Reservas>> GetUserReserva(string usuarioId) =>
      await  _reservas.Find(reserva => reserva.Usuario.Id == usuarioId).ToListAsync();

    public async Task<Reservas> Get(string id) => await _reservas.Find<Reservas>(reserva => reserva.Id == id).FirstOrDefaultAsync();

    public async Task Create(Reservas reserva)
    {
        reserva.Id ??= new BsonObjectId(ObjectId.GenerateNewId()).ToString();
        await _reservas.InsertOneAsync(reserva);
        return;
    }

    public async Task Update(string id, Reservas reserva) => await _reservas.ReplaceOneAsync(Builders<Reservas>.Filter.Eq(s => s.Id, id), reserva);

    public async void Remove(Reservas reservaIn) => await _reservas.DeleteOneAsync(reserva => reserva.Id == reservaIn.Id);
    
}