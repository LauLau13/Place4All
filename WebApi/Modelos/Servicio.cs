namespace WebApi.Modelos
{
    public class Servicio
    {
        //Atributos
        private string Nombre { get; set; }
        private string Descripcion { get; set; }
        static int nextId; //para el ID autoincremental
        private int ID { get; set; }

        //Constructor
        public Servicio(string nombre, string descripcion)
        {
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.ID = Interlocked.Increment(ref nextId);

        }

    }
}
