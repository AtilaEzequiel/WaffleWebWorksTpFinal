using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WaffleWebWorksTpFinal.Models;
// usibg
using Microsoft.Data.SqlClient;

namespace WaffleWebWorksTpFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MovieADO;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        // guardo lo necesario para conectar a la base de datos en un string para despues pegar el string y listo
        List<Carrera> list = new List<Carrera>();
        //lisrta para guardar los resultados del select
        public IActionResult Index()
        {
            try
            {
                //prepara la conectar
                SqlConnection connection = new SqlConnection(connectionString);
                //abre la coencta
                connection.Open();
                // guarda en un string el codigo sql a ejecutar
                string queryString = "Select * from Carrera";
                //string queryString = "INSERT INTO MovieADO (Id, titulo, fecha, genero, precio) VALUES (10, 'Delta', 15/12/1999, 'magia', 600);";
                // no me acurdo, creoq ue guarda en un comadno sql lo que debe ejecutar y en que conexion hacerlo
                SqlCommand command = new SqlCommand(queryString, connection);
                //  command.ExecuteReader(queryString);
                //command.Parameters.AddWithValue("@Id", id);
                //ejecuta el codigo sql y guarda en reader
                SqlDataReader reader = command.ExecuteReader();

                //repite las filas obtenidas
                while (reader.Read())
                {
                    Carrera movieADOs = new Carrera()
                    {
                        //guarda por elemento del modelo carrera
                        Id = int.Parse(reader[0].ToString()),
                        Name = reader[1].ToString(),
                        //
                        //ReleaseDate = DateTime.Parse(reader[2].ToString()),
                        Description = reader[2].ToString(),
                        //  Price = int.Parse(reader[4].ToString()),
                    };
                    //guarda en la lista
                    list.Add(movieADOs);
                    // return View(movieADOs);

                    //  list.Add(movieADOs);
                    //list.Add(movieADOs1);
                }


                //cierrra la conexion
                connection.Close();
                //muestra la lista de resutltado
                return View(list);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Carrera mov)
        {
            try
            {

                SqlConnection connection = new SqlConnection(connectionString);

                connection.Open();

                string queryString = "INSERT INTO Carrera (carrera, Descripcion) VALUES ( @Carrera, @Description);";
                //string queryString = "INSERT INTO MovieADO (Id, titulo, fecha, genero, precio) VALUES (10, 'Delta', 15/12/1999, 'magia', 600);";
                SqlCommand command = new SqlCommand(queryString, connection);
                //  command.ExecuteReader(queryString);
                //toma los parametros obtenidos para despues agregarlos en el consulta sql
                command.Parameters.AddWithValue("@Carrera", mov.Name);
                command.Parameters.AddWithValue("@Description", mov.Description);
                



                //ejecuta la consulta
                SqlDataReader reader = command.ExecuteReader();


                connection.Close();

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Detalle(int id)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                //filtra por id
                string queryString = "Select * from carrera Where Id=@ID";
                //string queryString = "INSERT INTO carrera (Id, titulo, fecha, genero, precio) VALUES (10, 'Delta', 15/12/1999, 'magia', 600);";
                SqlCommand command = new SqlCommand(queryString, connection);
                //  command.ExecuteReader(queryString);
                //necesario hacer este para que sepa a que id filtrar
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Carrera movieADOs = new Carrera()
                    {
                        Id = int.Parse(reader[0].ToString()),
                        Name = reader[1].ToString(),
                        //
                        //ReleaseDate = DateTime.Parse(reader[2].ToString()),
                        Description = reader[2].ToString(),
                    };
                    return View(movieADOs);

                    //  list.Add(movieADOs);
                    //list.Add(movieADOs1);
                }



                connection.Close();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                throw;
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //esto seria editor pero el editor sin httpost es lo mismo que el de arriba. ademas un poco de variedad
        public async Task<IActionResult> Detalle(int id, Carrera mov)
        {

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string queryString = "update carrera   set  carrera=@Name, Descripcion=@Description  where Id = @id";
                //string queryString = "INSERT INTO MovieADO (Id, titulo, fecha, genero, precio) VALUES (10, 'Delta', 15/12/1999, 'magia', 600);";
                SqlCommand command = new SqlCommand(queryString, connection);
                //  command.ExecuteReader(queryString);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", mov.Name);
                command.Parameters.AddWithValue("@Description", mov.Description);
                




                SqlDataReader reader = command.ExecuteReader();


                connection.Close();

                return RedirectToAction("Detalle");

            }
            catch (Exception)
            {

                throw;
            }


        }
        // intente hacer lo mismo que arriba pero no pude, la razon nose 
        public IActionResult Delete(int id)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string queryString = "Select * from carrera Where Id=@ID";
                //string queryString = "INSERT INTO carrera (Id, titulo, fecha, genero, precio) VALUES (10, 'Delta', 15/12/1999, 'magia', 600);";
                SqlCommand command = new SqlCommand(queryString, connection);
                //  command.ExecuteReader(queryString);
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Carrera movieADOs = new Carrera()
                    {
                        Id = int.Parse(reader[0].ToString()),
                        Name = reader[1].ToString(),
                        //
                        //ReleaseDate = DateTime.Parse(reader[2].ToString()),
                        Description = reader[2].ToString(),
                    };
                    return View(movieADOs);

                    //  list.Add(movieADOs);
                    //list.Add(movieADOs1);
                }



                connection.Close();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete( Carrera mov)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string queryString = "delete from carrera where Id = @Id";
                //string queryString = "INSERT INTO MovieADO (Id, titulo, fecha, genero, precio) VALUES (10, 'Delta', 15/12/1999, 'magia', 600);";
                SqlCommand command = new SqlCommand(queryString, connection);
                //  command.ExecuteReader(queryString);
                command.Parameters.AddWithValue("@Id", mov.Id);


                SqlDataReader reader = command.ExecuteReader();


                connection.Close();

                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                throw;
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}