using MongoDB.Bson;
using MongoDB.Driver;
using WebApi.Modelos;

namespace WebApi.Servicios
{
    public class RestaurantesServicio
    {
        //Damos a la lista de Restaurantes el nombre de _restaurantes
        private readonly IMongoCollection<Restaurantes> _restaurantes;
        //Creamos la variable DirecciónServicio del restaurante como _direccionServicio
        private readonly DireccionesServicio _direccionServicio;

        //Conexión de la base de datos con los objetos restaurantes
        public RestaurantesServicio(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _restaurantes = database.GetCollection<Restaurantes>("Restaurantes");
        }

        //Método que recoge todos los objetos restaurante de la base de datos y los devuelve en forma de lista
        public List<Restaurantes> Get() => _restaurantes.Find(restaurante => true).ToList();

        //Método que busca en la base de datos y devuelve el objeto restaurante con la id introducida por parámetro
        public Restaurantes Get(string id) => _restaurantes.Find(restaurante => restaurante.Id == id).FirstOrDefault();

        //Método que crea un objeto restaurante
        public Restaurantes Create(Restaurantes restaurante)
        {
            //Comprobación, si el restaurante nuevo no contiene un Id (al crearse en el constructor nulo), entonces se genera uno nuevo y se le asigna
            restaurante.Id ??= new BsonObjectId(ObjectId.GenerateNewId()).ToString();
            //Inserta un objeto restaurante en el listado de todos los restaurantes de la bd
            _restaurantes.InsertOne(restaurante);
            //Devuelve el restaurante recién creado
            return restaurante;
        }

        //Actualizamos la lista de restaurantes al insertar nuevo restaurante
        public void Update(string id, Restaurantes restaurante) => _restaurantes.ReplaceOne(Builders<Restaurantes>.Filter.Eq(r => r.Id, id), restaurante);

        //Borramos el restaurante pasado como parámetro de entrada de la lista de restaurantes de la bd identificándolo mediante su Id
        public void Delete(Restaurantes restauranteIn) => _restaurantes.DeleteOne(restaurante => restaurante.Id == restauranteIn.Id);

        public Task<List<Restaurantes>> Search(IBuscaCiudad busqueda) => _restaurantes
            .Find(restaurante => restaurante.Direccion.Ciudad.ToLower() == busqueda.Ciudad.ToLower()).ToListAsync();

    }
}
