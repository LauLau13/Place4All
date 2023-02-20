using MongoDB.Bson;
using MongoDB.Driver;
using WebApi.Modelos;

namespace WebApi.Servicios
{
    public class UsuariosServicio
    {
        //Damos a la lista Usuarios el nombre de _usuarios
        private readonly IMongoCollection<Usuarios> _usuarios;
        private readonly DireccionesServicio _direccionServicio;

        //Conectamos la base de datos con usuarios
        public UsuariosServicio(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _usuarios = database.GetCollection<Usuarios>("Usuarios");
        }

        //Recoge todos los usuarios que estan en la base de datos
        public List<Usuarios> Get() => _usuarios.Find(usuario => true).ToList();

        //Cogemos el id del usuario y comparamos el usuario con el id de usuario
        public Usuarios Get(string id) => _usuarios.Find<Usuarios>(usuario => usuario.Id == id).FirstOrDefault();

        public Usuarios Login(string email, string password) =>
            _usuarios.Find<Usuarios>(u => u.Email == email && u.Password == password).FirstOrDefault();
        //Creamos un nuevo usuario, si ese usuario no tienen ID se crea un nuevo ID y se inserta en la base de datos
        public Usuarios Create(Usuarios usuario)
        {
            usuario.Id ??= new BsonObjectId(ObjectId.GenerateNewId()).ToString();
            _usuarios.InsertOne(usuario);
            return usuario;
        }

        //Actualizamos la lista de usuarios al insertar nuevo usuario
        public void Update(string id, Usuarios usuarioIn) => _usuarios.ReplaceOne(Builders<Usuarios>.Filter.Eq(s => s.Id, id), usuarioIn);

        //Borramos un usuario de la lista comparando el usuario con su IP¿
        public void Remove(Usuarios usuarioIn) => _usuarios.DeleteOne(usuario => usuario.Id == usuarioIn.Id);
    }
}
