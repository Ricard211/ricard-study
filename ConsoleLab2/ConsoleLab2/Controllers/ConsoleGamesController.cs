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
             select Id as ""Id"" ,Name as ""Name"",
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
        public JsonResult Post(ConsoleGames consgam)
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
                    myCommand.Parameters.AddWithValue("@Id", consgam.Id);
                    myCommand.Parameters.AddWithValue("@Name", consgam.Name);
                    myCommand.Parameters.AddWithValue("@Developer", consgam.Developer);
                    myCommand.Parameters.AddWithValue("@Year", consgam.Year);
                    myCommand.Parameters.AddWithValue("@Units", consgam.Units);
                    myCommand.Parameters.AddWithValue("@Compatability", consgam.Compatability);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Succesfully");
        }

        [HttpPut]
        public JsonResult Put(ConsoleGames consgam)
        {
            string query = @"
            update ConsoleGames
            set Name = @Name,
            Developer = @Developer,
            Year = @Year, 
            Units = @Units, 
            Compatability = @Compatability
            where Id=@Id
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
                    myCommand.Parameters.AddWithValue("@Id", consgam.Id);
                    myCommand.Parameters.AddWithValue("@Name", consgam.Name);
                    myCommand.Parameters.AddWithValue("@Developer", consgam.Developer);
                    myCommand.Parameters.AddWithValue("@Year", consgam.Year);
                    myCommand.Parameters.AddWithValue("@Units", consgam.Units);
                    myCommand.Parameters.AddWithValue("@Compatability", consgam.Compatability);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Succesfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
            delete from ConsoleGames
            where Id=@Id
";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ConsoleAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Succesfully");
        }
    }
}
