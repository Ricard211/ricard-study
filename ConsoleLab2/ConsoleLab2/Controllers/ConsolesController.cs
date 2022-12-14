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
    public class ConsolesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConsolesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"
             select Id as ""Id"",Name as ""Name"",
Creator as ""Creator"", Year as ""Year"", Units as ""Units"", Generation as ""Generation""
from Consoles";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ConsoleAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon=new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(NpgsqlCommand myCommand=new NpgsqlCommand(query, myCon))
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
            insert into Consoles(Name, Creator, Year, Units, Generation)
            values (@Name,@Creator,@Year,@Units,@Generation)
";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ConsoleAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Id", cons.Id);
                    myCommand.Parameters.AddWithValue("@Creator", cons.Creator);
                    myCommand.Parameters.AddWithValue("@Year", cons.Year);
                    myCommand.Parameters.AddWithValue("@Units", cons.Units);
                    myCommand.Parameters.AddWithValue("@Generation", cons.Generation);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Succesfully");
        }

        [HttpPut]
        public JsonResult Put(Consoles cons)
        {
            string query = @"
            update Consoles
            set Name = @Name,
            Creator = @Creator,
            Year = @Year, 
            Units = @Units, 
            Generation = @Generation
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
                    myCommand.Parameters.AddWithValue("@Id", cons.Id);
                    myCommand.Parameters.AddWithValue("@Creator", cons.Creator);
                    myCommand.Parameters.AddWithValue("@Year", cons.Year);
                    myCommand.Parameters.AddWithValue("@Units", cons.Units);
                    myCommand.Parameters.AddWithValue("@Generation", cons.Generation);
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
            delete from Consoles
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
