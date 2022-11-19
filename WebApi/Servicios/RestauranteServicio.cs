﻿using System.Collections.Generic;
using WebApi.Modelos;
using MongoDB.Driver;

namespace WebApi.Servicios
{
    public class RestauranteServicio
    {
        private readonly IMongoCollection<Restaurante> _restaurantes;

        public RestauranteServicio(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _restaurantes = database.GetCollection<Restaurante>("Restaurante");
        }

        public List<Restaurante> Get() => _restaurantes.Find(restaurante => true).ToList();

        public Restaurante Get(string id) =>
            _restaurantes.Find(restaurante => restaurante.Id == id).FirstOrDefault();

        public Restaurante Add(Restaurante restaurante)
        {
            _restaurantes.InsertOne(restaurante);

            return restaurante;
        }

        public void Update(string id, Restaurante restaurante) =>
            _restaurantes.ReplaceOne(r => r.Id == id, restaurante);

        public void Delete(Restaurante restaurante) =>
            _restaurantes.DeleteOne(r => r.Id == restaurante.Id);
    }
}
