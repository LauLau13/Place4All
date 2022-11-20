using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using WebApi.Modelos;

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

        public Restaurante Create(Restaurante restaurante)
        {
            //Comprobación, si el restaurante nuevo no contiene un Id (al crearse el el constructor nulo), entonces se genera uno nuevo y se le asigna
            restaurante.Id ??= BsonObjectId.GenerateNewId().ToString();
            _restaurantes.InsertOne(restaurante);
            var restauranteWDireccion = HasDireccion(restaurante);
            var restauranteWServicioDireccion = HasServices(restauranteWDireccion);
            return restauranteWServicioDireccion;
            //Llamar a los métodos de comprobación de dirección y servicios
        }

        public void Update(string id, Restaurante restaurante) => _restaurantes.ReplaceOne(Builders<Restaurante>.Filter.Eq(r => r.Id, id), restaurante);

        public void Delete(Restaurante restaurante) =>
            _restaurantes.DeleteOne(r => r.Id == restaurante.Id);


        private Restaurante HasDireccion(Restaurante restaurante)
        {
            //En el caso de que no esté creada una direccion
            var direccion = restaurante.Direccion;
            if (direccion.Id == null)
            {
                var newDireccion = _direccionServicio.Create(direccion);
                restaurante.Direccion = newDireccion;
                return restaurante;
            }
            else
            {
                //En el caso de que la dirección creada no tenga Id
                var direccionId = _direccionServicio.Get(direccion.Id);
                if (direccionId == null)
                {
                    var newDireccion = _direccionServicio.Create(direccion);
                    restaurante.Direccion = newDireccion;
                    return restaurante;
                }
                else
                {
                    return restaurante;
                }
            }

            
            
        }

        private Restaurante HasServices(Restaurante restaurante)
        {
            //En el caso de que no esté creada la lista de servicios
            var servicio = restaurante.Servicio;
            if (servicio.Id == null)
            {
                var newDireccion = _servicioServicio.Create(servicio);
                restaurante.Servicio = newServicio;
                return restaurante;
            }
            else
            {
                //En el caso de que la dirección creada no tenga Id
                var servicioId = _servicioServicio.Get(servicio.Id);
                if (servicioId == null)
                {
                    var newServicio = _servicioServicio.Create(servicio);
                    restaurante.Servicio = newServicio;
                    return restaurante;
                }
                else
                {
                    return restaurante;
                }
            }

            
        }

    }
}
