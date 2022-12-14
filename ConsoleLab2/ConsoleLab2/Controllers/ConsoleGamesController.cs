using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using ConsoleLab2.Models;

namespace ConsoleLab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsoleGamesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConsoleGamesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"
             select Name as ""Name"",
Developer as ""Developer"", Year as ""Year"", Units as ""Units"", Compatability as ""Compatability""
from ConsoleGames";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ConsoleAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Consoles cons)
        {
            string query = @"
            insert into ConsoleGame (Name,Developer,Year,Units,Compatability)
            values (@Name,@Developer,@Year,@Units,@Compatability)
";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ConsoleAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Name", cons.Name);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Succesfully");
        }
    }
}
