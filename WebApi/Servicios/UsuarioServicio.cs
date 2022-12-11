using MongoDB.Bson;
using MongoDB.Driver;
using WebApi.Modelos;

namespace WebApi.Servicios
{
    public class UsuarioServicio
    {
        //Damos a la lista Usuarios el nombre de _usuarios
        private readonly IMongoCollection<Usuario> _usuarios;
        private readonly DireccionServicio _direccionServicio;

        //Conectamos la base de datos con usuarios
        public UsuarioServicio(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _usuarios = database.GetCollection<Usuario>("Usuario");
        }

        //Recoge todos los usuarios que estan en la base de datos
        public List<Usuario> Get() => _usuarios.Find(usuario => true).ToList();

        //Cogemos el id del usuario y comparamos el usuario con el id de usuario
        public Usuario Get(string id) => _usuarios.Find<Usuario>(usuario => usuario.Id == id).FirstOrDefault();

        public Usuario Login(string email, string password) =>
            _usuarios.Find<Usuario>(u => u.Email == email && u.Password == password).FirstOrDefault();
        //Creamos un nuevo usuario, si ese usuario no tienen ID se crea un nuevo ID y se inserta en la base de datos
        public Usuario Create(Usuario usuario)
        {
            usuario.Id ??= new BsonObjectId(ObjectId.GenerateNewId()).ToString();
            _usuarios.InsertOne(usuario);
            return usuario;
        }

        //Actualizamos la lista de usuarios al insertar nuevo usuario
        public void Update(string id, Usuario usuarioIn) => _usuarios.ReplaceOne(Builders<Usuario>.Filter.Eq(s => s.Id, id), usuarioIn);

        //Borramos un usuario de la lista comparando el usuario con su IP¿
        public void Remove(Usuario usuarioIn) => _usuarios.DeleteOne(usuario => usuario.Id == usuarioIn.Id);
    }
}
