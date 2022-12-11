using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using WebApi.Modelos;

namespace WebApi.Servicios;

public class ReservaServicio
{
    private readonly IMongoCollection<Reserva> _reservas;

    public ReservaServicio(IDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);

        _reservas = database.GetCollection<Reserva>("Reserva");
    }

    public async Task<List<Reserva>> Get() => await _reservas.Find(reserva => true).ToListAsync();

    public async Task<List<Reserva>> GetUserReserva(string usuarioId) =>
      await  _reservas.Find(reserva => reserva.Usuario.Id == usuarioId).ToListAsync();

    public async Task<Reserva> Get(string id) => await _reservas.Find<Reserva>(reserva => reserva.Id == id).FirstOrDefaultAsync();

    public async Task Create(Reserva reserva)
    {
        reserva.Id ??= new BsonObjectId(ObjectId.GenerateNewId()).ToString();
        await _reservas.InsertOneAsync(reserva);
        return;
    }

    public async Task Update(string id, Reserva reserva) => await _reservas.ReplaceOneAsync(Builders<Reserva>.Filter.Eq(s => s.Id, id), reserva);

    public async void Remove(Reserva reservaIn) => await _reservas.DeleteOneAsync(reserva => reserva.Id == reservaIn.Id);
    
}