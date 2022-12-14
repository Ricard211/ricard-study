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
             select Name as ""Name"",
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
