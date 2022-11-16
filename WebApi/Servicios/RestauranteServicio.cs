using WebApi.Modelos;
using MongoDB.Driver;

namespace WebApi.Servicios
{
	public class RestauranteServicio
	{
        private readonly IMongoCollection<Restaurante> _restaurantes;
		public RestauranteServicio(IGamesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _restaurantes = database.GetCollection<Restaurante>("Restaurante");
        }

        public List<Restaurante> Get() => _restaurantes.Find(restaurante => true).ToList();

        public Restaurante Get(int id) => _restaurantes.Find(restaurante => restaurante.ID == id).FirstOrDefault();

        public Restaurante Add(Restaurante restaurante)
        {
            _restaurantes.InsertOne(restaurante);

            return restaurante;
        }

        public void Update(int id, Restaurante restaurante) => _restaurantes.ReplaceOne(r => r.ID == id, restaurante);

        public void Delete(Restaurante restaurante) => _restaurantes.DeleteOne(r => r.ID == restaurante.ID);
            /*public GamesService(IGamesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _games = database.GetCollection<Game>(settings.GamesCollectionName);
        }

        public List<Game> Get() => _games.Find(game => true).ToList();

        public Game Get(string id) => _games.Find(game => game.Id == id).FirstOrDefault();

        public Game Create(Game game)
        {
            _games.InsertOne(game);
            return game;
        }

        public void Update(string id, Game updatedGame) => _games.ReplaceOne(game => game.Id == id, updatedGame);

        public void Delete(Game gameForDeletion) => _games.DeleteOne(game => game.Id == gameForDeletion.Id);

        public void Delete(string id) => _games.DeleteOne(game => game.Id == id);*/

	}
}


